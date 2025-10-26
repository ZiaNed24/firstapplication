using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApplication.Models;

[Table("addresses")]
public partial class Address
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("street")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Street { get; set; }

    [Column("suburb")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Suburb { get; set; }

    [Column("state")]
    [StringLength(50)]
    [Unicode(false)]
    public string? State { get; set; }

    [Column("country")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Country { get; set; }

    [Column("is_primary")]
    public bool? IsPrimary { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Addresses")]
    public virtual Employee Employee { get; set; } = null!;
}
