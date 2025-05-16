using Godot;

namespace Tiles {
    namespace Unlock{
        public partial class Model : Tiles.Model {
            private Vector2I position;
            public override string Name => "unlock";
            public Model(Vector2I position) : base(position) {

            }

        }        
    }

}



