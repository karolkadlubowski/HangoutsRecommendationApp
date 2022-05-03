namespace Library.Shared.Models.Response
{
    public record BaseResponse
    {
        public bool IsSucceeded { get; init; }
        public Error Error { get; init; }

        public BaseResponse(Error error = null)
            => (IsSucceeded, Error) = (error == null, Error = error);
    }
}