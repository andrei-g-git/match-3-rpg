using Constants;
using Godot;

namespace Tiles {
    namespace Stamina{
        public partial class Model : Tiles.Model {
            private Vector2I position;
            public override string Name => "stamina";
            public override NamableTile Type => TileName.Stamina;
            // public Model(Vector2I position) : base(position) {

            // }

        }        
    }

}


