using Constants;
using Godot.Collections;

public interface Collapsable{
    public Array<TileNames> Transportables {get;}
    public void AddTransportable(TileNames tile);
}