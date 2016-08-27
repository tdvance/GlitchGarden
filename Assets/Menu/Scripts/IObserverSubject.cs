public interface IObserverSubject
{
    //GOF p. 293
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(object message);

}