using Godot;

public interface Listenable{
    public Node SignalEmitter {get; set;}
    public StringName Signal {get; set;}
    //public void Connect(); //this probably makes the other two superfluous if I passed arguments...
    public void Connect(Node emitter);
}