using Godot;

namespace Tiles {
    namespace Ranged{
        public partial class Model : Tiles.Model {
            private Vector2I position;
            public override string Name => "ranged";
            public Model(Vector2I position) : base(position) {

            }

        }        
    }

}



