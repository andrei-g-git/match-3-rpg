using Godot;

namespace Tiles {
    namespace Walk{
        public partial class Model : Tiles.Model {
            private Vector2I position;
            public override string Name => "walk";
            public Model(Vector2I position) : base(position) {

            }

        }        
    }

}


