namespace memory_game
{
    class Program
    {
        static void Main(string[] args)
        {
           bool play = true;
           while (play) {
            int score = 0;
            Game game = new Game();
            while(game._current_state == Game.State.Play)
                if (game.MakeGuess())
                    score++;
            switch(game._current_difficulty) {
                case Game.Difficulty.Easy:
                    score += game.GetGuesses();
                    break;
                case Game.Difficulty.Hard:
                    score += game.GetGuesses();
                    score *= 2;
                    break;
            }        
            Console.WriteLine("Game over! Your score is: " + score);
            Console.WriteLine("Want to play again? (Y\\N)");
            switch(Console.ReadLine()) {
                case "Y":
                case "y":
                continue;
                default:
                Console.WriteLine("Goodbye!");
                play = false;
                break;
            }
           }
        }
    }
}