using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApplication.Models;

[Table("countries")]
public partial class Country
{
    [Key]
    [Column("country_id")]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryId { get; set; } = null!;

    [Column("country_name")]
    [StringLength(40)]
    [Unicode(false)]
    public string CountryName { get; set; } = null!;

    [Column("region_id")]
    public int RegionId { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    [ForeignKey("RegionId")]
    [InverseProperty("Countries")]
    public virtual Region Region { get; set; } = null!;
}
