using Godot;
using Tiles;

public partial class TransportAnimator: Node, Listenable, Transportable.View, Parentable, Box{
    [Export]
    private float duration;
    // [Export]
    private Node tileNode; //or could get as Owner but let's try dependency injection for good practice
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
    [Signal]
    public delegate void TransportedEventHandler();

    public override void _Ready(){ 
        // width = (tileNode as TileNode).SideLength; //AAaaand this is hard coupling...
        // height = (tileNode as TileNode).SideLength;
        // margin = (tileNode as TileNode).Margin;
        //tileNode is 2 tiles up so it should be GetParent().GetParent()
        tileNode = GetParent().GetParent(); //hiiiideous  //GetNode<Node>("%Tile"); //doesn't find it since this is a child class with a different scene name
        //(GetNode<Node>("%Transporter") as Transport).JumpTo += JumpTo; //obviously I won't be keeping this
        //(GetNode<Node>("%Transfer") as Transfer).TransferingActor += JumpTo; //this belongs to the transfering buff buff tile....
        //(signalEmitter as Transfer).TransferingActor += JumpTo;
    }



    // public void Connect(){ //will discard
    //     signalEmitter.Connect(signal, Callable.From((Vector2I target) => JumpTo(target))); //IT THINKS IT"S CALLING Connect() method of Object, that's why it's not working
    // }

    public void Connect(Node signalEmitter){
        this.signalEmitter = signalEmitter; //probably unnecessary
        (signalEmitter as Transfer).TransferingActor += JumpTo;
    }

    private void JumpTo(Vector2I target){
		Vector2 reverseDestination = MathUtilities.InvertVector(target); //should be in moveto function, dry
		Vector2I target_pixel_position = (Vector2I) reverseDestination * (/* sideLength */width + margin);	
		MoveTo(target_pixel_position, duration);
	}

    private void MoveTo(Vector2 target, float duration){
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
        //queue free deletes the whole node including the Control, not just the tween
        
		tween.TweenProperty(tileNode, "position", target, duration);
        tween.Finished += OnMoveFinished;
		//tween.Finished += OnMoveFinished;
	}


    private void OnMoveFinished(){ //this is incorrect naming for an emitter, OnAction name should be used by the receiver, not the invoker
    	EmitSignal(SignalName.Updated);

        EmitSignal(SignalName.Transported);
    }
}