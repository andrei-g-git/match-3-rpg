using Godot;
using Tiles;

public partial class Offend : Node, Offensive.Model
{
    // [Export]
    // private Node signalEmitter;
    private int damage = 3;
    public int Damage => damage;
    int Offensive.Model.Damage { get => Damage; }
    private Tiles.Model model;
    [Signal]
    public delegate void AttackedEventHandler(Vector2I source, Vector2I target);

    public override void _Ready(){
        var signalEmitter = GetNode<Node>("%Movement");
        (signalEmitter as Movement).TryFighting += Attack; //casting to class types instead of interfaces will come back to bite me in the ass but I can't define signals in the interface...
        model = GetNode<Node>("%Model") as Tiles.Model;
    }

    public void Attack(Model actor){
        if(actor is Hostility.Model dfdg){
            if((actor as Hostility.Model).IsEnemy){
                (actor as Defensive.Model).TakeDamage(damage); //the damage should be calculated form a formula     (should also add buff)     
                //assume actor is adjacent
                var source = model.Position;
                var target = (actor as Tiles.Model).Position;
                // var direction = target - source; //well this is useless, I already have the target position

                EmitSignal(SignalName.Attacked, source, target);      
            }
        }

    }
}