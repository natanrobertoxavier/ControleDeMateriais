﻿using ControleDeMateriais.Communication.Enum;

namespace ControleDeMateriais.Communication.Requests;
public class RequestRegisterMaterialJson
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string BarCode { get; set; }
    public Category Category { get; set; }
}
