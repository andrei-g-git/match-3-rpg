using System;
using Godot;

public partial class Defend : Node, Defensive.Model{
    private int health = 10; //these shouldn't init with values OR have editor exports! STATS SHOULD LOAD FROM DISK DATA!
    public int Health => health;
    private int defense = 1;
    public int Defense => defense;
    private Tiles.Model model;

    [Signal]
    public delegate void TookDamageEventHandler(Vector2I direction);

    public override void _Ready(){
        model = GetNode<Node>("%Model") as Tiles.Model;
    }

    public void TakeDamage(int damage){
        health -= Math.Max(0, damage - defense); //shouldd have a mode complex damage calc algorithm but this is fine for now
        var bp =234;
        EmitSignal(SignalName.TookDamage, model.Position);
    }

    public void ConnectTookDamage(Action<Vector2I> action){
        //TookDamage += action;
        Connect(SignalName.TookDamage, Callable.From(action));
    }
}