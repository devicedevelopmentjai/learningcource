namespace SOLID
{
    //Before
    public class Apple
    {
        public virtual string GetColor() => "Red";
    }
    public class Orange:Apple
    {
        public override string GetColor() => "Orange";
    }

    // After
    public abstract class Fruit
    {
        public abstract string GetColor();
    }
    public class AppleAfter: Fruit
    {
        public override string GetColor() => "Red";
    }
    public class OrangeAfter:Fruit
    {
        public override string GetColor() => "Orange";
    }

    public class LiskovSubstitutionPrincipleBeforeAndAfter
    {
        public void GetToKnow()
        {
            Apple apple = new Apple();
            apple.GetColor();
            apple = new Orange();
            apple.GetColor();

            // After
            Fruit appleAfter = new OrangeAfter();
            appleAfter.GetColor();
            appleAfter = new AppleAfter();
            appleAfter.GetColor();
        }
    }
}