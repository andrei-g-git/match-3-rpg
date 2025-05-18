using Abstractions;
using Godot;

public partial class TileNode : TextureButton, Controllable, Modelable, Animatable
{
    [Export] public int SideLength {get; set;} = 64;
    [Export] public int Margin {get; set;}= 3;
    private Node controller = null;
    public Node Controller { get => controller; set => controller = value; }
    private Node model = null;
    public Node Model { get => model; set => model = value; }
    private Node animators = null;
    public Node Animators { get => animators; set => animators = value; }

    //provisory
    private Swapable.View swapAnimator = null;
    public Swapable.View SwapAnimator { get => swapAnimator;}

    public override void _Ready(){
        //children will call this before overriding ... so it's not really being overriden
        controller = GetNode<Node>("%Controller"); 
        animators = GetNode<Node>("%Animators"); 
        model = GetNode<Node>("%Model"); 

        //((Tiles.Model/* not ideal... */) model).SwapBehavior = GetNode<Node>("%Swapper") as Swapable.Model; //not ideal either....

        swapAnimator = GetNode<Node>("%Swap") as Swapable.View;
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