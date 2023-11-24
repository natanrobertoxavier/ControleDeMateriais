using System.Runtime.Serialization;

namespace ControleDeMateriais.Exceptions.ExceptionBase;
public class ExceptionRecoveryErrors : ControleDeMateriaisException
{
    public ExceptionRecoveryErrors(string message) : base(message)
    {
    }


    protected ExceptionRecoveryErrors(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
