using System;

namespace Constants{ //shouldd get rid of this and use records
    public enum TileNames{
        Archer , 
        Barrel, //NEW
        Chair, //NEW, now the initials matrix won't show unique tiles
        Chest, 
        Defend, 
        Energy, 
        Fighter, 
        Health, 
        Melee, 
        Player, 
        Ranged, 
        Stamina, 
        Table, //NEW
        Unlock, 
        Walk, 
        Wall
    }

    public interface NamableTile{
        string Value {get;}
    }

    public record TileName(string Value): NamableTile{
        public static NamableTile Create(string name){
            return name switch{
                "archer" => Archer,
                "chest" => Chest,
                "defend" => Defend,
                "energy" => Energy,
                "fighter" => Fighter,
                "health" => Health,
                "melee" => Melee,
                "player" => Player,
                "ranged" => Ranged,
                "stamina" => Stamina,
                "unlock" => Unlock,
                "walk" => Walk,  
                _ => throw new ArgumentException("Invalid Tile Name"),           
            };
        }
        public override string ToString() => Value;
        //public static implicit operator string(TileName type) => type.Value; //can now assign records to string variables

        public static readonly NamableTile Archer  = new TileName("archer");
        public static readonly NamableTile Chest = new TileName("chest");
        public static readonly NamableTile Defend = new TileName("defend");
        public static readonly NamableTile Energy = new TileName("energy");
        public static readonly NamableTile Fighter = new TileName("fighter");
        public static readonly NamableTile Health = new TileName("health");
        public static readonly NamableTile Melee = new TileName("melee");
        public static readonly NamableTile Player = new TileName("player");
        public static readonly NamableTile Ranged = new TileName("ranged");
        public static readonly NamableTile Stamina = new TileName("stamina");
        public static readonly NamableTile Unlock = new TileName("unlock");
        public static readonly NamableTile Walk = new TileName("walk");
    }

}