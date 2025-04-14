namespace ConsoleTamagotchi
{
    public static class Actions
    {
        public static void Feed(Tamagotchi pet)
        {
            pet.UpdateHunger(10); // ✅ Increase fullness
            pet.GainExperience(5);
            pet.RefreshScreen();
            Console.SetCursorPosition(0, 7);
            Console.WriteLine($"🍖 You fed {pet.Name}. Hunger restored!");
        }

        public static void Clean(Tamagotchi pet)
        {
            pet.UpdateCleanliness(10); // ✅ Increase cleanliness
            pet.GainExperience(5);
            pet.RefreshScreen();
            Console.SetCursorPosition(0, 7);
            Console.WriteLine($"🧼 You cleaned {pet.Name}. Much better!");
        }

        public static void Pet(Tamagotchi pet)
        {
            pet.UpdatePlayfulness(10); // ✅ Increase playfulness
            pet.GainExperience(10);
            pet.RefreshScreen();
            Console.SetCursorPosition(0, 7);
            Console.WriteLine($"💖 You petted {pet.Name}. They're happier!");
        }

        public static void Play(Tamagotchi pet)
        {
            pet.UpdatePlayfulness(20); // ✅ Increase playfulness even more
            pet.GainExperience(20);
            pet.RefreshScreen();
            Console.SetCursorPosition(0, 7);
            Console.WriteLine($"🎾 You played with {pet.Name}. So much fun!");
        }
    }
}
