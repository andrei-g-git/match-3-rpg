using Abstractions;
using Constants;
using Godot;
using Godot.Collections;
using Transfering;

//THIS NEEDS TO SIT BELOW EVERY TILE MATCH BEHAVIOR IN THE SCENE BRANCH! this is to make sure other tile matching behaviors run before this prompts the player to act and run it's animation
public partial class Transfer : Node, Transfering.Model, Behavioral{
    [field: Export]
    public Collections.FixedSizeArray<TileNames> Transferables { get; set; }
    private Swapping swapper;
    private Tiles.Model thisModel;
    [Signal] 
    public delegate void TransferingActorEventHandler(Vector2I target);
    public override void _Ready(){
        //should get a bool checking if it's the player's turn to decide which transferable has priority (of course this means it can only store 2, one of them being the player... so not ideal)
        swapper = GetNode<Node>("%Swapper") as Swapping;
        thisModel = GetNode<Node>("%Model") as Tiles.Model;
        //it makes this connection before the swapped tile even sends the matchEmitter reference...
        //(swapper.MatchEmitter as Match).StartedCollapse += Foo; //not great, not terrible        
    }

    public async void TransferTile(){
        //if player turn:
        //bypass the transferables part now but it's nice to have on the backburner
        var board = swapper.Board;
        var playerNode = board.GetActor(TileNames.Player);
        var pos = thisModel.Position;
        board.SetTile(playerNode, pos.X, pos.Y); //I'm exposing too much of the tilegrid's implementation...

        EmitSignal(SignalName.TransferingActor, pos);

        var emitter = ((playerNode as Animatable).Animators as Player.Animators).TransportAnimator as Node; //he knows too much, we gotta "take care" of him
        await ToSignal(emitter, "Transported"); 
    }

    public void Fulfill(){
        TransferTile();
    }
}