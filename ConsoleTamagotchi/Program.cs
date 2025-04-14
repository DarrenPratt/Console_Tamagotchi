using System;
using System.Threading.Tasks;

namespace ConsoleTamagotchi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to Console Tamagotchi!");
            Console.Write("Enter your Tamagotchi's name: ");
            string name = Console.ReadLine();

            var pet = new Tamagotchi(name);
            var game = new Game(pet);
            await game.RunAsync();
        }
    }
}

