using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApplication.Models;

[Table("regions")]
public partial class Region
{
    [Key]
    [Column("region_id")]
    public int RegionId { get; set; }

    [Column("region_name")]
    [StringLength(25)]
    [Unicode(false)]
    public string RegionName { get; set; } = null!;

    [InverseProperty("Region")]
    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
}
