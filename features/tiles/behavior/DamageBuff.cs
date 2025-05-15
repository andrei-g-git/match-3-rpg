public class DamageBuff : BuffableDamage.Model{
    private int meleeBuff = 0;
    public int MeleeBuff {get => meleeBuff; set => meleeBuff = value;}
    private int rangedBuff = 0;
    public int RangedBuff {get => rangedBuff; set => rangedBuff = value;}
    private int spellBuff = 0;
    public int SpellBuff {get => spellBuff; set => spellBuff = value;}

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