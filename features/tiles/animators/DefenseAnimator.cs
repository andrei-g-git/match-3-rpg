using System;
using Godot;

public partial class DefenseAnimator: Node, Box, Defensive.View{
    [Export]
	private float duration;
	[Export]
	private Node signalEmitter; 
	[Export]
	private Node tileNode;
	private int width, height, margin = 0;
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public int Margin { get => margin; set => margin = value; }
    public Action<Vector2I> AnimateMethod => Defend;

    public override void _Ready(){
		
	}   

    public void Defend(Vector2I direction){
        var reverseDestinationInPixels = direction * (width + margin)/3;
        var pixelDestination = MathUtilities.InvertVector(reverseDestinationInPixels);
        var tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
        tween.TweenProperty(
            tileNode,
            "position",
            pixelDestination,
            duration
        );
    } 
}