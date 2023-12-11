using System.Runtime.Serialization;

namespace ControleDeMateriais.Exceptions.ExceptionBase;
public class ExceptionBorrowedMaterialErros : ControleDeMateriaisException
{
    public List<string> MessagesErrors { get; set; }
    public ExceptionBorrowedMaterialErros(List<string> messageErrors) : base(string.Empty)
    {
        MessagesErrors = messageErrors;
    }

    protected ExceptionBorrowedMaterialErros(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
