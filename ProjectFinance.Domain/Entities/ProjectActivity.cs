﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinance.Domain.Entities;

[Table("ProjectActivity")]
public partial class ProjectActivity
{
    [Key]
    public int Id { get; set; }

    public int? ActivityId { get; set; }

    public int? ProjectId { get; set; }

    [ForeignKey("ActivityId")]
    [InverseProperty("ProjectActivities")]
    public virtual Activity? Activity { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectActivities")]
    public virtual Project? Project { get; set; }

    [InverseProperty("ProjectActivity")]
    public virtual ICollection<ProjectActivityCost> ProjectActivityCosts { get; set; } = new List<ProjectActivityCost>();
}
