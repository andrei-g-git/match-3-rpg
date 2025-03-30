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
        public Factory(Array<PackedScene> tileScenes_) {
            tileScenes = tileScenes_;
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

        public Control Create(string tileName) {
            return (Control) preInstantiatedTiles[tileName].Instantiate();
        }
    }
}
