using FirstApplication.Mappings;
using FirstApplication.Models;
using FirstApplication.Repositories;
using FirstApplication.Repository.Interfaces;
using FirstApplication.Services;
using FirstApplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? Register DbContext
builder.Services.AddDbContext<CompanyManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ? Register Repositories and Services with interfaces
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<IDepartmentRepo,DepartmentRepo>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ignore cycles completely
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

        // Optional: make JSON look nice
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
