namespace Obtainable{
    public interface Model{
        public bool IsObtainable {get;}
        public void ObtainFor(Tiles.Model actor);
    }
}