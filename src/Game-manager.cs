namespace memory_game
{
    class Program
    {

        static void Main(string[] args)
        {
           Ascii.print(Ascii.drawings.logo);

           bool play = true;
           while (play) {
            Timer timer = new Timer();
            timer.start();
            long score = 0;
            int attemps = 0;
            Game game = new Game();
            while(game._current_state == Game.State.Play)
                if (game.MakeGuess())
                    score++;
            timer.stop();
            switch(game._current_difficulty) {
                case Game.Difficulty.Easy:
                    score += game.GetGuesses();
                    score -= timer.secondsElapsed() / 10;
                    attemps = 10 - game.GetGuesses();
                    if (score < 0)
                        score = 0;
                    break;
                case Game.Difficulty.Hard:
                    score += game.GetGuesses();
                    score -= timer.secondsElapsed() / 20;
                    score *= 2;
                    attemps = 15 - game.GetGuesses();
                    if (score < 0)
                        score = 0;
                    break;
            }        

            
           
            if (score > 0) {
            Console.WriteLine("Game over! Your score is: " + score);
            Console.WriteLine("You made it for " + timer.secondsElapsed() + " seconds and " + attemps + " attemps!");
            File_manager.Save_High_Score(timer.secondsElapsed(), game.GetGuesses(), score);
            }
           
            File_manager.printHighScore();
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