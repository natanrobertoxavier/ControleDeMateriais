using System.Runtime.Serialization;

namespace ControleDeMateriais.Exceptions.ExceptionBase;
public class LoginInvalidException : ControleDeMateriaisException
{
    public LoginInvalidException() : base(ErrorMessagesResource.LOGIN_INVALIDO)
    {
    }

    protected LoginInvalidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
