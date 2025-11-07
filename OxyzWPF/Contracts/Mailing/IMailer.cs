namespace OxyzWPF.Contracts.Mailing;
public interface IMailer
{
    public void Subscribe<TEventArgs>(EventEnum eventName, EventHandler<TEventArgs> callback) where TEventArgs : EventArgs;
    public void Unsubscribe(EventEnum eventName, EventHandler callback);
    public void Publish(EventEnum eventName, object sender, EventArgs e);
    public void Update(double deltaTime);
}
