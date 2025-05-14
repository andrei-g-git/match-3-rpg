using Godot;

public partial class PlayerView : ViewAndController, Listenable
{
    public Node SignalEmitter { get => ((Listenable) transportAnimator).SignalEmitter; set => ((Listenable) transportAnimator).SignalEmitter = value; }
    private Transportable.View transportAnimator = null;
    public Transportable.View TransportAnimator { get => transportAnimator; set => transportAnimator = value; }
    public StringName Signal { get => ((Listenable) transportAnimator).Signal; set => ((Listenable) transportAnimator).Signal = value; }

	public PlayerView(
		Node model_,	
		BaseButton tileNode_,
		int dragTreshold_,
		int sideLength_,
		int margin_
	): 
        base(
            model_,	
            tileNode_,
            dragTreshold_,
            sideLength_,
            margin_
        ){
            var abc = 1123;
        }

    public void Connect(){
        ((Listenable )transportAnimator).Connect();
    }

    // public void ConnectWithTransportSignal(Node signalEmitter){
    //     ((Listenable )transportAnimator).SignalEmitter = signalEmitter;
    // }
}