using Abstractions;
using Godot;
using Godot.Collections;
using System;
//using System.Collections.Generic;
using System.Linq;
using Tiles;

namespace Grid {
    public partial class Factory: Node{
        private Dictionary<string, PackedScene> preInstantiatedTiles = new Dictionary<string, PackedScene>();
        private Array<string> tileNames = new Array<string>();//private List<string> tileNames;
        private Array<PackedScene> tileScenes;//private List<PackedScene> tileScenes;
        //private Tiles.Factory tileFactory = null;
        public Factory(Array<PackedScene> tileScenes_/* , Tiles.Factory tileFactory_ */) {
            tileScenes = tileScenes_;
            //tileFactory = tileFactory_;
            var bp = 123;
        }

        public void Initialize() { //this should be called from director type class
            //List<Control> tileNodes = (List<Control>) tileScenes.Select(scene => scene.Instantiate());
            //tileNames = (List<string>)tileNodes.Select(node => node.Name);

            //var result = tileScenes.Select(scene => scene.Instantiate() as Control);
            //var tileNodes = new Array<Control>(result);
            var tileNodes = new Array<Control>();
            foreach(PackedScene scene in tileScenes){
                tileNodes.Add(scene.Instantiate() as Control);
            }
            //tileNames = (Array<string>) tileNodes.Select(node => node.Name.ToString());
            foreach(Control node in tileNodes){
                tileNames.Add(((string) node.Name).ToLower());
            }
            for(int i = 0; i < tileNames.Count; i++) {
                preInstantiatedTiles.Add(tileNames[i], tileScenes[i]);
            }
            var bp = 123;
        }

        public Control Create(string tileName, Tile model, Vector2I position) { //I don't like that I'm exposing this factory to the tile model when I already have a tile model factory
            var tileNode =  (Control) preInstantiatedTiles[tileName].Instantiate();
            ((/* Modelable */Controllable) tileNode).Model = model;//tileFactory.Create(tileName, position);
            return tileNode;
        }
    }
}
