namespace Constants{ //shouldd get rid of this and use records
    public enum TileNames{
        Archer , //"archer" ,
        Chest, //"chest",
        Defend, //"defend" ,
        Energy, //"energy" ,
        Fighter, //"fighter" =,
        Health, //"health" ,
        Melee, //"melee",
        Player, //"player" ,
        Ranged, //"ranged" ,
        Stamina, //"stamina" ,
        Unlock, //"unlock" ,
        Walk, //"walk"
    }

    public interface NamableTile{
        string Value {get;}
    }

    public record TileName(string Value): NamableTile{
        public static readonly TileName Archer  = new TileName("archer");
        public static readonly TileName Chest = new TileName("chest");
        public static readonly TileName Defend = new TileName("defend");
        public static readonly TileName Energy = new TileName("energy");
        public static readonly TileName Fighter = new TileName("fighter");
        public static readonly TileName Health = new TileName("health");
        public static readonly TileName Melee = new TileName("melee");
        public static readonly TileName Player = new TileName("player");
        public static readonly TileName Ranged = new TileName("ranged");
        public static readonly TileName Stamina = new TileName("stamina");
        public static readonly TileName Unlock = new TileName("unlock");
        public static readonly TileName Walk = new TileName("walk");
    }

}