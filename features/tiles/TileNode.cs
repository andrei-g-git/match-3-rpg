using Abstractions;
using Constants;
using Godot;

public partial class TileNode : TextureButton, Controllable,/*  Modelable, */ Animatable{
    [Export] public int SideLength {get; set;} = 64;
    [Export] public int Margin {get; set;}= 3;
    public /* TileName */ string Type => (model as Tiles.Model).Name;//(model as Tiles.Model).Type; 
    // [Export]
    private Node controller;
    public /* virtual */ Node Controller { get => controller; set => controller = value; }
    //[Export]
    private Node model; //this doesn't actually work I don't know why
    public Node Model { get => model; set => model = value; }
    //[Export]
    private Node animators;
    public Node Animators { get => animators; set => animators = value; }

    //provisory
    //[Export]
    private Swapable.View  swapAnimator;// = null;
    public Swapable.View SwapAnimator { get => swapAnimator;}

    public override void _Ready(){
        //children will call this before overriding ... so it's not really being overriden
        //controller = GetNode<Node>("%DragController"); //this doesn't work either
        controller = GetNode<Node>("%Controller");
        animators = GetNode<Node>("%Animators"); 
        model = GetNode<Node>("%Model"); 

        //((Tiles.Model/* not ideal... */) model).SwapBehavior = GetNode<Node>("%Swapper") as Swapable.Model; //not ideal either....

        swapAnimator = GetNode<Node>("%Swap") as Swapable.View;

        // //var swapAnimator = animators.GetChild<Node>("%Swap");
        // // var swapAnimator = GetNode<Node>("%Swap") as Box;
        // // swapAnimator.Width = sideLength;
        // // swapAnimator.Height = sideLength;
        // // swapAnimator.Margin = margin;
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