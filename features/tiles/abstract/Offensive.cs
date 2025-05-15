public interface Offensive{
    public int Damage { get; set; }
    public void ApplyDamageBuff(/* Combatable.Model actor */object foo); // += buff to actor's damage depending on whether tile is spell, ranged or damage buff
}