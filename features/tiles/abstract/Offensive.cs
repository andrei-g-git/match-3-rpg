namespace Offensive{
    public interface Model{
        public int Damage { get; }
        public void Attack(Tiles.Model actor);
    }    
}
