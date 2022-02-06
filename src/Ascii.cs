namespace memory_game {
    class Ascii {
        private static string[] logo = {
            @" _ __ ___   ___ _ __ ___   ___  _ __ _   _    __ _  __ _ _ __ ___   ___ ",
            @"| '_ ` _ \ / _ \ '_ ` _ \ / _ \| '__| | | |  / _` |/ _` | '_ ` _ \ / _ \",
            @"| | | | | |  __/ | | | | | (_) | |  | |_| | | (_| | (_| | | | | | |  __/",
            @"|_| |_| |_|\___|_| |_| |_|\___/|_|   \__, |  \__, |\__,_|_| |_| |_|\___|",
            @"                                      __/ |   __/ |                     ",
            @"                                     |___/   |___/                      "
        };
        private static string[] skull = {
            @"                                _,.-------.,_",
            @"                            ,;~'             '~;,",
            @"                          ,;                     ;,",
            @"                         ;                         ;",
            @"                        ,'                         ',",
            @"                       ,;                           ;,",
            @"                       ; ;      .           .      ; ;",
            @"                       | ;   ______       ______   ; |",
            @"                       |  ~  ,-~~~^~, | ,~^~~~-,  ~  |",
            @"                        |   |        }:{        |   |",
            @"                        |   l       / | \       !   |",
            @"                        .~  (__,.--  .^.  --.,__)  ~.",
            @"                        |     ---;' / | \ `;---     |",
            @"                         \__.       \/^\/       .__/",
            @"                          V| \                 / |V",
            @"                           | |T~\___!___!___/~T| |",
            @"                           | |`IIII_I_I_I_IIII'| | ",
            @"                           |  \,III I I I III,/  | ",
            @"                            \   `~~~~~~~~~~'    / ",
            @"                              \   .       .   /    ",
            @"                                \.    ^    ./    ",
            @"                                  ^~~~^~~~^ "

            
        };
        private static string[] cards = {
            @"          _____",
            @"         |A .  | _____",
            @"         | /.\ ||A ^  | _____",
            @"         |(_._)|| / \ ||A _  | _____",
            @"ejm98    |  |  || \ / || ( ) ||A_ _ |",
            @"         |____V||  .  ||(_'_)||( v )|",
            @"                |____V||  |  || \ / |",
            @"                       |____V||  .  |",
            @"                              |____V|",
            @""
        };
        private static string[] error = {
            @"███████ ██████  ██████   ██████  ██████  ",
            @"██      ██   ██ ██   ██ ██    ██ ██   ██ ",
            @"█████   ██████  ██████  ██    ██ ██████  ",
            @"██      ██   ██ ██   ██ ██    ██ ██   ██ ",
            @"███████ ██   ██ ██   ██  ██████  ██   ██ "
        };
        public enum drawings {
            logo,
            skull,
            cards,
        }
        public static void print(drawings draw) {
            string [] drawing = {""};
            switch (draw) {
                case drawings.logo:
                drawing = logo;
                break;
                case drawings.skull:
                drawing = skull;
                break;
                case drawings.cards:
                drawing = cards;
                break;
                default:
                drawing = error;
                break;
            }
            foreach (string line in drawing)
            {
                Console.WriteLine(line);
            }
        }
        
    
    }
}