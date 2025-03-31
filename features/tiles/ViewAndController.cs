using Godot;
using Tiles;
using System;

public partial class ViewAndController : Node{
	private Node model;
	private int dragTreshold;
	private int sideLengt;
	private int margin;
	private BaseButton tileNode;
	private bool pressing = false;
	private bool pressed = false;
	private Vector2 dragDirection = Vector2.Zero;
	private Vector2 totalDrag = Vector2.Zero;
	private Vector2 lastMousePostion = new Vector2();//Vector2.Zero;
	public Vector2 DragDirection{get => dragDirection;}

	public ViewAndController(
		Node model_,	
		BaseButton tileNode_,
		int dragTreshold_,
		int sideLengt_,
		int margin_
	){
		model = model_;
		dragTreshold = dragTreshold_;
		sideLengt = sideLengt_;
		margin = margin_;	
		tileNode = tileNode_;	
	}
	public override void _Ready(){
		tileNode.Connect(BaseButton.SignalName.Pressed, Callable.From(OnPressed));
		tileNode.Connect(BaseButton.SignalName.ButtonUp, Callable.From(OnReleased));
		tileNode.Connect(BaseButton.SignalName.ButtonDown, Callable.From(OnPressing));
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
	}

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
				((Swapable)model).NotifySwap(new Vector2I((int)dragDirection.X, (int)dragDirection.Y));
            }
		}
    }


	private void OnPressed(){
		pressed = true;
		totalDrag = Vector2I.Zero;
		Console.WriteLine("PRESSED");
	}

	private void OnReleased(){
		pressing = false;
		pressed = false;
		totalDrag = Vector2I.Zero;
		dragDirection = Vector2I.Zero;
		Console.WriteLine("RELEASED");
	}

	private void OnPressing(){
		pressing = true;
		Console.WriteLine("DURRRR");
	}

}


// 			row
// 	   o------------------------>
//   c |
//   o |  
//   l |
// 	   |
// 	   v

