
using System;
using System.Linq;
using System.Collections.Generic;


namespace mu
{
    public class WorldEntity
    {
        public int Id;
        public char Display;
        public int X;
        public int Y;
        // can the mods be put on a different class or on a tuple?
        public int XMod;
        public int YMod;

        public WorldEntity( int id, char display, int x, int y )
        {
            Id = id;
            Display = display;
            X = x;
            Y = y;
            XMod = 0;
            YMod = 0;
        }
    }

    public class World
    {
        // The _es also needs screen y modifier
        public List<WorldEntity> _es = new List<WorldEntity>();
        public WorldEntity _hero;

        private int MaxY;
        private int MaxX;

        public World()
        {
            MaxY = Console.WindowHeight; // TODO this changes and cant be static like this
            MaxX = Console.WindowWidth;
        }

        public void AddEntity( int id, char c, int x, int y )
        {
            _es.Add( new WorldEntity(id, c, x, y) );
        }

        public void AddHero( char c, int x, int y )
        {
            _hero = new WorldEntity( 0, c, x, y );
        }

        public (int, int) HeroPosition()
        {
            return (_hero.X, _hero.Y);
        }

        public void MoveUp( int id )
        {
            var entity = _es.Single( e => e.Id == id );
            entity.Y--;
        }

        public void MoveDown( int id )
        {
            var entity = _es.Single( e => e.Id == id );
            entity.Y++;
        }

        public void MoveLeft( int id )
        {
            var entity = _es.Single( e => e.Id == id );
            entity.X--;
        }

        public void MoveRight( int id )
        {
            var entity = _es.Single( e => e.Id == id );
            entity.X++;
        }

        private void RelativeHeroMoveUp()
        {
        }

        public void HeroMoveUp( )
        {
            if ( _hero.Y == 0 )
            {
                RelativeHeroMoveUp();
            }
            else
            {
                _hero.Y--; // need world y value and screen y value (maybe screen y modifier)
            }
        }

        public void HeroMoveDown(  )
        {
           _hero.Y++; 
        }

        public void HeroMoveLeft(  )
        {
            _hero.X--;
        }

        public void HeroMoveRight( )
        {
            _hero.X++;
        }
        
    }

    public static class Program
    {
        public static void Main()
        {
            Console.CursorVisible = false;

            var w = new World();
            w.AddHero( '@', 0, 0 );

            var c = '\0';
            
            while ( c != 'q' )
            {
                //var h = Console.WindowHeight;
                //var w = Console.WindowWidth;
                MyClear(ConsoleColor.Black);


                var (hpx, hpy) = w.HeroPosition();

                DrawLetter( '@', hpx, hpy, ConsoleColor.Cyan, ConsoleColor.Black );

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
