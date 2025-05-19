using Constants;
using Godot;

namespace Tiles {
    namespace Chest{
        public partial class Model : Tiles.Model {
            private Vector2I position;
                        public override NamableTile Type => TileName.Chest;
            public Model(/* Vector2I position */) : base(/* position */) {

            }

        }        
    }

}



