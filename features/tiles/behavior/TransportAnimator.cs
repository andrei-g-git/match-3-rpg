using Godot;
using Tiles;

public partial class TransportAnimator: Node, Listenable, /* Animatable, */ Transportable.View{
    private Node tileNode;
    private int sideLength;
    private int margin;
    private Node signalEmitter = null;
    public Node SignalEmitter { get => signalEmitter; set => signalEmitter = value; }
    private StringName signal;
    public StringName Signal { get => signal; set => signal = value; }

    public TransportAnimator(Node tileNode, int sideLength, int margin){
        this.tileNode = tileNode;
        this.sideLength = sideLength;
        this.margin = margin;
    }

    public override void _Ready(){ //hasn't been added as child, won't run
        var abc = 123;
        //signalEmitter.Connect(signal, Callable.From((Vector2I target) => JumpTo(target))); /* should be enum */
    }

    public void Connect(){
        signalEmitter.Connect(signal, Callable.From((Vector2I target) => JumpTo(target)));
    }

    private void JumpTo(Vector2I target){
		Vector2 reverseDestination = MathUtilities.InvertVector(target); //should be in moveto function, dry
		Vector2I target_pixel_position = (Vector2I) reverseDestination * (sideLength + margin);	
		MoveTo(target_pixel_position, 0.2f);
	}

    private void MoveTo(Vector2 target, float duration){
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(tileNode, "position", target, duration);
		//tween.Finished += OnMoveFinished;
	}



    // public void Animate()
    // {
    //     throw new System.NotImplementedException();
    // }

    // private void OnMoveFinished(){
    // 	EmitSignal(SignalName.Updated);
    // }
}