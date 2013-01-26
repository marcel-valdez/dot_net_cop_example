namespace Game.Logic.Impl
{
    using System;
    /*
            Responsabilidades:
            Conocer el resultado de una operación y el mensaje enviado como resultado de la misma
        */
    internal class OperationResult<T> : OperationResult, IOperationResult<T>
    {
        public OperationResult(ResultValue value, string message, T data)
            : base(value, message)
        {
            this.ResultData = data;
        }

        #region IOperationResult<T> Members

        public T ResultData
        {
            get;
            private set;
        }

        #endregion
    }

    internal class OperationResult : IOperationResult
    {
        public OperationResult(ResultValue value, string message)
        {
            this.Result = value;
            this.Message = message;
        }

        public ResultValue Result
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }
    }
}
