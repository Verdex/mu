
using System;
using System.Linq;
using System.Collections.Generic;

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
