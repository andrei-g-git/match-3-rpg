using Abstractions;
using Godot;
using System;

namespace Health{
    public partial class Manager : TileNode{
        public override void _Ready()
        {
            base._Ready();

                var matching = GetNode("%Matching") as Updating.Model;
                //var model = (GetNode<Node>("%TacticalGrid") as TacticalGrid).Model;
                var model = (GetParent().GetParent() as TacticalGrid).Model;
                matching.ConnectUpdate(model.Notify);
        }

    }    
}

