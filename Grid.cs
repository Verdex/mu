
using System;
using System.Linq;
using System.Collections.Generic;


namespace mu
{

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

        public Grid<(int origRow, int origCol, T origValue)> SubGridRadius( ILoc center, int radius )
        {
            var length = (radius * 2) + 1;
            var (centerRow, centerCol) = center.ToRowCol();
            return SubGrid( centerRow - radius, centerCol - radius, length, length );
        }

        public Grid<(int origRow, int origCol, T origValue)> SubGrid( int startRow, int startCol, int rowLength, int colLength)
        {
            if ( startRow < 0 )
            {
                startRow = 0;
            }
            if ( startCol < 0 )
            {
                startCol = 0;
            }
            if ( startRow + rowLength > RowCount )
            {
                rowLength = RowCount - startRow;
            }
            if ( startCol + colLength > ColCount )
            {
                colLength = ColCount - startCol;
            }
            var ret = new Grid<(int, int, T)>( rowLength, colLength );
            Transfer( (r, c) => (startRow + r, startCol + c, this[startRow + r, startCol + c]), ret );
            return ret;
        }

        //public IEnumerable<(int row, int col, T value)> Triangle( ILoc center, direction d, angle a )

        public IEnumerable<(int row, int col, T value)> Line( ILoc cell1, ILoc cell2 )
        {
            (int high, int low) HighLow( int v1, int v2 ) 
            {
                var l = v1 < v2 ? v1 : v2; 
                var h = v1 >= v2 ? v1 : v2; 
                return (h, l);
            }

            (int row, int col) CoordToRowCol( int x, decimal y )
            {
                var l = new Loc( x, (int)Decimal.Floor( y ) );
                return l.ToRowCol();
            }

            decimal GetSlope( ILoc c1, ILoc c2 )
            {
                decimal rise = cell2.Y - cell1.Y;
                decimal run = cell2.X - cell1.X;
                return rise / run; 
            }

            // b = y - m x 
            decimal YIntercept( ILoc loc, decimal m ) => loc.Y - ( m * loc.X );

            IEnumerable<(int x, decimal y)> Ys(decimal m, decimal yIntercept, int x1, int x2)
            {
                var (h, l) = HighLow( x1, x2 );
                var x = l;
                while( x <= h )
                {
                    // y = m x + b
                    yield return (x, m * x + yIntercept);
                    x++;
                }
            }

            if ( cell1.X == cell2.X && cell1.Y == cell2.Y )
            {
                var (row, col) = cell1.ToRowCol();
                yield return (row, col, this[row, col]);
                yield break;
            }

            if ( cell1.X == cell2.X )
            {
                var (h, l) = HighLow( cell1.Y, cell2.Y );
                var y = l;
                while ( y <= h )
                {
                    var (row, col) = CoordToRowCol( cell1.X, y ); 
                    yield return (row, col, this[row, col]);
                    y++;
                }
                yield break;
            }

            var slope = GetSlope( cell1, cell2 );
            var b = YIntercept( cell2, slope );
            
            var ys = Ys( slope, b, cell1.X, cell2.X ).ToList();

            foreach( var ( f, s ) in ys.Zip( ys.Skip( 1 ), (i, k) => (i, k) ) )
            {
                if ( Decimal.Floor( f.y ) == Decimal.Floor( s.y ) )
                {
                    var (row, col) = CoordToRowCol( f.x, f.y );
                    yield return (row, col, this[row, col]);  
                }
                else
                {
                    var (row1, col1) = CoordToRowCol( f.x, f.y );
                    yield return (row1, col1, this[row1, col1]);  
                    var (row2, col2) = CoordToRowCol( s.x, s.y );
                    yield return (row2, col2, this[row2, col2]);  
                }
            }
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
    }
}
