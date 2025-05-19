using Constants;
using Godot;

namespace Tiles {
    namespace Archer{
        public partial class Model : Tiles.Model{
            private Vector2I position;
            public override string Name => "archer";
            //public override TileName Type => "archer";
            public Model(/* Vector2I position */): base(/* position */) {

            }

        }        
    }

}

