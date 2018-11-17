
namespace mu
{
    public interface ILoc
    {
        int X { get; } 
        int Y { get; }
        (int row, int col) ToRowCol();
    }

    public interface IMutLoc
    {
        int X { get; set; }
        int Y { get; set; }
        (int row, int col) ToRowCol();
    }

    public class Loc : ILoc, IMutLoc
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Loc( int x, int y )
        {
            X = x;
            Y = y;
        }

        public (int row, int col) ToRowCol() => (Y, X);
    }

    public static class LocUtil
    {
        public static Loc ToLoc( int row, int col ) => new Loc( col, row );
    }
}
