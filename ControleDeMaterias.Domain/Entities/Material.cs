﻿using ControleDeMateriais.Domain.Enum;

namespace ControleDeMateriais.Domain.Entities;
public class Material : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string BarCode { get; set; }
    public Category Category { get; set; }
}