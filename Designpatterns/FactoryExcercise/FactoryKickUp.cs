using System;

namespace FactoryExcercise
{
    public class FactoryKickUp
    {
        public static void Main()
        {
            var cartesianPoint = Point.CreateInstanceForCartesian(12, 5);
            Console.WriteLine(cartesianPoint.ToString());
            var polarPoint = Point.CreateInstanceForPolar(13.0, 22.6 * Math.PI / 180);
            // Here I used radian instead of degree 1 degree = Math.PI/180
            Console.WriteLine(polarPoint.ToString());
        }
    }
}