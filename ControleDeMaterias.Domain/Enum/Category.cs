using System.ComponentModel;

namespace ControleDeMateriais.Domain.Enum;
public enum Category
{
    [Description("Notebook")]
    Notebook = 0,
    [Description("Smartphone")]
    Smartphone = 1,
    [Description("Tablet")]
    Tablet = 2,
    [Description("Diversos")]
    Diversos = 3,
}
