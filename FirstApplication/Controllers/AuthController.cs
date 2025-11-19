using Isopoh.Cryptography.Argon2;
using FirstApplication.DTOs;
using FirstApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MI.CRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly CompanyManagementContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(CompanyManagementContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ✅ Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _context.Users.AsNoTracking().AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists.");

            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);

            var config = new Argon2Config
            {
                Type = Argon2Type.HybridAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 2,
                MemoryCost = 1 << 15,
                Lanes = 1,
                Threads = 1,
                Salt = salt,
                HashLength = 32,
                Password = Encoding.UTF8.GetBytes(dto.Password),
            };

            string hash = Argon2.Hash(config);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                RoleId = dto.RoleId,
                PasswordHash = hash,
                CreatedOn = DateTime.UtcNow,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        // ✅ Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return Unauthorized("Invalid email or password");

            if (!Argon2.Verify(user.PasswordHash, dto.Password))
                return Unauthorized("Invalid email or password");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expires = token.ValidTo,
                user = new { user.UserId, user.Name, user.Email, user.RoleId }
            });
        }

        // ✅ Forgot Password → Email reset link
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                return Ok("If user exists, reset link has been sent.");

            // Generate JWT token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15), // short expiry
                signingCredentials: creds
            );

            var resetToken = new JwtSecurityTokenHandler().WriteToken(token);
            var resetLink = $"http://localhost:4200/reset-password?token={resetToken}&email={user.Email}";

            // ✅ Send Email via SMTP Gmail
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_configuration["Smtp:Email"], _configuration["Smtp:Password"]),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["Smtp:Email"], "MI CRM"),
                    Subject = "Password Reset Link",
                    Body = $"Click the link to reset your password: {resetLink}",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(user.Email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                return BadRequest("Error sending email: " + ex.Message);
            }

            return Ok(new { message = "Reset link sent to email" });
        }

        // ✅ Reset Password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                var principal = tokenHandler.ValidateToken(dto.Token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true
                }, out SecurityToken validatedToken);

                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                if (email != dto.Email) return BadRequest("Invalid token");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
                if (user == null) return BadRequest("User not found");

                // Hash new password
                var salt = new byte[16];
                using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(salt);

                var config = new Argon2Config
                {
                    Type = Argon2Type.HybridAddressing,
                    Version = Argon2Version.Nineteen,
                    TimeCost = 2,
                    MemoryCost = 1 << 15,
                    Lanes = 1,
                    Threads = 1,
                    Salt = salt,
                    HashLength = 32,
                    Password = Encoding.UTF8.GetBytes(dto.NewPassword),
                };

                user.PasswordHash = Argon2.Hash(config);
                await _context.SaveChangesAsync();

                return Ok("Password reset successful");
            }
            catch
            {
                return BadRequest("Invalid or expired token");
            }
        }
    }
}
