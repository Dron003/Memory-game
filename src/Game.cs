namespace memory_game {
    class Game
     {
        public enum State {
            Play,
            Pause,
            Score
        }
        public enum Difficulty {
            Easy = 4,
            Hard = 8
        }

        public Difficulty _current_difficulty;
        public State _current_state;
        private List<string> _words;
        private List<string> _words_in_turn;
        private List<string> _words_to_guess;

        private bool[] coveredMatrixA;
        private bool[] coveredMatrixB;
        private int guesses = 2;
        public Game() {
            _words = File_manager.Load_words();
            _current_difficulty = ChooseDifficulty();
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
        private void DrawBoard() {
            Console.WriteLine("--------------------------");
            if (_current_difficulty == Difficulty.Easy)
                Console.WriteLine("Level: easy");
            else
                Console.WriteLine("Level: hard");
            Console.WriteLine("Guess chances: " + guesses);

            Console.Write("\n\t  ");


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
            Console.WriteLine("\n--------------------------");
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
        public bool MakeGuess() {
            DrawBoard();
            // Returns true if guess is good
            Console.Write("Chose column from first row: ");
            string? userInputA;
            while (true)
            {
                userInputA = Console.ReadLine();
                if (userInputA == null) {
                    continue;
                }
                if (userInputA.Count() != 2) {
                    Console.WriteLine("Your input is wrong. Type for example A2");
                    continue;
                }
                if (userInputA[0] != 'A') {
                    Console.WriteLine("You have too chose from A row first. Type A2 for example");
                    continue;
                }
                if (!Char.IsDigit(userInputA[1])) {
                    Console.WriteLine("Your second character is not a digit. Type A2 for example");
                    continue;
                }
                if (int.Parse(userInputA[1].ToString()) <= 0 || (int.Parse(userInputA[1].ToString()) > (int) _current_difficulty)) {
                    Console.WriteLine("There is no such column \"" + userInputA[1] + "\". Type A2 for example");
                    continue;
                }
                if (coveredMatrixA[int.Parse(userInputA[1].ToString()) - 1] == true) {
                    Console.WriteLine("That cell (A" + userInputA[1] + ") uncovered already. Chose another one");
                    continue;
                }
                break;
            }

            Console.Clear();
            coveredMatrixA[int.Parse(userInputA[1].ToString()) - 1] = true;
            DrawBoard();
            Console.Write("Chose column from second row: ");
            
            string? userInputB;
            while (true)
            {
                userInputB = Console.ReadLine();
                if (userInputB == null) {
                    continue;
                }
                if (userInputB.Count() != 2) {
                    Console.WriteLine("Your input is wrong. Type for example B2");
                    continue;
                }
                if (userInputB[0] != 'B') {
                    Console.WriteLine("You have too chose from B row now. Type B2 for example");
                    continue;
                }
                if (!Char.IsDigit(userInputB[1])) {
                    Console.WriteLine("Your second character is not a digit. Type B2 for example");
                    continue;
                }
                if (int.Parse(userInputB[1].ToString()) <= 0 || (int.Parse(userInputB[1].ToString()) > (int) _current_difficulty)) {
                    Console.WriteLine("There is no such column \"" + userInputB[1] + "\". Type B2 for example");
                    continue;
                }
                if (coveredMatrixB[int.Parse(userInputB[1].ToString()) - 1] == true) {
                    Console.WriteLine("That cell (B" + userInputB[1] + ") uncovered already. Chose another one");
                    continue;
                }
                break;
            }

            Console.Clear();
            coveredMatrixB[int.Parse(userInputB[1].ToString()) - 1] = true;
            DrawBoard();


            if (_words_in_turn[int.Parse(userInputA[1].ToString()) - 1] == _words_to_guess[int.Parse(userInputB[1].ToString()) - 1]) {
                if (CheckWin())
                    _current_state = State.Score;
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
                return true;
            }
            else {
                Console.WriteLine("Your guess is wrong!");
                System.Threading.Thread.Sleep(1500);
                coveredMatrixA[int.Parse(userInputA[1].ToString()) - 1] = false;
                coveredMatrixB[int.Parse(userInputB[1].ToString()) - 1] = false;
                Console.Clear();
                guesses--;
                if (guesses == 0)
                    _current_state = State.Score;
                return false;
            }
        }

        public int GetGuesses() {
            return guesses;
        }
        private bool CheckWin() {
            bool win = true;
            // If there still uncovered cells then game must go on

            foreach(bool cell in coveredMatrixA) {
                if(!cell) {
                    win = false;
                    break;
                }
            }
            foreach(bool cell in coveredMatrixB) {
                if(!cell) {
                    win = false;
                    break;
                }
            }
            return win;
        }
    }
}
