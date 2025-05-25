using Godot;
using Tiles;

public partial class Healing : Node, Healable.Model, Consumable.Model, Listenable{ //need Listenable to connect emitter to matches, but it's bad because I already have another standardized way of connecting signals to actions...
    [Export]
    private int healAmount;
    [Export]
    private int healAmountOnMatch;
    public int HealAmount => healAmount;

    public Node SignalEmitter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); } //probably don't need these...
    public StringName Signal { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int HealAmountOnMatch => healAmountOnMatch;

    public void Connect(Node emitter){
        ((Match) emitter).StartedCollapse += HealPlayerOnMatch;
    }

    public void ConsumeFor(Model actor)
    {
        throw new System.NotImplementedException();
    }

    public void HealPlayer(Defensive.Model player){
        player.ReceiveHealing(healAmount);
    }

    public void HealPlayerOnMatch(){
        var board = (GetNode<Node>("%Model") as Tiles.Model).Board;
        var player = board.GetPlayer() as Defensive.Model;
        player.ReceiveHealing(healAmountOnMatch);
        var bp = 123;
    }
}