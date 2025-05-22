using Godot;

public partial class DamageBuff : Node, BuffableDamage.Model{   
    [Export]
    private int meleeBuff = 0;
    public int MeleeBuff {get => meleeBuff; /* set => meleeBuff = value; */}   
    [Export]
    private int rangedBuff = 0;
    public int RangedBuff {get => rangedBuff; /* set => rangedBuff = value; */}   
    [Export]
    private int spellBuff = 0;
    public int SpellBuff {get => spellBuff; /* set => spellBuff = value; */}
    private Swapping swapper;

    public override void _Ready(){
        swapper = GetNode<Node>("%Swapper") as Swapping;
        (swapper.MatchEmitter as Match).StartedCollapse += Foo; //not great, not terrible
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
}