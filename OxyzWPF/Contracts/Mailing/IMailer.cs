namespace OxyzWPF.Contracts.Mailing;
public interface IMailer
{
    void Subscribe(EventEnum eventName, Action callback);

    void Subscribe<T>(EventEnum eventName, Action<T> callback);

    void Unsubscribe(EventEnum eventName, Action method);

    public void Publish(EventEnum eventName, object arg);

    void Update(double deltaTime);
}
