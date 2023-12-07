using System.ComponentModel;
using System.Reflection;

namespace ControleDeMateriais.Communication.Enum;
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

public static class EnumExtensions
{
    public static string GetDescription(this Category value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        if (field is null)
            return value.ToString();

        DescriptionAttribute attribute =
            (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

        return attribute is null ? value.ToString() : attribute.Description;
    }
}