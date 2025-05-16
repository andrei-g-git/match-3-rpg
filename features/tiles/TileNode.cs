using Godot;

public partial class TileNode : TextureButton
{
    [Export] int sideLength = 64;
    [Export] int margin = 3;



    public override void _Ready(){
        //base._Ready();
        var animators = GetNode<Node>("%Animators"); //don't need
        //var swapAnimator = animators.GetChild<Node>("%Swap");
        var swapAnimator = GetNode<Node>("%Swap") as Box;
        swapAnimator.Width = sideLength;
        swapAnimator.Height = sideLength;
        swapAnimator.Margin = margin;
    }
}