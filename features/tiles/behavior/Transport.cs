using Abstractions;
using Godot;

public partial class Transport : Node, Transportable
{
    [Signal]
    public delegate void JumpToEventHandler(Vector2I target);
    //Viewable observer;
    // public Transport(Viewable observer){
    //     this.observer = observer;
    // }
    public void NotifyTransport(Vector2I target){
        //observer.Update(target);
        EmitSignal(SignalName.JumpTo, target);
    }
}