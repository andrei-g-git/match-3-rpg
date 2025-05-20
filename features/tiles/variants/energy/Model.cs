using Constants;
using Godot;

namespace Tiles {
    namespace Energy{
        public partial class Model : Tiles.Model {
            private Vector2I position;
            public override string Name => "energy";
            //public override NamableTile Type => TileName.Energy;
            public Model(/* Vector2I position */) : base(/* position */) {

            }
        }        
    }

}


