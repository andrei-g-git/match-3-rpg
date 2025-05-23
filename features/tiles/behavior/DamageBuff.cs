using Godot;

public partial class DamageBuff : Node, BuffableDamage.Model, Behavioral, Listenable{   
    [Export]
    private int meleeBuff = 0;
    public int MeleeBuff {get => meleeBuff; /* set => meleeBuff = value; */}   
    [Export]
    private int rangedBuff = 0;
    public int RangedBuff {get => rangedBuff; /* set => rangedBuff = value; */}   
    [Export]
    private int spellBuff = 0;
    public int SpellBuff {get => spellBuff; /* set => spellBuff = value; */}
    public Node SignalEmitter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public StringName Signal { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private Swapping swapper;

    public override void _Ready(){
        swapper = GetNode<Node>("%Swapper") as Swapping;
        //it makes this connection before the swapped tile even sends the matchEmitter reference...
        //(swapper.MatchEmitter as Match).StartedCollapse += Foo; //not great, not terrible
    }
    public void Connect(Node emitter){
        ((Match) emitter).StartedCollapse += Foo;
    }

    public void Foo(){
        var board = swapper.Board;
        var player = board.GetPlayer() as BuffableDamage.Model;
        player.IncreaseDamageOfMelee(meleeBuff);
        player.IncreaseDamageOfRanged(rangedBuff);
        player.IncreaseDamageOfSpell(spellBuff);
    }

    public void IncreaseDamageOfMelee(int damageIncrement){
        meleeBuff += damageIncrement;
    }

    public void IncreaseDamageOfRanged(int damageIncrement){
        rangedBuff += damageIncrement;
    }

    public void IncreaseDamageOfSpell(int damageIncrement){
        spellBuff += damageIncrement;
    }

    public void Fulfill(){
        Foo();
    }


}