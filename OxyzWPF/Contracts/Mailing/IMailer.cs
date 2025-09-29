namespace OxyzWPF.Contracts.Mailing;
public interface IMailer
{
    public void Subscribe(string eventName, Action method);

    public void Unsubscribe(string eventName, Action method);

    void Publish(string eventName);

    void Update(double deltaTime);
}
