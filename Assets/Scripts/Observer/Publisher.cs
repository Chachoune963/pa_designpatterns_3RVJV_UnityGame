using System.Collections.Generic;

public abstract class Publisher<T>
{
    private List<Subscriber<T>> subscribers = new List<Subscriber<T>>();

    public void Subscribe(Subscriber<T> subscriber)
        => subscribers.Add(subscriber);

    public void Unsubscribe(Subscriber<T> subscriber)
        => subscribers.Remove(subscriber);

    protected void Publish(T value)
    {
        foreach (var subscriber in subscribers)
        {
            subscriber.Notify(value);
        }
    }
}
