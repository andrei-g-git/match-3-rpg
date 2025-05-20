using Godot;

public partial class Movement : Node, Movable.Model{
    [Export]
    private Node matcher;   
    private Grid.Model board;
    public Grid.Model Board { set => board = value; }    
    public override void _Ready(){
        (matcher as Match).TryMoving += Move;
    }     
    public void Move(Vector2I source, Vector2I direction){
        throw new System.NotImplementedException();
    }
}