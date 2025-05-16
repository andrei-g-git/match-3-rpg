using Godot;

// [Signal] 
// public delegate void RefreshBoardEventHandler();
public interface BoardRefreshing{
    //event RefreshBoardEventHandler RefreshBoard;
    //RefreshBoardEventHandler RefreshBoard {get; set;}
    public void OnTileActionFinished();
}