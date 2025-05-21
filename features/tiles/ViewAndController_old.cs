using Godot;
using Tiles;
using System;
using Abstractions;

public abstract partial class ViewAndController : Node, Viewable, Listenable{//I don't think I need Listenable...
	private Node model;
	private int dragTreshold;
	public int DragThreshold {get => dragTreshold; set => dragTreshold = value; }
	private int sideLength;
	public int SideLength {get => sideLength; set => sideLength = value;}
	private int margin;
	public int Margin {get => margin; set => margin = value;}
	private BaseButton tileNode;
	private bool pressing = false;
	private bool pressed = false;
	private Vector2 dragDirection = Vector2.Zero;
	private Vector2 totalDrag = Vector2.Zero;
	private Vector2 lastMousePostion = new Vector2();//Vector2.Zero;
	public Vector2 DragDirection{get => dragDirection;}
	private Node signalEmitter = null;
	public Node SignalEmitter{get => signalEmitter; set => signalEmitter = value;} //signal emitter is the model, but I don't want the view (which this will be when I separate it from the controller, to access the model's implementations, that's not mvc ... come to think of ti neither is the view listening for events but screw it...)
	private StringName signal;
    public StringName Signal { get => signal; set => signal = value; }

    [Signal]
	public delegate void UpdatedEventHandler();

	public ViewAndController(
		Node model_,	
		BaseButton tileNode_,
		int dragTreshold_,
		int sideLength_,
		int margin_
	){
		model = model_;
		dragTreshold = dragTreshold_;
		sideLength = sideLength_;
		margin = margin_;	
		tileNode = tileNode_;	
	}
	public override void _Ready(){
		tileNode.Connect(BaseButton.SignalName.Pressed, Callable.From(OnPressed));
		tileNode.Connect(BaseButton.SignalName.ButtonUp, Callable.From(OnReleased));
		tileNode.Connect(BaseButton.SignalName.ButtonDown, Callable.From(OnPressing));

		//signalEmitter.Connect("JumpTo", Callable.From((Vector2I target) => JumpTo(target))); /* should be enum */
		
	}

    // private void OnJumpTo()
    // {
    //     throw new NotImplementedException();
    // }

    public override void _Input(InputEvent event_)
    {
        base._Input(event_);
		var bp = 123;

		//if(event_ is InputEventScreenDrag drag){
		if(event_ is InputEventMouseMotion move){
			bp = 124;
			if(pressing){
				var delta = move.Relative; 
				// var currentMousePosition = GetViewport().GetMousePosition();
				// var delta = currentMousePosition - lastMousePostion;
				//lastMousePostion = currentMousePosition;
				//Vector2 smoothedMouseDelta = delta.LinearInterpolate(Vector2.Zero, delta * 10);
				totalDrag += delta;

				if(
					totalDrag.Y >= -dragTreshold && 
					totalDrag.Y <= dragTreshold				
				){
					if(totalDrag.X >= dragTreshold){
						dragDirection = Vector2I.Down;
					}
					if(totalDrag.X <= -dragTreshold){
						dragDirection = Vector2I.Up;
					}
				}
				if(
					totalDrag.X >= -dragTreshold && 
					totalDrag.X <= dragTreshold
				){
					if(totalDrag.Y >= dragTreshold){
						dragDirection = Vector2I.Right;
					}
					if(totalDrag.Y <= -dragTreshold){
						dragDirection = Vector2I.Left;
					}
                }
				var yy = dragDirection.Y;
                //Console.WriteLine("drag direction y:  ", yy);
				//((Swapable)model).NotifySwap(new Vector2I((int)dragDirection.X, (int)dragDirection.Y));
            }
		}
    }

	public void Update(Vector2I destination){
		//prob need reverse destination
		Vector2 reverseDestination = MathUtilities.InvertVector(destination);
		Vector2I target_pixel_position = (Vector2I) reverseDestination * (sideLength + margin);
		MoveTo(target_pixel_position, 0.5f);
	}

	private void MoveTo(Vector2 target, float duration){
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(tileNode, "position", target, duration);
		//queue free deletes the whole node including the Control, not just the tween
		tween.Finished += OnMoveFinished;
		GD.Print("MOVED");
	}

	// private void JumpTo(Vector2I target){
	// 	Vector2 reverseDestination = MathUtilities.InvertVector(target); //should be in moveto function, dry
	// 	Vector2I target_pixel_position = (Vector2I) reverseDestination * (sideLength + margin);	
	// 	MoveTo(target_pixel_position, 0.2f);
	// }

	private void OnMoveFinished(){
		EmitSignal(SignalName.Updated);
	}

	private void OnPressed(){
		pressed = true;
		totalDrag = Vector2I.Zero;
		Console.WriteLine("PRESSED");
		GD.Print("+");
	}

	private void OnReleased(){
		pressing = false;
		pressed = false;
		totalDrag = Vector2I.Zero;

		//((Swapable.Model)model).NotifySwap(new Vector2I((int)dragDirection.X, (int)dragDirection.Y));
		
		dragDirection = Vector2I.Zero;
		Console.WriteLine("RELEASED");
	}

	private void OnPressing(){
		pressing = true;
		//pressed = false; //NEW
		Console.WriteLine("DURRRR");
		GD.Print("DURRRR");
	}

    public void Connect()
    {
        throw new NotImplementedException();
    }
}


// 			row
// 	   o------------------------>
//   c |
//   o |  
//   l |
// 	   |
// 	   v

