
using System;
using System.Linq;
using System.Collections.Generic;

using static mu.LocUtil;

namespace mu
{
    public static class Tests 
    {
        public static void Main()
        {
            Test( "Grid Initializer should work", () =>
            {
                var s = new Grid<int>( 2, 3, 77 );

                Check( "0, 0", s[0,0] == 77 );
                Check( "0, 1", s[0,1] == 77 );
                Check( "0, 2", s[0,2] == 77 );
                Check( "1, 0", s[1,0] == 77 );
                Check( "1, 1", s[1,1] == 77 );
                Check( "1, 2", s[1,2] == 77 );
            });

            Test( "Grid Index Get and Set work", () =>
            {
                var s = new Grid<int>( 2, 2, 77 );

                Check( "initial get", s[0,1] == 77 );
                s[0,1] = 8;
                Check( "set and get", s[0,1] == 8 );
            });

            Test( "Grid Cells should work", () =>
            {
                var s = new Grid<int>( 2, 2, 77 );
                s[0,0] = 8;
                var o = s.Cells().ToList();
                Check( "Length", o.Count == 4 );
                Check( "3 77 exist", o.Where( v => v == 77 ).Count() == 3 );
                Check( "1 8 exists", o.Where( v => v == 8 ).Count() == 1 );
            });

            Test( "Grid Cells with index should work", () =>
            {
                var s = new Grid<int>( 2, 2, 77 );
                s[0,0] = 8;
                s[0,1] = 9;
                s[1,0] = 10;
                s[1,1] = 11;
                var o = s.CellsWithIndex().ToList();
                Check( "Length", o.Count == 4 );
                var _00 = o.Single( v => v.value == 8 );
                var _01 = o.Single( v => v.value == 9 );
                var _10 = o.Single( v => v.value == 10 );
                var _11 = o.Single( v => v.value == 11 );
                Check( "0, 0; value", _00.value == 8 );
                Check( "0, 0; row", _00.row == 0 );
                Check( "0, 0; col", _00.col == 0 );
                Check( "0, 1; value", _01.value == 9 );
                Check( "0, 1; row", _01.row == 0 );
                Check( "0, 1; col", _01.col == 1 );
                Check( "1, 0; value", _10.value == 10 );
                Check( "1, 0; row", _10.row == 1 );
                Check( "1, 0; col", _10.col == 0 );
                Check( "1, 1; value", _11.value == 11 );
                Check( "1, 1; row", _11.row == 1 );
                Check( "1, 1; col", _11.col == 1 );
            });

            Test( "SubGrid should work", () =>
            {
                var g = new Grid<int>( 8, 8, 77 );
                g[2,2] = 1;
                g[2,3] = 2;
                g[2,4] = 3;
                g[3,2] = 4;
                g[3,3] = 5;
                g[3,4] = 6;
                g[4,2] = 7;
                g[4,3] = 8;
                g[4,4] = 9;
                var subG = g.SubGrid(2, 2, 3, 3);
                Check( "Length", subG.Cells().Count() == 9 );
                Check( "0, 0; val", subG[0, 0].origValue == 1 );
                Check( "0, 0; row", subG[0, 0].origRow == 2 );
                Check( "0, 0; col", subG[0, 0].origCol == 2 );

                Check( "0, 1; val", subG[0, 1].origValue == 2 );
                Check( "0, 1; row", subG[0, 1].origRow == 2 );
                Check( "0, 1; col", subG[0, 1].origCol == 3 );
                
                Check( "0, 2; val", subG[0, 2].origValue == 3 );
                Check( "0, 2; row", subG[0, 2].origRow == 2 );
                Check( "0, 2; col", subG[0, 2].origCol == 4 );

                Check( "1, 0; val", subG[1, 0].origValue == 4 );
                Check( "1, 0; row", subG[1, 0].origRow == 3 );
                Check( "1, 0; col", subG[1, 0].origCol == 2 );

                Check( "1, 1; val", subG[1, 1].origValue == 5 );
                Check( "1, 1; row", subG[1, 1].origRow == 3 );
                Check( "1, 1; col", subG[1, 1].origCol == 3 );
                
                Check( "1, 2; val", subG[1, 2].origValue == 6 );
                Check( "1, 2; row", subG[1, 2].origRow == 3 );
                Check( "1, 2; col", subG[1, 2].origCol == 4 );

                Check( "2, 0; val", subG[2, 0].origValue == 7 );
                Check( "2, 0; row", subG[2, 0].origRow == 4 );
                Check( "2, 0; col", subG[2, 0].origCol == 2 );

                Check( "2, 1; val", subG[2, 1].origValue == 8 );
                Check( "2, 1; row", subG[2, 1].origRow == 4 );
                Check( "2, 1; col", subG[2, 1].origCol == 3 );
                
                Check( "2, 2; val", subG[2, 2].origValue == 9 );
                Check( "2, 2; row", subG[2, 2].origRow == 4 );
                Check( "2, 2; col", subG[2, 2].origCol == 4 );
            });

            Test( "SubGrid should return grid when asked for larger grid", () =>
            {
                var g = new Grid<int>( 2, 2, 77 );
                g[0,0] = 1;
                g[0,1] = 2;
                g[1,0] = 3;
                g[1,1] = 4;
                var subG = g.SubGrid( -1, -1, 4, 4 );
                var o = subG.CellsWithIndex().ToList();
                Check( "Length", o.Count == 4 );
                Check( "0, 0", subG[0,0].origValue == 1 );
                Check( "0, 1", subG[0,1].origValue == 2 );
                Check( "1, 0", subG[1,0].origValue == 3 );
                Check( "1, 1", subG[1,1].origValue == 4 );
            });

            Test( "SubGrid should handle negative row and col", () =>
            {
                var g = new Grid<int>( 2, 2, 77 );
                g[0,0] = 1;
                g[0,1] = 2;
                g[1,0] = 3;
                g[1,1] = 4;
                var subG = g.SubGrid( -1, -1, 1, 1 );
                var o = subG.CellsWithIndex().ToList();
                Check( "Length", o.Count == 1 );
                Check( "0, 0", subG[0,0].origValue == 1 );
            });

            Test( "SubGrid should handle negative row and positive  col", () =>
            {
                var g = new Grid<int>( 2, 2, 77 );
                g[0,0] = 1;
                g[0,1] = 2;
                g[1,0] = 3;
                g[1,1] = 4;
                var subG = g.SubGrid( -1, 0, 1, 1 );
                var o = subG.CellsWithIndex().ToList();
                Check( "Length", o.Count == 1 );
                Check( "0, 0", subG[0,0].origValue == 1 );
            });

            Test( "SubGrid should handle positive row and negative col", () =>
            {
                var g = new Grid<int>( 2, 2, 77 );
                g[0,0] = 1;
                g[0,1] = 2;
                g[1,0] = 3;
                g[1,1] = 4;
                var subG = g.SubGrid( 0, -1, 1, 1 );
                var o = subG.CellsWithIndex().ToList();
                Check( "Length", o.Count == 1 );
                Check( "0, 0", subG[0,0].origValue == 1 );
            });

            Test( "SubGrid should handle rowLength and colLength overflow", () =>
            {
                var g = new Grid<int>( 2, 2, 77 );
                g[0,0] = 1;
                g[0,1] = 2;
                g[1,0] = 3;
                g[1,1] = 4;
                var subG = g.SubGrid( 1, 1, 2, 2 );
                var o = subG.CellsWithIndex().ToList();
                Check( "Length", o.Count == 1 );
                Check( "0, 0", subG[0,0].origValue == 4 );
            });

            Test( "SubGridRadius should get correct values at 0,0", () =>
            {
                var g = new Grid<int>( 2, 2, 77 );
                g[0,0] = 1;
                g[0,1] = 2;
                g[1,0] = 3;
                g[1,1] = 4;
                var subG = g.SubGridRadius( ToLoc( 0, 0 ), 0 );
                var o = subG.CellsWithIndex().Single();
                Check( "row", o.row == 0 );
                Check( "col", o.col == 0 );
                Check( "orig row", o.value.origRow == 0 );
                Check( "orig col", o.value.origCol == 0 );
                Check( "orig value", o.value.origValue == 1 );
            });

            Test( "SubGridRadius should get correct values at 0,1", () =>
            {
                var g = new Grid<int>( 2, 2, 77 );
                g[0,0] = 1;
                g[0,1] = 2;
                g[1,0] = 3;
                g[1,1] = 4;
                var subG = g.SubGridRadius( ToLoc( 0, 1 ), 0 );
                var o = subG.CellsWithIndex().Single();
                Check( "row", o.row == 0 );
                Check( "col", o.col == 0 );
                Check( "orig row", o.value.origRow == 0 );
                Check( "orig col", o.value.origCol == 1 );
                Check( "orig value", o.value.origValue == 2 );
            });

            Test( "SubGridRadius should get correct values at 1,0", () =>
            {
                var g = new Grid<int>( 2, 2, 77 );
                g[0,0] = 1;
                g[0,1] = 2;
                g[1,0] = 3;
                g[1,1] = 4;
                var subG = g.SubGridRadius( ToLoc( 1, 0 ), 0 );
                var o = subG.CellsWithIndex().Single();
                Check( "row", o.row == 0 );
                Check( "col", o.col == 0 );
                Check( "orig row", o.value.origRow == 1 );
                Check( "orig col", o.value.origCol == 0 );
                Check( "orig value", o.value.origValue == 3 );
            });

            Test( "SubGridRadius should get correct values at 1,1", () =>
            {
                var g = new Grid<int>( 2, 2, 77 );
                g[0,0] = 1;
                g[0,1] = 2;
                g[1,0] = 3;
                g[1,1] = 4;
                var subG = g.SubGridRadius( ToLoc( 1, 1 ), 0 );
                var o = subG.CellsWithIndex().Single();
                Check( "row", o.row == 0 );
                Check( "col", o.col == 0 );
                Check( "orig row", o.value.origRow == 1 );
                Check( "orig col", o.value.origCol == 1 );
                Check( "orig value", o.value.origValue == 4 );
            });

            Test( "SubGridRadius should get correct count for radius 0", () =>
            {
                var g = new Grid<int>( 7, 7, 77 );
                var count = g.SubGridRadius( ToLoc( 3, 3 ), 0).Cells().Count();
                Check( "Count", count == 1 );
            });

            Test( "SubGridRadius should get correct count for radius 1", () =>
            {
                var g = new Grid<int>( 7, 7, 77 );
                var count = g.SubGridRadius( ToLoc( 3, 3 ), 1).Cells().Count();
                Check( "Count", count == 9 );
            });

            Test( "SubGridRadius should get correct count for radius 2", () =>
            {
                var g = new Grid<int>( 7, 7, 77 );
                var count = g.SubGridRadius( ToLoc( 3, 3 ), 2).Cells().Count();
                Check( "Count", count == 25 );
            });

            Test( "SubGridRadius should get correct count for radius 3", () =>
            {
                var g = new Grid<int>( 7, 7, 77 );
                var count = g.SubGridRadius( ToLoc( 3, 3 ), 3).Cells().Count();
                Check( "Count", count == 49 );
            });

            Test( "SubGridRadius should get correct count for radius 4", () =>
            {
                var g = new Grid<int>( 7, 7, 77 );
                var count = g.SubGridRadius( ToLoc( 3, 3 ), 4).Cells().Count();
                Check( "Count", count == 49 );
            });

            Test( "Line should get horizontal moving forward", () =>
            {
            });

            Test( "Line should get horizontal moving backward", () =>
            {
            });
        }

        private static string _name;
        private static List<string> _checks = new List<string>();
        private static void Test( string name, Action a )
        {
            _name = name;
            try
            {
                a();
                DisplayFailedChecks();
                if ( _checks.Count == 0 )
                {
                    Console.WriteLine( $"{_name} : Passed" );
                }
            }
            catch( Exception e )
            {
                Console.WriteLine( $"{_name} has thrown exception : {e}" );
            }
            finally
            {
                _name = "";
                _checks = new List<string>();
            }
        }

        private static void DisplayFailedChecks()
        {
            foreach( var check in _checks )
            {
                Console.WriteLine( $"{_name} failed check {check}" );
            }
        }
        private static void Check( string m, bool t )
        {
            if ( !t ) 
            {
                _checks.Add( m );
            }
        }
        private static void Assert( string m, bool t )
        {
            if ( !t )
            {
                throw new Exception( $"Failed assert {m}" );
            }
        }
    }
}
