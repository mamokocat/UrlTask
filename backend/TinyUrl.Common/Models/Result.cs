using TinyUrl.Common.Interfaces;

namespace TinyUrl.Common.Models
{
    public class Result<T> : IResult<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }

        public Result(T value, bool isSuccess)
        {
            this.IsSuccess = isSuccess;
            this.Value = value;
        }
    }
}
