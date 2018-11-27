
using System;
using System.Linq;
using System.Collections.Generic;

using static mu.LocUtil;

namespace mu
{
    public static class Program
    {
        public static void Main()
        {
            var world = new Grid<(char c, ConsoleColor f, ConsoleColor b)>( 1000, 1000, ('.', ConsoleColor.White, ConsoleColor.Black) );
            Console.CursorVisible = false;

            var c = '\0';

            while ( c != 'q' )
            {
                MyClear( ConsoleColor.Black );
                var MaxY = Console.WindowHeight; 
                var MaxX = Console.WindowWidth;

                var screenGrid = world.SubGrid( new Loc( 30, 30 ), MaxY, MaxX );
                var screenCells = screenGrid.CellsWithLoc().Select( cell => new blarg { C = cell.value.origValue.c 
                                                                                      , Fore = cell.value.origValue.f
                                                                                      , Back = cell.value.origValue.b
                                                                                      , Loc = cell.loc
                                                                                      } );
                DrawWorld( screenCells );

                var v = Console.ReadKey(true);
                c = v.KeyChar;

                switch( c )
                {
                    case 'h':
                        break;
                    case 'd':
                        break;
                    case 'f':
                        break;
                    case 'l':
                        break;
                }
            }
        }

        private class blarg
        {
            public ConsoleColor Fore;
            public ConsoleColor Back;
            public char C;
            public ILoc Loc;
        }

        private static void DrawWorld( IEnumerable<blarg> blargs )
        {
            foreach( var b in blargs )
            {
                DrawLetter( b.C, b.Loc.X, b.Loc.Y, b.Fore, b.Back );
            }
        }

        private static void MyClear( ConsoleColor c )
        {
            Console.BackgroundColor = c;
            Console.Clear();
        }

        private static void DrawLetter( char c, int x, int y,  ConsoleColor fore, ConsoleColor back )
        {
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
            Console.SetCursorPosition( x, y );
            Console.Write( c );
        }
    }
}
