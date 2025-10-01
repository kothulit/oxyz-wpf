namespace OxyzWPF.Contracts.Mailing;
public interface IMailer
{
    void Subscribe(string eventName, Action callback);

    void Subscribe<T>(string eventName, Action<T> callback);

    void Unsubscribe(string eventName, Action method);

    public void Publish(string eventName, object arg);

    void Update(double deltaTime);
}
