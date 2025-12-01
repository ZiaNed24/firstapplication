using System;
using System.Collections.Generic;
using FirstApplication.DTOs;

namespace FirstApplication.DTOs
{
	
	public class EmployeeDto
	{
		public string? FirstName { get; set; }
		public string LastName { get; set; } = null!;
		public string Email { get; set; } = null!;
        public string EmploymentStatus { get; set; }
        public string? PhoneNumber { get; set; }
		public DateOnly HireDate { get; set; }
		public int? JobId { get; set; }
		public decimal? Salary { get; set; }
		public int? ManagerId { get; set; }
		public int? DepartmentId { get; set; }
		public int? RoleId {  get; set; }	
		public List<AddressDto>? Addresses { get; set; }
		public List<DependentDto>? Dependents { get; set; }
	}

	
	public class AddressDto
	{
		public string Street { get; set; } = null!;
		public string? Suburb { get; set; }
		public string? State { get; set; }
		public string? Country { get; set; }
		public bool IsPrimary { get; set; }
	}


	public class DependentDto
	{
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string Relationship { get; set; } = null!;
	}
	public class DepartmentDto
	{
        public string DepartmentName { get; set; } = null!;
        public int? LocationId { get; set; }


    }
    public class EmployeeResponseDto
    {
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateOnly HireDate { get; set; }

        public string? JobTitle { get; set; }
        public string? DepartmentName { get; set; }
        public string? ManagerName { get; set; }
        public decimal? Salary { get; set; }
		public string? EmploymentStatus { get; set; }
    }
}
