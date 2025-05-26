
using Abstractions;
using Godot;

namespace Tiles
{
    public partial class Controller : Node/* , Listenable */
    {
        [Export] 
        private int dragTreshold = 16;
        // [Export]
        // private Node model;
        // public Node Model {set => model = value;} //should not need...
        //[Export]
        //private Node tileNode;
        public Node SignalEmitter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public StringName Signal { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        //private Swapable.Model model = null;
        private Vector2 dragDirection = Vector2.Zero;
        private Vector2 totalDrag = Vector2.Zero;
        private bool pressing = false;
        private bool pressed = false;
        [Signal]
        public delegate void TriedSwappingEventHandler(Vector2I direction); //NEW
        public override void _Ready(){
            // var modelScene = GetNode<Node>("%Model");

            // model = modelScene as Swapable.Model;

            var tileNode = GetParent() as Node;
            tileNode.Connect(BaseButton.SignalName.Pressed, Callable.From(OnPressed));
            tileNode.Connect(BaseButton.SignalName.ButtonUp, Callable.From(OnReleased));
            tileNode.Connect(BaseButton.SignalName.ButtonDown, Callable.From(OnPressing));
        }

        public override void _Input(InputEvent event_){
            base._Input(event_);

            if (event_ is InputEventMouseMotion move){
                if (pressing){
                    var delta = move.Relative;
                    totalDrag += delta;

                    if (
                        totalDrag.Y >= -dragTreshold &&
                        totalDrag.Y <= dragTreshold
                    ){
                        if (totalDrag.X >= dragTreshold){
                            dragDirection = Vector2I.Down;
                        }
                        if (totalDrag.X <= -dragTreshold){
                            dragDirection = Vector2I.Up;
                        }
                    }
                    if (
                        totalDrag.X >= -dragTreshold &&
                        totalDrag.X <= dragTreshold
                    ){
                        if (totalDrag.Y >= dragTreshold){
                            dragDirection = Vector2I.Right;
                        }
                        if (totalDrag.Y <= -dragTreshold){
                            dragDirection = Vector2I.Left;
                        }
                    }
                }
            }
        }
        
        private void OnPressed(){
            pressed = true;
            totalDrag = Vector2I.Zero;
        }

        private void OnReleased(){
            pressing = false;
            pressed = false;
            totalDrag = Vector2I.Zero;

            //var test = model as Tiles.Model;
            //((Swapable.Model) model).NotifySwap(new Vector2I((int)dragDirection.X, (int)dragDirection.Y));
            EmitSignal(SignalName.TriedSwapping, new Vector2I((int)dragDirection.X, (int)dragDirection.Y));
            
            dragDirection = Vector2I.Zero;
        }

        private void OnPressing(){
            pressing = true;
        }

    }
}
