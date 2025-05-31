using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace Grid{
    public interface Viewable{
        //public Task Update<[MustBeVariant]T>(Array<Array<T>> tiles) where T : Node;
        //public Task Update(Array<Array</* Tile */Node>> tiles);
        public void Update(Array<Array<Node>> tiles);
    }
}