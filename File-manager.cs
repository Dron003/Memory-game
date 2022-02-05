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
    }
}