using Godot;
using Godot.Collections;
using Tiles;

namespace Grid {
    public partial class Model : Node {
        private int columns = 0;
        private int rows = 0;
        //private List<List<string>> tileNameMatrix = new List<List<string>>();
        private Array<Array<string>> tileNameMatrix = new Array<Array<string>>();
        //private List<List<Tile>> grid = new List<List<Tile>>();
        private Array<Array<Tile>> grid = new Array<Array<Tile>>();
        //private List<PackedScene> tileResources;
        //private PackedScene[] tileResources;
        private Array<PackedScene> tileResources;
        private Tiles.Factory tileFactory;
        //private List<List<Control>> observers;
        private Array<Array<Control>> observers;  
        //public List<List<Tile>> Grid { get; }
        public Array<Array<Tile>> Grid { get => grid; }
        public Model(
            //int columns_,
            //int rows_, 
            //List<PackedScene> tileResources_, 
            Array<PackedScene> tileResources_, //HUHH???? Ce fac cu astea??
            //List<List<string>> tileNameMatrix_,
            Array<Array<string>> tileNameMatrix_,
            Tiles.Factory tileFactory_
        ){
            //columns = columns_;
            //rows = rows_;

            tileResources = tileResources_;//.ToArray().Select(item => (PackedScene) item).ToArray();
            tileNameMatrix = tileNameMatrix_;
            tileFactory = tileFactory_;
            //Console.WriteLine("tile resources:  ", tileResources_);
        }
        public void CreateGrid() {
            rows = tileNameMatrix.Count;
            columns = tileNameMatrix[0].Count;
            grid.Resize(rows);
            for (int x = 0; x < rows; x++) {
                grid[x].Resize(columns);
                for(int y = 0; y < columns; y++) {
                    grid[x][y] = tileFactory.Create(tileNameMatrix[x][y], new Vector2I(x, y));
                }
            }
        }

        public void Register(Control tileNode_, int x_, int y_) {
            observers[x_][y_] = tileNode_;
        }



    }

}

