using Godot;

public static class MathUtilities{

    public static Vector2 InvertVector(Vector2 old){
        float x = old.Y;
        float y = old.X;
        return new Vector2(x, y);
    }

    public static bool CheckNegativeVectorAxes(Vector2 vec){
        return vec.X >= 0 && vec.Y >= 0;
    }
}