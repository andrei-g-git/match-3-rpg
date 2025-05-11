using Abstractions;
using Godot;

public class Transport : Transportable
{
    Viewable observer;
    public Transport(Viewable observer){
        this.observer = observer;
    }
    public void NotifyTransport(Vector2I target){
        observer.Update(target);
    }
}