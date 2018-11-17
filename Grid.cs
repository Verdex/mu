
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

    public class DistanceGridd
    {
        public decimal Value;

        public DistanceGridd( decimal v )
        {
            Value = v;
        }
    }

    public class Grid<T>
    {
        private readonly T[,] _cells;

        private Grid( int rowCount, int colCount )
        {
            _cells = new T[rowCount, colCount];
            RowCount = rowCount;
            ColCount = colCount;
        }

        public Grid( int rowCount, int colCount, T init )
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

        public IEnumerable<(int row, int col, T value)> CellsWithIndex()
        {
            for( var r = 0; r < RowCount; r++ )
            {
                for( var c = 0; c < ColCount; c++ )
                {
                    yield return (r, c, this[r,c]);
                }
            }
        }

        public Grid<S> Map<S>( Func<T, S> f )
        {
            var ret = new Grid<S>( RowCount, ColCount );
            Transfer( (r, c) => f( this[r,c] ), ret );
            return ret;
        }

        public Grid<T> SubGrid( int centerRow, int centerCol, int radius )
        {
            return null;
        }

        public Grid<(int origRow, int origCol, T origValue)> SubGrid( int startRow, int startCol, int rowLength, int colLength)
        {
            var ret = new Grid<(int, int, T)>( rowLength, colLength );
            Transfer( (r, c) => (startRow + r, startCol + c, this[startRow + r, startCol + c]), ret );
            return ret;
        }
        
        private static void Transfer<S>( Func<int, int, S> src, Grid<S> dest )
        {
            for(var r = 0; r < dest.RowCount; r++)
            {
                for(var c = 0; c < dest.ColCount; c++)
                {
                    dest[r,c] = src(r,c);
                }
            }
        }

        // all cells between two cells (line)
        // sub "triangle" subsquare but only take a specific angle of cells
        //          with some facing
        // distance between squares
        // distance squared between squares
    }
}