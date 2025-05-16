using Godot;

public partial class SwapAnimator : Node, Swapable.View, Box, BoardRefreshing{
    [Export] float duration = 0.5f;
    [Signal] private delegate void RefreshBoardEventHandler();
    private int width = 64;
    public int Width { get => width; set => width = value; }
    private int height = 64;
    public int Height { get => height; set => height = value; }
    private int margin = 3;
    public int Margin { get => margin; set => margin = value; }
    private Node tileNode = null;

    //can't decorate w/ [Signal]
    //public event RefreshBoardEventHandler RefreshBoard;

    public override void _Ready(){
        //base._Ready();
        tileNode = GetParent() as Node;
    }
    public void SwapTo(Vector2I target){
		Vector2 reverseDestination = MathUtilities.InvertVector(target);
		Vector2I target_pixel_position = (Vector2I) reverseDestination * (width + margin);
		MoveTo(target_pixel_position, duration);
    }

	private void MoveTo(Vector2 target, float duration){
		Tween tween = CreateTween()
			.SetTrans(Tween.TransitionType.Elastic)
			.SetEase(Tween.EaseType.Out);
		tween.TweenProperty(tileNode, "position", target, duration);
		//queue free deletes the whole node including the Control, not just the tween
		tween.Finished += OnTileActionFinished;
		GD.Print("MOVED");
	}

    // private void OnMoveFinished(){
	// 	EmitSignal(SignalName.RefreshBoard);
	// }

    public void OnTileActionFinished(){
        EmitSignal(SignalName.RefreshBoard);throw new System.NotImplementedException();
    }
}