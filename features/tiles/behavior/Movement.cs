using System;
using Godot;

public partial class Movement : Node, Movable.Model{
    // [Export]
    // private Node swapper; //matcher;   
    // [Export]
    // private Node model;
    private Grid.Model board;
    public Grid.Model Board { set => board = value; }
    [Export]
    private int stamina;
    public int Stamina {get => stamina; set => stamina = value;}

    [Signal]
    public delegate void TryFightingEventHandler(Tiles.Model target);
    [Signal]
    public delegate void TookStepEventHandler(Vector2I direction);

    public override void _Ready(){
        //(matcher as Match).TryMoving += Move;
        var swapper = GetNode<Node>("%Swapper");
        (swapper as Swapping).TryMoving += Move;
        //board = (GetNode<Node>("%Model") as GridItem).Board; //doesn't have it at this point....
        var bp = 324;
    }     

    public void TakeAStep(Vector2I target){
        var model = GetNode<Node>("%Model") as Tiles.Model;
        var source = model.Position;
        board = model.Board;
        //test\
        board.PrintGridInitials("TAKING A STEP:  BOARD:  ");
        var targetModel = board.JustSwap(source, target) as Walkable.Model; //too much exposure to other modules' implementation
        stamina -= targetModel.StaminaCost;
        if(stamina < 0) stamina = 0;
        var bp = 3452;
    }
    public void Move(Vector2I target/* source, Vector2I direction */){
        var destination = target;//source + direction;
        board = (GetNode<Node>("%Model") as GridItem).Board;
        var targetTile = board.GetTileModel(destination);
        //assume that only the player can move... this might cause problems
        if(targetTile is Walkable.Model){ //THIS DOESN'T WORK because the actor already changed position and will itself appear as the target
            TakeAStep(target);
            EmitSignal(SignalName.TookStep, target);
        }else{
            if(targetTile is Obtainable.Model /* wefwef */){
                //move over and obtain -- if it's a walk tile then get nothing or decrease stamina -- but then other actions should decrease stamina too
                var bp =345;
            }else{
                if(targetTile is Hostility.Model /* dfdg */){
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

    public void ConnectTookStep(Action<Vector2I> action){
        Connect(SignalName.TookStep, Callable.From(action));
    }
}