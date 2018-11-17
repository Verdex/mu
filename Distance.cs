
using System;

namespace mu
{
    public class Distance
    {
        public decimal Value { get; }

        public Distance( decimal v )
        {
            Value = v;
        }
    }

    public class DistanceSquared
    {
        public decimal Value { get; }

        public DistanceSquared( decimal v )
        {
            Value = v;
        }
    }

    public static class DistUtil
    {
        private static decimal Sqrt( decimal x ) => (decimal)Math.Sqrt( (double)x );
        private static decimal Sq( decimal x ) => x * x;

        public static Distance Dist( int x1, int y1, int x2, int y2 )
            => new Distance( (decimal)Sqrt( Sq( x2 - x1 ) + Sq( y2 - y1 ) ) );

        public static DistanceSquared DistSq( int x1, int y1, int x2, int y2 )
            => new DistanceSquared( Sq( x2 - x1 ) + Sq( y2 - y1 ) );

        public static bool Lt( this Distance t, Distance dist ) => t.Value < dist.Value;
        public static bool Gt( this Distance t, Distance dist ) => t.Value > dist.Value;

        public static bool Lt( this Distance t, DistanceSquared distSq ) => t.Value < Sqrt(distSq.Value);
        public static bool Gt( this Distance t, DistanceSquared distSq ) => t.Value > Sqrt(distSq.Value);

        public static bool Lt( this DistanceSquared t, Distance dist ) => Sqrt(t.Value) < dist.Value;
        public static bool Gt( this DistanceSquared t, Distance dist ) => Sqrt(t.Value) > dist.Value;

        public static bool Lt( this DistanceSquared t, DistanceSquared distSq ) => t.Value < distSq.Value;
        public static bool Lte( this DistanceSquared t, DistanceSquared distSq ) => t.Value <= distSq.Value;
        public static bool Gt( this DistanceSquared t, DistanceSquared distSq ) => t.Value > distSq.Value;
        public static bool Gte( this DistanceSquared t, DistanceSquared distSq ) => t.Value >= distSq.Value;

        public static bool Eq( this DistanceSquared t, DistanceSquared distSq ) => t.Value == distSq.Value;
        public static bool Neq( this DistanceSquared t, DistanceSquared distSq ) => t.Value != distSq.Value;

    }
}
