namespace memory_game {
    class File_manager {
        /*
        Handling IO operations, reading words from file for the game with basic error handling.
        High score, reading and writing records.
        */
        private File_manager() {}
        private static File_manager _instance = null!;

        public static File_manager Get_Instance() {
            if (_instance == null) {
                _instance = new File_manager();
            }
            return _instance;
        }
        public static List<string> Load_words() {
            List<string> words = new List<string>();
            try {
                words = File.ReadAllLines("Words.txt").ToList();
            } catch (FileNotFoundException ex) {
                Console.WriteLine(ex.Message);
                System.Environment.Exit(2); // ENOENT 2 No such file or directory
            }
            return words;
        }
        public static void Save_High_Score(long time, int guesses, long score) {
            Console.WriteLine("Save your highscore? (Y\\N)");
          switch(Console.ReadLine()) {
                case "Y":
                case "y":
                break;
                default:
                return;
            } 

            Console.WriteLine("Enter your name: ");
            string? _username = "";
            while (true) {
                _username = Console.ReadLine();
                if (_username != null && _username.Contains(';')) {
                    Console.WriteLine("Username cannot contain ';'");
                    continue;
                }
                if (_username != null && _username.Count() > 12) {
                    Console.WriteLine("Username is too long");
                    continue;
                }
                if (_username != null && _username.Count() < 3) {
                    Console.WriteLine("Username is too short");
                    continue;
                }
                break;
            } 
            

            writeToFile(time, guesses, score, _username);
            Console.WriteLine("Successfully writed to a file");
        }
        private static void writeToFile(long time, int guesses, long score, string? username) {
            string fileName = "Highscore.txt";
            if (!File.Exists(fileName)) {
                StreamWriter sw = File.CreateText(fileName);
                sw.WriteLine(time.ToString() + ';' + guesses.ToString() + ';' + score.ToString() + ';' + username);
                sw.Close();
            } else {
                StreamWriter sw = File.AppendText(fileName);
                sw.WriteLine(time.ToString() + ';' + guesses.ToString() + ';' + score.ToString() + ';' + username + ';' + DateTime.Today.ToString("d"));
                sw.Close();
            }
        }
        public static void printHighScore() {
            string fileName = "Highscore.txt";
             if (!File.Exists(fileName)) {
                Console.WriteLine("Highscore file not found");
                return;
             }
             StreamReader sr = File.OpenText(fileName);

            List<string[]> highscores = new List<string[]>();
             string? line = "";
             while ((line = sr.ReadLine()) != null) {
                 if (String.IsNullOrEmpty(line))
                    continue;
                 highscores.Add(line.Split(';'));
             }

             if (highscores.Capacity == 0) {
                 Console.WriteLine("There is no records");
                 return;
             }
                highscores.Sort(delegate(string[] x, string[] y) {
                    return -(int.Parse(x[2]).CompareTo(int.Parse(y[2]))); 
                });

            Console.WriteLine("Username\tScore\tTime\tGuesses\tDate");
            int b = 10;
            if (highscores.Count < 10) {
                b = highscores.Count;
            }
            
            for (int i = 0; i < b; i++) {
                 Console.Write((i+1).ToString() + ") " + String.Format("{0,-10}", highscores[i][3].ToString()) + "\t" + highscores[i][2] + '\t' + highscores[i][0] + "s\t" + highscores[i][1] + "g\tat " + highscores[i][4] + '\n');
             }

             sr.Close();
        }
    }
}