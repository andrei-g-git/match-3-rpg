using System;
using Godot;

namespace Haulable{
    public interface Model{
        public void AssessSurroundings(Vector2I position);
        public void ConnectTryFighting(Action<Tiles.Model> action); 
    }

    public interface View{

    }
}