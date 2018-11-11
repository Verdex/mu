
using System;
using System.Collections.Generic;

namespace mu
{
    public static class Program
    {
        public static void Main()
        {
            Console.CursorVisible = false;

            var sx = 0;
            var sy = 0;
            var c = '\0';
            
            while ( c != 'q' )
            {
                var h = Console.WindowHeight;
                var w = Console.WindowWidth;
                MyClear(ConsoleColor.Black);
                DrawLetter( 'a', 5, 5, ConsoleColor.Blue, ConsoleColor.Black );
                DrawLetter( 'b', 1, 1, ConsoleColor.Red, ConsoleColor.Black );
                DrawLetter( 'c', 1, 5, ConsoleColor.Green, ConsoleColor.Black );
                DrawLetter( 'd', 5, 1, ConsoleColor.Yellow, ConsoleColor.Black );
                var v = Console.ReadKey(true);
                c = v.KeyChar;
                if ( 

                //l.Add( T(v.Modifiers) );
            }
        }

        private static string T( ConsoleModifiers ms )
        {
            // Control and shift shows up but the others dont
            // multiple control and shift cant be pressed at the same time
            var s = "";
            if ( (ms & ConsoleModifiers.Alt) != 0 )
            {
                s += " alt ";
            }
            if ( (ms & ConsoleModifiers.Control) != 0 )
            {
                s += " control ";
            }
            if ( (ms & ConsoleModifiers.Shift) != 0 )
            {
                s += " shift ";
            }
            return s;
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
