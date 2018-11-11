
using System;
using System.Linq;
using System.Collections.Generic;


namespace mu
{
    public class World
    {
        public List<(int, char, int, int)> _es = new List<(int, char, int, int)>();
        public char _hc;
        public int _hx;
        public int _hy;

        private int MaxY;
        private int MaxX;

        public World()
        {
            MaxY = Console.WindowHeight; // TODO this changes and cant be static like this
            MaxX = Console.WindowWidth;
        }

        public void AddEntity( int id, char c, int x, int y )
        {
            _es.Add( (id, c, x, y) );
        }

        public void AddHero( char c, int x, int y )
        {
            _hc = c;
            _hx = x;
            _hy = y;
        }

        public void MoveUp( int id )
        {
            var (i, c, x, y) = _es.Single( e => e.Item1 == id );
            _es.RemoveAll( e => e.Item1 == id );
            _es.Add( (i, c, x, y - 1 ) );
        }

        public void MoveDown( int id )
        {
            var (i, c, x, y) = _es.Single( e => e.Item1 == id );
            _es.RemoveAll( e => e.Item1 == id );
            _es.Add( (i, c, x, y + 1 ) );
        }

        public void MoveLeft( int id )
        {
            var (i, c, x, y) = _es.Single( e => e.Item1 == id );
            _es.RemoveAll( e => e.Item1 == id );
            _es.Add( (i, c, x - 1, y  ) );
        }

        public void MoveRight( int id )
        {
            var (i, c, x, y) = _es.Single( e => e.Item1 == id );
            _es.RemoveAll( e => e.Item1 == id );
            _es.Add( (i, c, x + 1, y ) );
        }

        public void HeroMoveUp( )
        {
            _hy--;
        }

        public void HeroMoveDown(  )
        {
            _hy++;
        }

        public void HeroMoveLeft(  )
        {
            _hx--;
        }

        public void HeroMoveRight( )
        {
            _hx++;
        }
        
    }

    public static class Program
    {
        public static void Main()
        {
            Console.CursorVisible = false;

            var w = new World();
            w.AddHero( '@', 0, 0 );

            var sx = 0;
            var sy = 0;

            var wx = 0;
            var wy = 0;
            var c = '\0';
            
            while ( c != 'q' )
            {
                //var h = Console.WindowHeight;
                //var w = Console.WindowWidth;
                MyClear(ConsoleColor.Black);


                DrawLetter( w._hc, w._hx, w._hy, ConsoleColor.Cyan, ConsoleColor.Black );

                DrawLetter( 'a', 5, 5, ConsoleColor.Blue, ConsoleColor.Black );
                DrawLetter( 'b', 1, 1, ConsoleColor.Red, ConsoleColor.Black );
                DrawLetter( 'c', 1, 5, ConsoleColor.Green, ConsoleColor.Black );
                DrawLetter( 'd', 5, 1, ConsoleColor.Yellow, ConsoleColor.Black );
                var v = Console.ReadKey(true);
                c = v.KeyChar;

                switch( c )
                {
                    case 'h':
                        w.HeroMoveLeft();
                        break;
                    case 'j':
                        w.HeroMoveDown();
                        break;
                    case 'k':
                        w.HeroMoveUp();
                        break;
                    case 'l':
                        w.HeroMoveRight();
                        break;
                }

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
