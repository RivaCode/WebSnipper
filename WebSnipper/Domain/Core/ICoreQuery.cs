namespace Domain.Core
{
    public interface ICoreQuery<out T>
    {
        T Execute();
    }
}