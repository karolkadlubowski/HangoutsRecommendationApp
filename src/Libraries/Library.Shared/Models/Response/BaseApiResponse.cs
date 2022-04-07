namespace Library.Shared.Models.Response
{
    public record BaseApiResponse
    {
        public bool IsSucceeded { get; init; }
        public Error Error { get; init; }

        public BaseApiResponse(Error error = null)
            => (IsSucceeded, Error) = (error == null, Error = error);
    }
}