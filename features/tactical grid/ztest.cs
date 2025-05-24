using System;
using Godot;
namespace Ztest{
    public interface Inter{
        public void SomeMethod(string text);
    }
    public partial class Impl: Node, Inter{
        
        public void SomeMethod(string text){
            GD.Print("test string:   " + text);
        }
    }    

    public partial class Sender: Node{
        [Signal]
        public delegate void MySignalEventHandler(string text);

        public override void _Ready(){
            EmitSignal(SignalName.MySignal, "mehhhhhhhhhhhhhhhhh");
        }

        public void ConnectMySignal(Action<string> action){
            Connect(SignalName.MySignal, Callable.From(action));
        }
    }
}
