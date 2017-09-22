using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commandline_Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Commandline Minesweeper");
            Console.WriteLine("-------------------------");
            Console.WriteLine("\n");
            Console.WriteLine("Controls: ");
            Console.WriteLine("\t Arrow keys: Movement");
            Console.WriteLine("\t Num0: Reveal Field");
            Console.WriteLine("\t Backspace: Flag Field");
            Console.WriteLine("\t Esc: End game");
            Console.WriteLine("\n");
            Console.WriteLine("Press a key to start");
            Console.ReadKey();
            ConsoleKeyInfo info;
            Game MineSweeper = new Game();
            do
            {
                Console.Clear();
                Console.WriteLine(MineSweeper.VisualizeGameField());
                info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            MineSweeper.Move(0, -1);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            MineSweeper.Move(1, 0);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            MineSweeper.Move(0, 1);
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            MineSweeper.Move(-1, 0);
                            break;
                        }
                    case ConsoleKey.Backspace:
                        {
                            MineSweeper.FlagPos();
                            break;
                        }
                    case ConsoleKey.NumPad0:
                        {
                            MineSweeper.Choose();
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            MineSweeper.GameOver = true;
                            break;
                        }
                }
            } while (!MineSweeper.IsGameOver());
            MineSweeper.GameOver = true;
            Console.Clear();
            Console.WriteLine("G A M E  O V E R");
            Console.WriteLine(MineSweeper.VisualizeGameField());
            Console.ReadKey();
        }
    }
}
