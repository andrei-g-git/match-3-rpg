using System;
using Constants;
using Godot;

namespace Replaceable{
    public interface Model{

    }
    public interface Controller{
        //public Replaceable.Controller ReplaceController { get; }
        public Replaceable.Controller GetReplaceController();
        public bool _CanDropData(Vector2 atPosition, Variant data);
        public void _DropData(Vector2 atPosition, Variant data);
        // public void ConnectCanDropData(Action<Vector2, Variant> action);
        // public void ConnectDropData(Action<Vector2, Variant> action);
        public void ConnectReplacingTile(Action<int> action);
    }
}