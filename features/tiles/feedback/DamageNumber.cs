using Godot;
using System;

public partial class DamageNumber : Node, DisplayableNumber{
	[Export]
	private Label label;
	[Export]
	private Node container;
	[Export]
	private AnimationPlayer animationPlayer;
	[Export]
	private float spread;
	[Export]
	private float height;
	public override void _Ready(){

	}

	public void DisplayNumberAt(Vector2I position, int value){
		label.Text = value.ToString();

		animationPlayer.Play("fade_scale_in_out");
		var tween = CreateTween();
		var endPosition = new Vector2(
			new Random().Next((int)-spread, (int)spread),
			-height
		) + position;
		var duration = animationPlayer.GetAnimation("fade_scale_in_out").Length;

		var pixelStart = MathUtilities.InvertVector(position * 48);
		var pixelEnd = MathUtilities.InvertVector(endPosition * 48);
		tween.TweenProperty(
			container,
			"position",
			pixelStart,
			duration
		)
			.From(pixelEnd);
	}
}
