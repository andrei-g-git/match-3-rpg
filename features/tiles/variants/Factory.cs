using Constants;
using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Tiles;



namespace Tiles {
	public partial class Factory : Node {
		private Dictionary<string, Tiles.Model> tileVariants = new Dictionary<string, Tiles.Model>();

        public Factory() {

        }

		public Tiles.Model Create(string name_, Vector2I position_) {
			Tiles.Model tile = null;

            return name_ switch { //apparently this is the new c# switch statement... I wonder how I can add more expressions per case...
                "archer" => CreateArcher(position_),
                "chest" => CreateChest(position_),
                "defend" => CreateDefend(position_),
                "energy" => CreateEnergy(position_),
                "fighter" => CreateFighter(position_),
                "health" => CreateHealth(position_),
                "melee" => CreateMelee(position_),
                "player" => CreatePlayer(position_),
                "ranged" => CreateRanged(position_),
                "stamina" => CreateStamina(position_),
                "unlock" => CreateUnlock(position_),
                "walk" => CreateWalk(position_),
                _ => null,
            };
        }

        private Tiles.Model CreateArcher(Vector2I position){
            var tile = new Archer(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            //tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private Tiles.Model CreateChest(Vector2I position){
            var tile = new Chest(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tiles.Model CreateDefend(Vector2I position){
            var tile = new Defend(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            //tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private Tiles.Model CreateEnergy(Vector2I position){
            var tile = new Energy(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            //tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private Tiles.Model CreateFighter(Vector2I position){
            var tile = new Fighter(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tiles.Model CreateHealth(Vector2I position){
            var tile = new Health(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            //tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private Tiles.Model CreateMelee(Vector2I position){
            var tile = new Melee(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            //tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private Tiles.Model CreatePlayer(Vector2I position){
            var tile = new Player(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.MoveBehavior = new Movement();
            tile.AddTiles([
                TileNames.Defend.ToString().ToLower(),
                TileNames.Melee.ToString().ToLower(),
                TileNames.Ranged.ToString().ToLower(),
                TileNames.Walk.ToString().ToLower(),
            ]);
            tile.TransportBehavior = new Transport();
            tile.DamageBuffBehavior = new DamageBuff();
            return tile;
        }
        private Tiles.Model CreateRanged(Vector2I position){
            var tile = new Ranged(position);
            tile.SwapBehavior = new Swapping(tile);
            return tile;
        }
        private Tiles.Model CreateStamina(Vector2I position){
            var tile = new Stamina(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            //tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private Tiles.Model CreateUnlock(Vector2I position){
            var tile = new Unlock(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            //tile.AddTransportable(TileNames.Player);
            return tile;
        }
        private Tiles.Model CreateWalk(Vector2I position){
            var tile = new Walk(position);
            tile.SwapBehavior = new Swapping(tile);
            tile.CollapseBehavior = new Collapse();
            //tile.AddTransportable(TileNames.Player);
            return tile;
        }                                                                                        
	}
}




//tileVariants.Add("archer", Archer);
//tileVariants.Add("chest", Chest);
//tileVariants.Add("defend", Defend);
//tileVariants.Add("energy", Energy);
//tileVariants.Add("fighter", Fighter);
//tileVariants.Add("health", Health);
//tileVariants.Add("melee", Melee);
//tileVariants.Add("player", Player);
//tileVariants.Add("ranged", Ranged);
//tileVariants.Add("stamina", Stamina);
//tileVariants.Add("unlock", Unlock);
//tileVariants.Add("walk", Walk);

