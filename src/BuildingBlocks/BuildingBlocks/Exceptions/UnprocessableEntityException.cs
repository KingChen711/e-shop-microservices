namespace BuildingBlocks.Exceptions;

public class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException(Dictionary<string, string> errors)
        : base("UnprocessableEntityException")
    {
        Errors = errors;
    }

    public Dictionary<string, string> Errors { get; set; }
}
