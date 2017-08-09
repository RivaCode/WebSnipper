namespace WebSnipper.UI.Core
{
    public interface ICoreQuery<out T>
    {
        T Execute();
    }
}