using MyDemoProject001.Common.Utilities;

namespace MyDemoProject001.Application.Common.Models
{
    public class ResponseDto<T>
    {
        public T Value { get; set; }

        public bool IsError { get; set; }

        public ResponseCode ResponseCode { get; set; }

        public string JsonResponded { get; set; }

        public string Description { get; set; }
    }
}
