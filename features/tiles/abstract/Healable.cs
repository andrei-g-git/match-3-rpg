namespace Healable{
    public interface Model{
        public int HealAmount{get;}
        public int HealAmountOnMatch {get;}
        public void HealPlayer(Defensive.Model player);
        public void HealPlayerOnMatch();
    }
}