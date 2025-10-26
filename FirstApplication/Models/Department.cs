using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApplication.Models;

[Table("departments")]
public partial class Department
{
    [Key]
    [Column("department_id")]
    public int DepartmentId { get; set; }

    [Column("department_name")]
    [StringLength(30)]
    [Unicode(false)]
    public string DepartmentName { get; set; } = null!;

    [Column("location_id")]
    public int? LocationId { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [ForeignKey("LocationId")]
    [InverseProperty("Departments")]
    public virtual Location? Location { get; set; }
}
