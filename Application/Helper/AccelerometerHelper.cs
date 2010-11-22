using System;

namespace Coding4Fun.DiceShaker.Application.Helper
{
    public class AccelerometerHelper
    {
        private readonly double _threshold;
        private double _lastPostionX;
        private double _lastPostionY;
        private double _lastPostionZ;

        public AccelerometerHelper(double x, double y, double z, double threshold)
        {
            _threshold = threshold;
            _lastPostionX = x;
            _lastPostionY = y;
            _lastPostionZ = z;
        }

        public bool IsShook(double x, double y, double z)
        {
            //Get the deltas of the new reading
            double deltaX = Math.Abs((_lastPostionX - x));
            double deltaY = Math.Abs((_lastPostionY - y));
            double deltaZ = Math.Abs((_lastPostionZ - z));

            //Set Values for next accelerometer reading
            _lastPostionX = x;
            _lastPostionY = y;
            _lastPostionZ = z;

            // if a delta is greater than the threshold lets roll the dice
            return (deltaX > _threshold && deltaY > _threshold) ||
                   (deltaX > _threshold && deltaZ > _threshold) ||
                   (deltaY > _threshold && deltaZ > _threshold);
        }
    }
}