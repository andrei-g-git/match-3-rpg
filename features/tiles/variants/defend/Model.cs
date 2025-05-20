using Constants;
using Godot;

namespace Tiles {
    namespace Defend{
        public partial class Model : Tiles.Model {
            private Vector2I position;
            public override string Name => "defend";
            //public override NamableTile Type => TileName.Defend;
            public Model(/* Vector2I position */) : base(/* position */) {

            }

        }        
    }

}


