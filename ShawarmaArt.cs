// Scary Shawarma Simulator - ASCII art module
// Linked to Shawarma.C# by being compiled together (csc Shawarma.C# ShawarmaArt.cs)
// or by being placed in the same .NET project folder.
// Only uses arrays, methods and output (and a for loop) to stay within the game's constraints.

static class ShawarmaArt
{
    // Each piece of art is stored as a string array - one element per line - so the
    // print methods can just loop and write them out.

    static string[] title =
    {
        @"  ____  ____    _    ______   __ ",
        @" / ___||  _ \  / \  |  _ \ \ / / ",
        @" \___ \| |_) |/ _ \ | |_) \ V /  ",
        @"  ___) |  _ <| ___ \|  _ < | |   ",
        @" |____/|_| \_\_/ \_\|_| \_\|_|   ",
        @"   SHAWARMA   SIMULATOR          ",
        @"        ~ Night Shift ~          "
    };

    static string[] shawarma =
    {
        @"        .-""""""""-.        ",
        @"      .'  .--.  '.      ",
        @"     /   /    \   \     ",
        @"     |  |  ()  |  |     ",
        @"     \   \    /   /     ",
        @"      '. '--' .'        ",
        @"      .'------'.        ",
        @"     /__________\       ",
        @"      ||      ||        ",
        @"      ||      ||        ",
        @"      \\______//        "
    };

    static string[] ghost =
    {
        @"     .-.            ",
        @"    (o o)  boo      ",
        @"    | O \           ",
        @"     \   \          ",
        @"      `~~~'         "
    };

    static string[] skull =
    {
        @"      _____         ",
        @"    ,'     `.       ",
        @"   /  o   o  \      ",
        @"  |  .--^--.  |     ",
        @"   \  '---'  /      ",
        @"    `.     ,'       ",
        @"      `~~~'         "
    };

    static string[] shawarmaMan =
    {
        @"        /\_/\           ",
        @"       (  -.- )         ",
        @"       /  -O- \         ",
        @"      |   /|\  |        ",
        @"      |  / | \ |        ",
        @"       \_/   \_/        ",
        @"       I AM HUNGRY      "
    };

    static string[] police =
    {
        @"    .---.       ",
        @"   /POLICE\      ",
        @"   | o o |      ",
        @"   |  >  |      ",
        @"   |_____|      ",
        @"  /|=====|\     ",
        @"   |     |      "
    };

    static string[] money =
    {
        @"  __________________  ",
        @" |  ____________  | ",
        @" | |####  $   ##| | ",
        @" | |##  ($)   ##| | ",
        @" | |####     ##| | ",
        @" | |____________| | ",
        @" |________________| "
    };

    static string[] gameOver =
    {
        @"   ____    _    __  __ _____    _____     _______ ____  ",
        @"  / ___|  / \  |  \/  | ____|  / _ \ \   / / ____|  _ \ ",
        @" | |  _  / _ \ | |\/| |  _|   | | | \ \ / /|  _| | |_) |",
        @" | |_| |/ ___ \| |  | | |___  | |_| |\ V / | |___|  _ < ",
        @"  \____/_/   \_\_|  |_|_____|  \___/  \_/  |_____|_| \_\"
    };

    static string[] win =
    {
        @"   __        _____ _   _ _   _ _____ ____  _ ",
        @"   \ \      / /_ _| \ | | \ | | ____|  _ \| |",
        @"    \ \ /\ / / | ||  \| |  \| |  _| | |_) | |",
        @"     \ V  V /  | || |\  | |\  | |___|  _ <|_|",
        @"      \_/\_/  |___|_| \_|_| \_|_____|_| \_(_)"
    };

    // Generic print helper - kept here so the main file doesn't need to know
    // how art is stored.
    static void PrintArt(string[] art)
    {
        for (int i = 0; i < art.Length; i = i + 1)
        {
            Console.WriteLine(art[i]);
        }
    }

    // Public entry points the main game calls.
    public static void PrintTitle()        { PrintArt(title); }
    public static void PrintShawarma()     { PrintArt(shawarma); }
    public static void PrintGhost()        { PrintArt(ghost); }
    public static void PrintSkull()        { PrintArt(skull); }
    public static void PrintShawarmaMan()  { PrintArt(shawarmaMan); }
    public static void PrintPolice()       { PrintArt(police); }
    public static void PrintMoney()        { PrintArt(money); }
    public static void PrintGameOver()     { PrintArt(gameOver); }
    public static void PrintWin()          { PrintArt(win); }
}
