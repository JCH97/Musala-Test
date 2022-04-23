namespace MusalaTest.Domain.Interfaces
{
    public interface ISimpleWrapper<TResponse>
    {
        bool IsSuccess { get; set; }

        TResponse Value { get; set; }

        string ErrorMessage { get; set; }
    }
}