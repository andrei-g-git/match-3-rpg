using Godot;
using Godot.Collections;

namespace Tiles {
    public partial class Player : Tile, Movable, Transportable.Model{
        private Vector2I position;
        public override string Name => "player";
        private Movable moveBehavior = null;
        public Movable MoveBehavior {get => moveBehavior; set => moveBehavior = value;}
        private Transportable.Model transportBehavior = null;
        public Transportable.Model TransportBehavior{get => transportBehavior; set => transportBehavior = value;}
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

        public void NotifyTransport(Vector2I target){
            transportBehavior.NotifyTransport(target);
        }

    }
}


