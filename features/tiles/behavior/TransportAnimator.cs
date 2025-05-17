using Godot;
using Tiles;

public partial class TransportAnimator: Node, Listenable, /* Animatable, */ Transportable.View, Parentable, Box{
    private Node tileNode;
    // private int sideLength;
    // private int margin;
    private int width, height, margin = 0;
    private Node signalEmitter = null;
    public Node SignalEmitter { get => signalEmitter; set => signalEmitter = value; }
    private StringName signal;
    public StringName Signal { get => signal; set => signal = value; }
    private Node parentNode = null; //this is tileNode, the hell is this here?...
    public Node ParentNode {get => parentNode; set => parentNode = value;}
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public int Margin { get => margin; set => margin = value; }

    [Signal]
    public delegate void UpdatedEventHandler();

    // public TransportAnimator(Node tileNode, int sideLength, int margin){
    //     this.tileNode = tileNode;
    //     this.sideLength = sideLength;
    //     this.margin = margin;
    // }

    public override void _Ready(){ //hasn't been added as child, won't run
        var abc = 123;
        //signalEmitter.Connect(signal, Callable.From((Vector2I target) => JumpTo(target))); /* should be enum */



        //tileNode.AddChild(this);
    }

    public void Connect(){
        signalEmitter.Connect(signal, Callable.From((Vector2I target) => JumpTo(target)));
    }

    private void JumpTo(Vector2I target){
		Vector2 reverseDestination = MathUtilities.InvertVector(target); //should be in moveto function, dry
		Vector2I target_pixel_position = (Vector2I) reverseDestination * (/* sideLength */width + margin);	
		MoveTo(target_pixel_position, 0.2f);
	}

    private void MoveTo(Vector2 target, float duration){
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
        //queue free deletes the whole node including the Control, not just the tween
        //parentNode.AddChild((Node) tween);
        
		tween.TweenProperty(tileNode, "position", target, duration);
        tween.Finished += OnMoveFinished;
		//tween.Finished += OnMoveFinished;
	}


    private void OnMoveFinished(){
    	EmitSignal(SignalName.Updated);
    }
}