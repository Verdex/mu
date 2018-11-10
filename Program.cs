
using System;

namespace mu
{
    public static class Program
    {
        public static void Main()
        {
            MyClear(ConsoleColor.Black);
            Console.CursorVisible = false;
            DrawLetter( 'a', 5, 5, ConsoleColor.Blue, ConsoleColor.Black );
            DrawLetter( 'b', 1, 1, ConsoleColor.Red, ConsoleColor.Black );
            DrawLetter( 'c', 1, 5, ConsoleColor.Green, ConsoleColor.Black );
            DrawLetter( 'd', 5, 1, ConsoleColor.Yellow, ConsoleColor.Black );
            Console.Read();
            Console.Clear();
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
