public interface Effectful{

    public void ApplyEffects(object subject); //iterate all the properties for both this and the subject somehow and add/subract or change bools if the property exists on both
}