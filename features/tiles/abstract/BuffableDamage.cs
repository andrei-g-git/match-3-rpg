namespace BuffableDamage{
    public interface Model{
        public int MeleeBuff{get; set;}
        public int RangedBuff{get; set;}
        public int SpellBuff{get; set;}

        public void IncreaseDamageOfMelee(int damageIncrement);
        public void IncreaseDamageOfRanged(int damageIncrement);
        public void IncreaseDamageOfSpell(int damageIncrement);
    }    
}
