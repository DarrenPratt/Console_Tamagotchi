using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTamagotchi
{
    public class Game
    {
        private readonly Tamagotchi _pet;

        public Game(Tamagotchi pet)
        {
            _pet = pet;
            DrawStaticMenu();
            _pet.RefreshScreen();
        }

        public async Task RunAsync()
        {
            Task.Run(async () =>
            {
                while (_pet.IsRunning)
                {
                    _pet.PassTime();
                    _pet.RefreshScreen();
                    await Task.Delay(2000);
                }
            });

            while (_pet.IsRunning)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    HandleInput(keyInfo.Key);
                }
                Thread.Sleep(100);
            }
        }

        private void DrawStaticMenu()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("What would you like to do?");
            Console.SetCursorPosition(0, 1);
            Console.WriteLine("1. Feed");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("2. Clean");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("3. Pet");

            Console.SetCursorPosition(0, 4);
            if (_pet.Level >= 2)
            {
                Console.WriteLine("4. Play");
            }
            else
            {
                Console.WriteLine("4. Play - Currently Locked");
            }

            Console.SetCursorPosition(0, 5);
            Console.WriteLine("5. Quit");
        }

        private void HandleInput(ConsoleKey key)
        {
            Console.SetCursorPosition(0, 7); // Clear previous messages
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 7);

            switch (key)
            {
                case ConsoleKey.D1:
                    Actions.Feed(_pet);
                    break;
                case ConsoleKey.D2:
                    Actions.Clean(_pet);
                    break;
                case ConsoleKey.D3:
                    Actions.Pet(_pet);
                    break;
                case ConsoleKey.D4:
                    if (_pet.Level >= 2)
                        Actions.Play(_pet);
                    else
                        Console.WriteLine("Play is currently locked. Reach Level 2 to unlock it.");
                    break;
                case ConsoleKey.D5:
                    _pet.IsRunning = false;
                    Console.WriteLine("Thanks for playing! Goodbye.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            DrawStaticMenu(); // Refresh the menu whenever an action is performed
        }
    }
}
