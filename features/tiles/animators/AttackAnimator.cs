using Godot;
using System;

public partial class AttackAnimator : Node, Box{
	[Export]
	private float duration;
	[Export]
	private Node signalEmitter; //prob won't work
	[Export]
	private Node tileNode;
	private int width, height, margin = 0;
    public int Width { get => width; set => width = value; }
    public int Height { get => height; set => height = value; }
    public int Margin { get => margin; set => margin = value; }

    public override void _Ready(){
		(signalEmitter as Offend).Attacked += Attack;
	}

	public void Attack(Vector2I source_, Vector2I target_){
		var target = MathUtilities.InvertVector(target_);
		var source = MathUtilities.InvertVector(source_);
		var direction = source - target;
		var pixelRelativeTarget = direction * width/4;
		var pixelOtherWayAround = MathUtilities.InvertVector(pixelRelativeTarget);
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(
			tileNode, 
			"position", 
			pixelRelativeTarget, 
			duration
		);		
		tween.TweenProperty(
			tileNode, 
			"position", 
			pixelOtherWayAround, 
			duration
		)
			.AsRelative();		
	}

	public void Attack_old(Vector2I source_, Vector2I target_){
		var target = MathUtilities.InvertVector(target_);
		var source = MathUtilities.InvertVector(source_);
		var direction = source - target;
		var pixelTarget = target * width + direction * width/4;
		//var pixelTarget = MathUtilities.InvertVector(reversedPixelTarget);
		var pixelSource = source * width;
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(
			tileNode, 
			"position", 
			pixelTarget, 
			duration
		)
			.From(pixelSource);

		tween.TweenProperty(
			tileNode, 
			"position", 
			pixelSource, 
			duration
		)
			.From(pixelTarget);
		// tween
		// 	.Chain()
		// 	.TweenProperty(
		// 		tileNode, 
		// 		"position", 
		// 		pixelSource, 
		// 		duration				
		// 	);

		//tween.Finished += OnMoveFinished;			
	}
}
