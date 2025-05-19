namespace Defensive{
    public interface Model{
        public int Health{get;}
        public int Defense{get;}
        public void TakeDamage(int damage);
    }    
}