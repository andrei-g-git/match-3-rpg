using Abstractions;
using Godot;
using Tiles;

public partial class TileNode : TextureButton, Controllable, Modelable
{
	[Export] int dragTreshold = 16;
	[Export] int sideLength = 64;
	[Export] int margin = 3;
	private Node controller = null;
	private Node model = null;
	public Node Controller{get => controller; /* set => controller = value; */}
	public Node Model{get => model; set => model = value;}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready(){
        //model = new Model();
		controller = new ViewAndController(
			model,
			this,
			dragTreshold,
			sideLength,
			margin
		);
		AddChild(controller);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
