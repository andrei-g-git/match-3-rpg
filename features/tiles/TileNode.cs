using Abstractions;
using Godot;
using Tiles;

public partial class TileNode : TextureButton, Controllable, /* Modelable, */ Viewable, Listenable
{
	[Export] int dragTreshold = 16;
	[Export] int sideLength = 64;
	[Export] int margin = 3;
	private Node controller = null;
	private Node model = null;
	public Node Controller{get => controller; set => controller = value;}
	public Node Model{get => model; set => model = value;}
	private Node signalEmiter = null;
    public Node SignalEmitter { get => ((Listenable) controller).SignalEmitter; set => ((Listenable) controller).SignalEmitter = value; }

    public StringName Signal { get => ((Listenable) controller).Signal; set => ((Listenable) controller).Signal = value; }

    // Called when the node enters the scene tree for the first time.

	//these are temporary until I can figure out how to pass them before _ready() ---NOPE


    public override void _Ready(){
        //model = new Model();
		// controller = new ViewAndController(
		// 	model,
		// 	this,
		// 	dragTreshold,
		// 	sideLength,
		// 	margin
		// );
		//((Viewable) controller).SignalEmitter = signalEmiter;
		//AddChild(controller);
	}

	public void SetController(Node controller){
		this.controller = controller;
		((ViewAndController) controller).Margin = margin;
		((ViewAndController) controller).DragThreshold = dragTreshold;
		((ViewAndController) controller).SideLength = sideLength;
		AddChild(this.controller);
	}

	public void Update(Vector2I destination){
		((Viewable) controller).Update(destination);
	}

    public void Connect(){
        ((Listenable) controller).Connect();
    }

}
