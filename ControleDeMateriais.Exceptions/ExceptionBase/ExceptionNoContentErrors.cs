using System.Runtime.Serialization;

namespace ControleDeMateriais.Exceptions.ExceptionBase;
public class ExceptionNoContentErrors : ControleDeMateriaisException
{
    public List<string> MessagesErrors { get; set; }
    public ExceptionNoContentErrors(List<string> messageErrors) : base(string.Empty)
    {
        MessagesErrors = messageErrors;
    }

    protected ExceptionNoContentErrors(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
