using System.Runtime.Serialization;

namespace ControleDeMateriais.Exceptions.ExceptionBase;
public class ExceptionLoginInvalid : ControleDeMateriaisException
{
    public ExceptionLoginInvalid() : base(ErrorMessagesResource.LOGIN_INVALIDO)
    {
    }

    protected ExceptionLoginInvalid(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
