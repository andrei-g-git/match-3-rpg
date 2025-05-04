using Godot;
using Godot.Collections;

namespace Tiles {
    public partial class Player : Tile, Movable {
        private Vector2I position;
        public override string Name => "player";
        private Movable moveBehavior = null;
        public Movable MoveBehavior {get => moveBehavior; set => moveBehavior = value;}
        private Array<string> shortMoveTiles = [];
        public Array<string> ShortMoveTiles {get => shortMoveTiles;}


        public Player(Vector2I position) : base(position) {

        }

        public bool VerifyShortMoveEligibility(string tileName){
            return moveBehavior.VerifyShortMoveEligibility(tileName);
        }

        public void AddTile(string tileName){
            moveBehavior.AddTile(tileName);
        }
        public void AddTiles(string[] tiles){
            moveBehavior.AddTiles(tiles);
        }
        public void RemoveTile(string tileName){
            moveBehavior.RemoveTile(tileName);
        }
    }
}


