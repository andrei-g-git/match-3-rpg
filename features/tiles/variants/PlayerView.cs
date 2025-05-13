using Godot;

public class PlayerView : Listenable
{
    public Node SignalEmitter { get => ((Listenable) transportAnimator).SignalEmitter; set => ((Listenable) transportAnimator).SignalEmitter = value; }
    private Transportable.View transportAnimator = null;
    public Transportable.View TransportAnimator { get => transportAnimator; set => transportAnimator = value; }
    public StringName Signal { get => ((Listenable) transportAnimator).Signal; set => ((Listenable) transportAnimator).Signal = value; }

    public void ConnectWithTransportSignal(Node signalEmitter){
        ((Listenable )transportAnimator).SignalEmitter = signalEmitter;
    }
}