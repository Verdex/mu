
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

        /*public IEnumerable<(int row, int col, T value)> Triangle( ILoc center, Direction direction, angle a )
        {
        }*/

        public IEnumerable<(int row, int col, T value)> Line( ILoc center, Direction direction, Distance distance )
        {
            var dist = decimal.Floor(distance.Value);

            var (row, col) = center.ToRowCol();

            switch(direction)
            {
                case Direction.North:
                    for( var i = 0; i < dist; i++ )
                    {
                        yield return ( row - i, col, this[row - i, col] );
                    }
                    break;
                case Direction.NorthEast:
                    for( var i = 0; i < dist; i++ )
                    {
                        yield return ( row - i, col + i, this[row - i, col + i] );
                    }
                    break;
                case Direction.NorthWest:
                    for( var i = 0; i < dist; i++ )
                    {
                        yield return ( row - i, col - i, this[row - i, col - i] );
                    }
                    break;
                case Direction.East:
                    for( var i = 0; i < dist; i++ )
                    {
                        yield return ( row, col + i, this[row, col + i] );
                    }
                    break;
                case Direction.West:
                    for( var i = 0; i < dist; i++ )
                    {
                        yield return ( row, col - i, this[row, col - i] );
                    }
                    break;
                case Direction.SouthEast:
                    for( var i = 0; i < dist; i++ )
                    {
                        yield return ( row + i, col + i, this[row + i, col + i] );
                    }
                    break;
                case Direction.SouthWest:
                    for( var i = 0; i < dist; i++ )
                    {
                        yield return ( row + i, col - i, this[row + i, col - i] );
                    }
                    break;
                case Direction.South:
                    for( var i = 0; i < dist; i++ )
                    {
                        yield return ( row + i, col, this[row + i, col] );
                    }
                    break;
                default:
                    throw new Exception( $"Missing direction {direction}" );
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
