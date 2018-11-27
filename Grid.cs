
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
            Transfer( loc => init, this );
        }

        public T this[ILoc loc]
        {
            get 
            { 
                var (row, col) = loc.ToRowCol();
                return _cells[row,col]; 
            }
            set 
            { 
                var (row, col) = loc.ToRowCol();
                _cells[row,col] = value; 
            }
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

        public IEnumerable<(ILoc loc, T value)> CellsWithLoc()
        {
            for( var y = 0; y < RowCount; y++ )
            {
                for( var x = 0; x < ColCount; x++ )
                {
                    var l = new Loc(x, y);
                    yield return (l, this[l]);
                }
            }
        }

        public Grid<S> Map<S>( Func<T, S> f )
        {
            var ret = new Grid<S>( RowCount, ColCount );
            Transfer( loc => f( this[loc] ), ret );
            return ret;
        }

        public Grid<(ILoc origLoc, T origValue)> SubGrid( ILoc startLoc, int rowLength, int colLength)
        {
            var (startRow, startCol) = startLoc.ToRowCol();

            (ILoc originLoc, T origValue) MoveCell( ILoc loc )
            {
                var (r, c) = loc.ToRowCol();
                var l = new Loc( startRow + r, startCol + c );
                return (l, this[l]);
            }

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
            var ret = new Grid<(ILoc, T)>( rowLength, colLength );
            Transfer( MoveCell, ret );
            return ret;
        }

        // TODO probably want to move this into the same place as Line 
        /*public Grid<(int origRow, int origCol, T origValue)> SubGridRadius( ILoc center, int radius )
        {
            var length = (radius * 2) + 1;
            var (centerRow, centerCol) = center.ToRowCol();
            return SubGrid( centerRow - radius, centerCol - radius, length, length );
        }
        // TODO might consider moving line functionality someplace else.  Given an x and y provide a list of xs and ys.
        // Can also do that with other shapes.
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
        }*/

        private static void Transfer<S>( Func<ILoc, S> src, Grid<S> dest )
        {
            for(var y = 0; y < dest.RowCount; y++)
            {
                for(var x = 0; x < dest.ColCount; x++)
                {
                    var loc = new Loc( x, y );
                    dest[loc] = src(loc);
                }
            }
        }
    }
}
