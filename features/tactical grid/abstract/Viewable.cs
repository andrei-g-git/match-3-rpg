using Godot;
using Godot.Collections;

namespace Grid{
    public interface Viewable{
        public void Update(Array<Array<Node>> tiles);
        public void ClearTiles();
    }
}