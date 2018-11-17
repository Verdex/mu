
namespace mu
{
    public class Distance
    {
        public decimal Value;

        public Distance( decimal v )
        {
            Value = v;
        }
    }

    public class DistanceSquared
    {
        public decimal Value;

        public DistanceSquared( decimal v )
        {
            Value = v;
        }
    }

    public class Cell<T>
    {
        // map
        // applicative
        // bind?
    }

    public class Square<T>
    {
        private readonly Cell<T>[,] _cells;

        public Cell<T> this[int r, int c]
        {
            get { return _cells[r,c]; }
            set { _cells[r,c] = value; }
        }
        // bind?
        // applicative
        // map
        // sub square
        // all cells in square
        // all cells between two cells (line)
        // sub "triangle" subsquare but only take a specific angle of cells
        //          with some facing
        // distance between squares
        // distance squared between squares
    }
}
