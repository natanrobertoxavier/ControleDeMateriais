using System.Runtime.Serialization;

namespace ControleDeMateriais.Exceptions.ExceptionBase;
public class ControleDeMateriaisException : SystemException
{
    public ControleDeMateriaisException(string message) : base(message) 
    { 
    }

    public ControleDeMateriaisException(SerializationInfo info, StreamingContext context) : base(info, context)
    { 
    }
}
