using System;

namespace FactoryExcercise
{
    public class Point
    {
        

        private double x, y;
        #region Ex1
        /*public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }*/

        // We have polar cooridinate too
        /*public Point(double rho, double theta)
        {
            this.x = x * Math.Cos(y);
            this.y = x * Math.Sin(y);
        }*/
        // Error/Problem
        // Point already defines a member with same signature

        /**
         * Solution: We have to customize the Point type for an polar value
         * 1. Create decision making type, here I used enum and configured 
         */
        #endregion

        #region Ex1_Solution
        public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian)
        {
            if(system == CoordinateSystem.Polar)
            {
                this.x = x * Math.Cos(y);
                this.y = x * Math.Sin(y);
            }
            switch(system)
            {
                case CoordinateSystem.Cartesian:
                    this.x = a;
                    this.y = b;
                    break;
                case CoordinateSystem.Polar:
                    this.x = x * Math.Cos(y);
                    this.y = x * Math.Sin(y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);

            }
        }
        #endregion
        // For Cartesian
        public static Point CreateInstanceForCartesian(double x, double y)
         =>  new Point(x, y);
        
        public static Point CreateInstanceForPolar(double rho, double theta) 
            => new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString() => $"X: {this.x} Y:{this.y}";
    }

    public enum CoordinateSystem
    {
        Cartesian = 1,
        Polar = 2
    }
}
