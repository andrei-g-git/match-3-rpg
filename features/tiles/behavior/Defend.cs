using System;
using Godot;

public partial class Defend : Node, Defensive.Model{
    [Export]
    private int health;// = 10; //these shouldn't init with values OR have editor exports! STATS SHOULD LOAD FROM DISK DATA!
    [Export]
    private int maxHealth;
    [Export]
    private int defense;
    public int Health => health;
    public int MaxHealth => maxHealth;
    public int Defense => defense;
    private Tiles.Model model;

    [Signal]
    public delegate void TookDamageEventHandler(Vector2I direction, int amount);

    public override void _Ready(){
        model = GetNode<Node>("%Model") as Tiles.Model;
    }

    public void TakeDamage(int damage){
        health -= Math.Max(0, damage - defense); //shouldd have a mode complex damage calc algorithm but this is fine for now
        var bp =234;
        EmitSignal(SignalName.TookDamage, model.Position, damage - defense);
    }

    public void ConnectTookDamage(Action<Vector2I, int> action){
        //TookDamage += action;
        Connect(SignalName.TookDamage, Callable.From(action));
    }

    public void ReceiveHealing(int amount){
        health += amount;
        if(health > maxHealth) health = maxHealth;
    }
}