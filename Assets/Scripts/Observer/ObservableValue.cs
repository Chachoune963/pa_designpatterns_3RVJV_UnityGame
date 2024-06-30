
public class ObservableValue<T> : Publisher<T>
{
    public T Value
    {
        get => value;
        set
        {
            this.value = value;
            Publish(value);
        }
    }
    
    private T value;
}
