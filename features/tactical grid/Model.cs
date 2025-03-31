using System;
using Abstractions;
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
        private Array<Array<Control>> observers = new Array<Array<Control>>();  
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
        public void Initialize(){
            rows = tileNameMatrix.Count;
            columns = tileNameMatrix[0].Count;
            observers.Resize(rows);
            foreach(Array<Control> observer in observers){
                observer.Resize(columns);
            }
        }
        public void CreateGrid() {

            grid.Resize(rows);
            for (int x = 0; x < rows; x++) {
                grid[x].Resize(columns);
                for(int y = 0; y < columns; y++) {
                    grid[x][y] = tileFactory.Create(tileNameMatrix[x][y], new Vector2I(x, y));
                }
            }
        }

        public void Register(Control tileNode_, int x_, int y_) {
            var observer = observers[x_][y_] = tileNode_;
            var tileModel = ((Modelable) observer).Model;
            //((Tiles.Model)tileModel).Connect()
            //this.Connect(Tiles.Model.SignalName.TriedSwapping, Callable.From(Swap2)); //this isn't great...
            //Tiles.Model.SignalName.TriedSwapping += (Vector2I source, Vector2I direction) => {Swap2(source, direction);};
            //this.Connect("TriedSwapping", this)
            //((Tiles.Model)tileModel).Connect(Tiles.Model.SignalName.TriedSwapping, Callable.From())
            //tileModel.Connect(Tiles.Model.SignalName.TriedSwapping, )
            ((Tiles.Model)tileModel).TriedSwapping += (Vector2I source, Vector2I direction) => Swap2(source, direction); //Jesus Christ what an ordeal
        }


        private void Swap2(Vector2I source, Vector2I direction){
            Console.WriteLine("source and direction below \n");
            Console.WriteLine("source:  ", source.X, "   ", source.Y);
            Console.WriteLine("direction:  ", direction.X, "   ", direction.Y);
        }
    }

}

