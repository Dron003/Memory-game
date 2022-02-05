namespace memory_game {
    class Game {
        private enum State {
            Play,
            Pause
        }
        private enum Difficulty {
            Easy = 4,
            Hard = 8
        }

        private Difficulty _current_difficulty;
        private State _current_state;
        private List<string> _words;
        private List<string> _words_in_turn;
        private List<string> _words_to_guess;

        private bool[] coveredMatrixA;
        private bool[] coveredMatrixB;
        private int guesses;
        public Game() {
            _words = File_manager.Load_words();
            _current_difficulty = Difficulty.Hard;
            // ChooseDifficulty();
            _current_state = State.Play;
            _words_in_turn = GetWords(_current_difficulty);
            _words_to_guess = RandomiseWords(_words_in_turn);

            coveredMatrixA = new bool[((int) _current_difficulty)];
            coveredMatrixB = new bool[((int) _current_difficulty)];
        }

        private Difficulty ChooseDifficulty() {
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("1) Easy - 4 word pairs and 10 chances");
            Console.WriteLine("2) Hard - 8 word pairs and 15 chances");
            string? playerChoice = Console.ReadLine();

            switch (playerChoice)
            {
                case "1":
                    System.Console.WriteLine("You've chosen easy difficulty");
                    guesses = 10;
                    return Difficulty.Easy;
                case "2":
                    System.Console.WriteLine("You've chosen hard difficulty");
                    guesses = 15;
                    return Difficulty.Hard;
                default:
                    System.Console.WriteLine("There is no option: " + playerChoice + " easy difficulty will be chosen");
                    return Difficulty.Easy;
            }
            
        }
        public void DrawBoard() {
            Console.WriteLine("---------------");
            if (_current_difficulty == Difficulty.Easy)
                Console.WriteLine("Level: easy");
            else
                Console.WriteLine("Level: hard");
            Console.WriteLine("Guess chances: " + guesses);

            Console.Write("\t  ");


            // Spaces for uncovered leters
            for (int i = 0; i < (int) _current_difficulty; i++)
            {
                if (!coveredMatrixA[i])
                    Console.Write((i + 1) + new String(' ', 1));
                else
                    Console.Write((i + 1) + new String(' ', _words_in_turn[i].Count()));
            }
            Console.Write("\n\tA ");
            
            // Words if uncovered otherwise X
            for (int i = 0; i < coveredMatrixA.Count(); i++) {
                if (!coveredMatrixA[i])
                    Console.Write("X ");
                else
                    Console.Write(_words_in_turn[i] + ' ');
            }
            Console.Write("\n\tB ");
            for (int i = 0; i < coveredMatrixB.Count(); i++) {
                if (!coveredMatrixB[i])
                    Console.Write("X ");
                else
                    Console.Write(_words_to_guess[i] + ' ');
            }
        }
        private List<string> GetWords(Difficulty difficulty) {
            Random rnd = new Random();
            List<int> rnd_words = new List<int>();
            int max_words = (int) difficulty;
            

            // Generating set of random words for game
            int number = 0;
            while(rnd_words.Count < max_words) {

                // Making shure that words are unique
                do {
                    number = rnd.Next(0, _words.Count());
                } while(rnd_words.Contains(number));

                rnd_words.Add(number);
            }


            List<string> generated_words = new List<string>();
            foreach (int num in rnd_words)
            {
                generated_words.Add(_words[num]);
            }


            return generated_words;
        } 
        private List<string> RandomiseWords(List<string> words) {
            Random rnd = new Random();
            Console.Write('\n');
            return words.OrderBy(item => rnd.Next()).ToList();
        }
    }
}
