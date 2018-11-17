
using System;
using System.Linq;
using System.Collections.Generic;


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

    public class Square<T>
    {
        private readonly T[,] _cells;

        private Square( int rowCount, int colCount )
        {
            _cells = new T[rowCount, colCount];
            RowCount = rowCount;
            ColCount = colCount;
        }

        public Square( int rowCount, int colCount, T init )
            :this( rowCount, colCount )
        {
            Transfer( (r,c) => init, this );
        }

        public T this[int r, int c]
        {
            get { return _cells[r,c]; }
            set { _cells[r,c] = value; }
        }

        public int RowCount { get; }
        public int ColCount { get; }

        public IEnumerable<T> Cells()
        {
            foreach( var c in _cells )
            {
                yield return c;
            }
        }

        public Square<S> Map<S>( Func<T, S> f )
        {
            var ret = new Square<S>( RowCount, ColCount );
            Transfer( (r, c) => f( this[r,c] ), ret );
            return ret;
        }

        public Square<T> SubSquare( int centerRow, int centerCol, int radius )
        {
            return null;
        }
        
        private static void Transfer<S>( Func<int, int, S> src, Square<S> dest )
        {
            for(var r = 0; r < dest.RowCount; r++)
            {
                for(var c = 0; c < dest.ColCount; c++)
                {
                    dest[r,c] = src(r,c);
                }
            }
        }

        // sub square
        // all cells between two cells (line)
        // sub "triangle" subsquare but only take a specific angle of cells
        //          with some facing
        // distance between squares
        // distance squared between squares
    }
}
