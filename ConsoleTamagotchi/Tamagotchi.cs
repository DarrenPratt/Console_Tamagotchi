using System;

namespace ConsoleTamagotchi
{
    public class Tamagotchi
    {
        public string Name { get; set; }
        public int Hunger { get; private set; }         // 100 = Full, 0 = Starving
        public int Cleanliness { get; private set; }    // 100 = Clean, 0 = Filthy
        public int Playfulness { get; private set; }    // 100 = Happy, 0 = Bored
        public int Level { get; private set; }
        public int Experience { get; private set; }

        public bool IsRunning { get; set; }

        private string[][] ArtStyles;

        // Stat Decrease Rates (You can adjust these as needed)
        private int HungerRate = 3;        // Decreases faster than others
        private int CleanlinessRate = 1;   // Decreases slower
        private int PlayfulnessRate = 2;   // Moderate decrease rate

        // Column Display Settings (Easily Adjustable)
        private static int Column1Width = 15;
        private static int Column2Width = 25;
        private static int ColumnSpacing = 25;
        private static int Column3Start = Column1Width + Column2Width + ColumnSpacing;

        public Tamagotchi(string name)
        {
            Name = name;
            Hunger = 100;         // Start full
            Cleanliness = 100;     // Start clean
            Playfulness = 100;     // Start happy
            Level = 1;
            Experience = 0;
            IsRunning = true;

            // Define all available art styles

            ArtStyles = new string[][]
{
            // 😊 Happy
            new string[]
            {
                "               (\\_^)/",
                "              (^.^)",
                "               > ♥ <",
                $"       {Name} is feeling great!"
            },

            // 😐 Content
            new string[]
            {
                "               (\\_/) ",
                "              (o.o) ",
                "               > ^ <",
                $"       {Name} is doing fine."
            },

            // 😟 Sad
            new string[]
            {
                "               (._.) ",
                "              (u_u) ",
                "               > ~ <",
                $"       {Name} looks a bit down..."
            },

            // 😢 Upset
            new string[]
            {
                "               (x_x) ",
                "              (;_;) ",
                "               > . <",
                $"       {Name} is really upset!"
            }
        };
      }

        public void GainExperience(int amount)
        {
            Experience += amount;
            if (Experience >= Level * 50)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Experience = 0;
            Console.SetCursorPosition(Column2Width + ColumnSpacing, 10);
            Console.WriteLine($"Congratulations! {Name} leveled up to level {Level}!");
        }

    

        private void DisplayStats()
        {
            Console.SetCursorPosition(Column1Width + ColumnSpacing, 0);
            ClearLine();
            Console.WriteLine($"  {Name}'s Stats:");

            Console.SetCursorPosition(Column1Width + ColumnSpacing, 1);
            ClearLine();
            Console.WriteLine($"  Hunger: {Hunger}/100");

            Console.SetCursorPosition(Column1Width + ColumnSpacing, 2);
            ClearLine();
            Console.WriteLine($"  Cleanliness: {Cleanliness}/100");

            Console.SetCursorPosition(Column1Width + ColumnSpacing, 3);
            ClearLine();
            Console.WriteLine($"  Playfulness: {Playfulness}/100");

            Console.SetCursorPosition(Column1Width + ColumnSpacing, 4);
            ClearLine();
            Console.WriteLine($"  Mood: {GetMoodDescription()}");

            Console.SetCursorPosition(Column1Width + ColumnSpacing, 5);
            ClearLine();
            Console.WriteLine($"  Level: {Level}");

            Console.SetCursorPosition(Column1Width + ColumnSpacing, 6);
            ClearLine();
            Console.WriteLine($"  Experience: {Experience}");

            DisplayPetArt();
        }


        // Clear only the stats area
        private void ClearLine()
        {
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            Console.SetCursorPosition(Column1Width + ColumnSpacing, currentTop);
            Console.Write(new string(' ', 40));  // Clears 40 characters of the line

            Console.SetCursorPosition(currentLeft, currentTop);
        }

        private void DisplayPetArt()
        {
            int artIndex = SelectArtIndex();
            var art = ArtStyles[artIndex];

            int artPositionY = 0;
            foreach (var line in art)
            {
                Console.SetCursorPosition(Column3Start, artPositionY++);
                Console.WriteLine(line);
            }
        }

        private int SelectArtIndex()
        {
            int mood = GetMoodScore();

            if (mood >= 80)
                return 0; // Happy
            else if (mood >= 50)
                return 1; // Content
            else if (mood >= 20)
                return 2; // Sad
            else
                return 3; // Upset
        }




        public void PassTime()
        {
            int mood = GetMoodScore();

            // Base decay rates
            int baseHunger = 1;
            int baseCleanliness = 1;
            int basePlayfulness = 1;

            // Modify decay rates based on mood
            if (mood >= 80) // Happy
            {
                baseHunger = 1;
                baseCleanliness = 1;
                basePlayfulness = 1;
            }
            else if (mood >= 50) // Neutral
            {
                baseHunger = 2;
                baseCleanliness = 2;
                basePlayfulness = 2;
            }
            else if (mood >= 20) // Sad
            {
                baseHunger = 3;
                baseCleanliness = 3;
                basePlayfulness = 3;
            }
            else // Very upset
            {
                baseHunger = 4;
                baseCleanliness = 4;
                basePlayfulness = 4;
            }

            // Level scaling: higher levels handle things better
            float levelModifier = 1f - (Level * 0.05f); // 5% reduction per level
            levelModifier = Math.Max(0.5f, levelModifier); // Never decay slower than 50%

            //Apply decay
            Hunger = Math.Max(0, Hunger - Math.Max(1, (int)(baseHunger * levelModifier)));
            Cleanliness = Math.Max(0, Cleanliness - Math.Max(1, (int)(baseCleanliness * levelModifier)));
            Playfulness = Math.Max(0, Playfulness - Math.Max(1, (int)(basePlayfulness * levelModifier)));

        }


        private int GetMoodScore()
        {
            return (Hunger + Cleanliness + Playfulness) / 3;
        }


        private string GetMoodDescription()
        {
            int mood = GetMoodScore();

            if (mood >= 80)
                return "😊 Happy";
            else if (mood >= 50)
                return "😐 Content";
            else if (mood >= 20)
                return "😟 Sad";
            else
                return "😢 Upset";
        }



        public void RefreshScreen() => DisplayStats();

        // Adjusted to increase stats instead of decrease them
        public void UpdateHunger (int amount) => Hunger = Math.Min(100, Hunger + amount);
        public void UpdateCleanliness(int amount) => Cleanliness = Math.Min(100, Cleanliness + amount);
        public void UpdatePlayfulness(int amount) => Playfulness = Math.Min(100, Playfulness + amount);
    }
}
