using System;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerceAPII.Domain.Entities.Common;

namespace ECommerceAPII.Domain.Entities;

public class File :BaseEntity
{
    public string FileName { get; set; }

    public string Path { get; set; }

    public string Storage { get; set; }

    [NotMapped]
    public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
}

