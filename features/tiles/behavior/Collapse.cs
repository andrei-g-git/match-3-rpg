using Constants;
using Godot;
using Godot.Collections;

public partial class Collapse : Node, Collapsable {
    private Array<TileNames> transportables = [];
    public Array<TileNames> Transportables {get => transportables;}

    public void AddTransportable(TileNames tile){
        transportables.Add(tile);
    }
}