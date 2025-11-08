namespace OxyzWPF.Contracts.Mailing;
public interface IMessenger
{
    public void Subscribe<TEventArgs>(string eventName, EventHandler<TEventArgs> callback) where TEventArgs : EventArgs;
    public void Unsubscribe(string eventName, EventHandler callback);
    public void Publish(string eventName, object sender, EventArgs e);
    public void Update(double deltaTime);
}
