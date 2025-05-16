using Abstractions;
using Godot;

public partial class TileNode : TextureButton, Controllable, Modelable, Animatable
{
    [Export] int sideLength = 64;
    [Export] int margin = 3;
    private Node controller = null;
    public Node Controller { get => controller; set => controller = value; }
    private Node model = null;
    public Node Model { get => model; set => model = value; }
    private Node animators = null;
    public Node Animators { get => animators; set => animators = value; }

    public override void _Ready(){
        //base._Ready();
        controller = GetNode<Node>("%Controller"); 
        animators = GetNode<Node>("%Animators"); 
        model = GetNode<Node>("%Model"); 
        //var swapAnimator = animators.GetChild<Node>("%Swap");
        // var swapAnimator = GetNode<Node>("%Swap") as Box;
        // swapAnimator.Width = sideLength;
        // swapAnimator.Height = sideLength;
        // swapAnimator.Margin = margin;
    }    

    
    public void Register(Control tileNode)
    {
        //throw new System.NotImplementedException();
    }

    public void SetController(Node controller)
    {
        //throw new System.NotImplementedException();
    }
}