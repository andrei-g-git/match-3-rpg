using Godot;

public partial class Movement : Node, Movable.Model{
    [Export]
    private Node swapper; //matcher;   
    [Export]
    private Node model;
    private Grid.Model board;
    public Grid.Model Board { set => board = value; }    
    [Signal]
    public delegate void TryFightingEventHandler(Tiles.Model target);

    public override void _Ready(){
        //(matcher as Match).TryMoving += Move;
        (swapper as Swapping).TryMoving += Move;
    }     
    public void Move(Vector2I source, Vector2I direction){
        var destination = source + direction;
        var targetTile = board.GetTileModel(destination);
        //assume that only the player can move... this might cause problems
        if(targetTile is Obtainable.Model wefwef){
            //move over and obtain -- if it's a walk tile then get nothing or decrease stamina -- but then other actions should decrease stamina too
        }else{
            if(targetTile is Hostility.Model dfdg){
                if((targetTile as Hostility.Model).IsEnemy){
                    //attack
                    //board.Fight(model as Tiles.Model, targetTile); no, this should send signal to offense behavior
                    EmitSignal(SignalName.TryFighting, targetTile);
                }
            }else{ //wait these should also be separate behaviors...
                /* 
                    if it's a chest or door
                        remove tile
                            if it's a chest
                                replace with contents (could be reward or something perilous)
                    else
                        if it's a friend
                            open dialog/trade UI
                        else
                            if it's a switch
                                operate it with custom effects
                 */
            }
        }
    }
}