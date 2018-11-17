
namespace mu
{
    public class Loc
    {
        public int X { get; }
        public int Y { get; }

        public Loc( int x, int y )
        {
            X = x;
            Y = y;
        }
    }

    public class MutLoc
    {
        public int X;
        public int Y;

        public MutLoc( int x, int y )
        {
            X = x;
            Y = y;
        }
    }

    public static class LocUtil
    {
        public static Loc ToLoc( this MutLoc l ) => new Loc( l.X, l.Y );
        public static Loc ToLoc( int row, int col ) => new Loc( col, row );
        public static MutLoc ToMutLoc( this Loc l ) => new MutLoc( l.X, l.Y );
        public static MutLoc ToMutLoc( int row, int col ) => new MutLoc( col, row );
    }
}
