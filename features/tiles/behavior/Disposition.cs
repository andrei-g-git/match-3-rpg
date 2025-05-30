using Godot;

public partial class Disposition : Node, Hostility.Model{
    [Export]
    private bool isAggressive;
    [Export]
    public bool isEnemy;    
    public bool IsAggressive { 
        get => isAggressive; 
        set { 
            isAggressive = isEnemy && value; 
            isEnemy = isAggressive? isEnemy : false;
        } 
    }
    public bool IsEnemy { 
        get => isEnemy; 
        set {
            isEnemy = value;
            isAggressive = value? value : false;
        }
    }
}