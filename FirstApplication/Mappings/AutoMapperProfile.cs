using AutoMapper;
using FirstApplication.DTOs;
using FirstApplication.Models;

namespace FirstApplication.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Map EmployeeDto → Employee
            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore()) // auto-generated ID
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.Job, opt => opt.Ignore())
                .ForMember(dest => dest.Manager, opt => opt.Ignore())
                .ForMember(dest => dest.InverseManager, opt => opt.Ignore());

            // Map AddressDto → Address
            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());

            // Map DependentDto → Dependent
            CreateMap<DependentDto, Dependent>()
                .ForMember(dest => dest.DependentId, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
                .ForMember(dest => dest.Employee, opt => opt.Ignore());
            CreateMap<DepartmentDto, Department>()
                .ForMember(de => de.DepartmentId, opt => opt.Ignore())
                .ForMember(de => de.Location, opt => opt.Ignore())
                .ForMember(de => de.Employees, opt => opt.Ignore());
        }
    }
}
