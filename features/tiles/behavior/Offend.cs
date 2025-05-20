using Godot;
using Tiles;

public partial class Offend : Node, Offensive.Model
{
    private int damage = 3;
    public int Damage => damage;

    int Offensive.Model.Damage { get => Damage; }

    public void Attack(Model actor){
        (actor as Defensive.Model).TakeDamage(damage); //the damage should be calculated form a formula
    }
}