namespace Fintranet.TaxCalculation.Message.DTOs
{
    public class ResponseMessage
    {
        public ResponseMessage()
        {

        }

        /// <inheritdoc />
        public ResponseMessage(string message)
        {
            Message = message;
        }

        public ResponseMessage(string message, object content)
        {
            Message = message;
            Content = content;
            Total = 0;
        }

        /// <inheritdoc />
        public ResponseMessage(string message, object content, int total)
        {
            Message = message;
            Content = content;
            Total = total;
        }

        public string Message { get; set; }

        public object Content { get; set; }
        public int Total { get; set; }

    }

    public class ResponseMessage<T>
    {
        public ResponseMessage()
        {

        }

        /// <inheritdoc />
        public ResponseMessage(string message)
        {
            Message = message;
        }

        public ResponseMessage(string message, T content)
        {
            Message = message;
            Content = content;
            Total = 0;
        }

        /// <inheritdoc />
        public ResponseMessage(string message, T content, int total)
        {
            Message = message;
            Content = content;
            Total = total;
        }

        public string Message { get; set; }
        public T Content { get; set; }
        public int Total { get; set; }
    }
}
