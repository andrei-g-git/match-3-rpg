namespace Healable{
    public interface Model{
        public int HealAmount{get;}
        public void HealPlayer(Defensive.Model player);
    }
}