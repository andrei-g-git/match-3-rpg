using Godot;

public static class MathUtilities{

    public static Vector2 InvertVector(Vector2 old){
        float x = old.Y;
        float y = old.X;
        return new Vector2(x, y);
    }
}