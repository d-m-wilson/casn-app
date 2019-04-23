namespace CASNApp.Core.Misc
{
    public struct CommandResult<T>
    {
        public ErrorCode ErrorCode { get; set; }

        public T Data { get; set; }

        public CommandResult(ErrorCode errorCode, T data)
        {
            ErrorCode = errorCode;
            Data = data;
        }

    }
}
