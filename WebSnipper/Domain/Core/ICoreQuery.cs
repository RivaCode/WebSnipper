namespace Domain.Core
{
    public interface ICoreQuery<out T>
    {
        T Execute();
    }

    public interface ICoreQuery<in TIn, out TOut>
    {
        TOut Execute(TIn parameter);
    }
}