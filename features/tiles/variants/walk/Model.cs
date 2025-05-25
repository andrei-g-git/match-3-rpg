using Constants;
using Godot;

namespace Tiles {
    namespace Walk{
        public partial class Model : Tiles.Model, Walkable.Model {
            private Vector2I position;
            public override string Name => "walk";
            public Walkable.Model Walkway{get;set;}
            public int StaminaCost => Walkway.StaminaCost;

            //public override NamableTile Type => TileName.Walk;
            // public Model(Vector2I position) : base(position) {

            // }


        }        
    }

}


