namespace ShoppingCart_Application.Responses
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public Response()
        {
            this.StatusCode = 200;
            this.IsSuccess = true;
            this.Message = "";
            //this.Data = null;
            this.Errors = new List<string>();
        }
    }
}
