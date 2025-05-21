using System;
using Godot;

public partial class Defend : Node, Defensive.Model
{
    private int health = 10; //these shouldn't init with values OR have editor exports! STATS SHOULD LOAD FROM DISK DATA!
    public int Health => health;
    private int defense = 1;
    public int Defense => defense;

    public void TakeDamage(int damage){
        health -= Math.Max(0, damage - defense); //shouldd have a mode complex damage calc algorithm but this is fine for now
        var bp =234;
    }
}