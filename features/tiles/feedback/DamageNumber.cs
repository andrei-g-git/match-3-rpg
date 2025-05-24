using Godot;
using System;

public partial class DamageNumber : Node, DisplayableText{
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

	public void DisplayTextAt(string text, Vector2 position){
		label.Text = text;
		animationPlayer.Play("fade_scale_in_out");
		var tween = CreateTween();
		var endPosition = new Vector2(
			new Random().Next((int)-spread, (int)spread),
			-height
		) + position;
		var duration = animationPlayer.GetAnimation("fade_scale_in_out").Length;
		tween.TweenProperty(
			container,
			"position",
			endPosition,
			duration
		)
			.From(position);
	}
}
