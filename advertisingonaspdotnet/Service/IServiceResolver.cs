namespace advertisingonaspdotnet.Service;

public interface IServiceResolver
{
    T Get<T>() where T : notnull;
}