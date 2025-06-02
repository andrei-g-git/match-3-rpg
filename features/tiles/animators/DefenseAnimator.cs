using System;
using Defensive;
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
    //public Action<Vector2I, int> AnimateMethod => Defend;


    public override void _Ready(){
		
	}   

    public void AnimateDefending(Vector2I direction, int damage /* not using */){
        var reverseDestinationInPixels = direction * (width + margin)/3;
        var pixelDestination = MathUtilities.InvertVector(reverseDestinationInPixels);
        //var pixelStart = (Vector2) reverseDestinationInPixels;
        //var pixelStart = GetOriginalPoint(pixelDestination);
        //var pixelStart = GetBackwardVector(pixelDestination);
        var pixelStart = MathUtilities.NegateVector(pixelDestination);
        var tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
        tween.TweenProperty(
            tileNode,
            "position",
            pixelDestination,
            duration
        )
            //.From(pixelStart);
            .AsRelative();

        tween.TweenProperty(
            tileNode,
            "position",
            pixelStart,
            duration
        )
            //.From(pixelDestination);
            .AsRelative();

        var bp = 3245;
    } 

    public Vector2 GetOriginalPoint(Vector2 destination){ //make utility
        var axisTravel = Math.Max(destination.X, destination.Y);
        var x = destination.X; //one of these should be 0
        var y = destination.Y;
        var xx = Math.Max(0, x - axisTravel);
        var yy = Math.Max(0, y - axisTravel);
        return new Vector2(xx, yy);
    }

    public Vector2 GetBackwardVector(Vector2 destination){
        var x = destination.X; 
        var y = destination.Y;
        var xx = -x;
        var yy = -y;
        return new Vector2(xx, yy);
    }
}