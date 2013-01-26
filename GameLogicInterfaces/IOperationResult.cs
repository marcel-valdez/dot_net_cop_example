using System;
namespace Game.Logic
{
    public interface IOperationResult
    {
        /*
            Es el resultado de la operación
            { Success | Fail | Error }
            Nota: La diferencia entre Fail y Error puede ser que Fail
            es un resultado preponderado, y Error un resultado no esperado
        */
        ResultValue Result
        {
            get;
        }

        /*
            Mensaje de error
        */
        string Message
        {
            get;
        }
    }

    /*
    Responsabilidades:
    Conocer el resultado de una operación y el mensaje enviado como resultado de la misma
*/
    public interface IOperationResult<T> : IOperationResult
    {
        /// <summary>
        /// Gets the result data of the operation.
        /// </summary>
        T ResultData
        {
            get;
        }
    }

    public enum ResultValue
    {
        Success = 0,
        Fail = 1,
        Error = 2
    }
}