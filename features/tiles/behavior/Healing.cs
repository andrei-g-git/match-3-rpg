using Godot;

public partial class Healing : Node, Healable.Model{
    [Export]
    private int healAmount;
    public int HealAmount => healAmount;

    public void HealPlayer(Defensive.Model player){
        player.ReceiveHealing(healAmount);
    }
}