using Godot;

public interface Listenable{
    public Node SignalEmitter {get; set;}
    public StringName Signal {get; set;}
}