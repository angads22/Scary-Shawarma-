//sharmaos v1.3.0.
// Scary Shawarma Simulator - a text based decision game

using System;
using System.Threading;
using System.Media;
using System.Runtime.Versioning;

[assembly: SupportedOSPlatform("windows")] //declare windows support (this is for sound only)

namespace ScaryShawarmaGame
{

	class Game
	{

		// variables (to be used by many methods)
		public static bool gameActive = true; // keep running game or stop
		public static Random glitch = new Random(); // random generator for vhs glitch effect
		public static int customer = 0; // number of customers served
		public static int money = 0; // money earned
		public static string time = ""; // 
		public static bool camera = false; // whether the night vision camera is on or off
		public static int satisfaction = 4; //4 = 100%, 3 = 75%, 2 = 50%, 1 = 25%, 0 = 0% (game over)
		public static SoundPlayer? VHS_Sound;
		public static SoundPlayer? Quick_Scream;
		public static SoundPlayer? VHS_Static;
		public static SoundPlayer? VHS_Distort;
		public static SoundPlayer? Intro_Music;
		public static SoundPlayer? Game_Music;

		static Game()
		{
			if (OperatingSystem.IsWindows()) //Only load sounds if OS is Windows.
			{
				VHS_Sound = new SoundPlayer("VHS_Sound.wav");
				Quick_Scream = new SoundPlayer("Quick_Scream.wav");
				VHS_Static = new SoundPlayer("VHS_Static.wav");
				VHS_Distort = new SoundPlayer("VHS_Distort.wav");
				Intro_Music = new SoundPlayer("Intro_Music.wav");
				Game_Music = new SoundPlayer("Game_Music.wav");
			}
		}

		// VHS Glitch Effect
		public static void PlayVhsGlitch() //VHS Glitch effect prints random order characters for effect
		{
			char[] glitchSymbols = { ' ', '-', '=', '~', '#', '%', '░', '▒', '█' }; // characters for vhs glitch effect
			VHS_Static?.Play();
			Console.ResetColor();
			for (int flash = 0; flash < 3; flash++)
			{
				Console.Clear();
				for (int line = 0; line < 60; line++)
				{
					for (int column = 0; column < 54; column++)
					{
						int glitchScreen = glitch.Next(glitchSymbols.Length);
						Console.Write(glitchSymbols[glitchScreen]);
					}
					Console.WriteLine();
				}
				Thread.Sleep(120);
			}
			Console.Clear();
			Console.WriteLine("\x1b[3J"); //clear whole console - make previous terminal output inaccessible by scrolling
			VHS_Static?.Stop();
		}

		public static void TypeWriter(string input) // https://logicandchaos.itch.io/endless-prose/devlog/488908/animate-text-in-c-console-applications-a-step-by-step-tutorial
		{
			char[] letters = input.ToCharArray();
			foreach (char c in letters) // break string into characters and print one by one with a delay to create a typewriter effect
			{
				Console.Write(c);
				Console.Out.Flush();

				if (c == '.' || c == '?' || c == '!')
				{
					Thread.Sleep(180);
				}
				else
				{
					Thread.Sleep(45);
				}
			}
			Console.WriteLine();
		}

		public static void Satisfaction() // satisfaction bar
		{
			if (satisfaction == 0)
			{
				//end game		
			}

			else
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				if (satisfaction == 1)
				{
					Console.WriteLine("█████                           	                  Satisfaction: 25%");
				}
				else if (satisfaction == 2)
				{
					Console.WriteLine("█████  █████                                       Satisfaction: 50%");
				}
				else if (satisfaction == 3)
				{
					Console.WriteLine("█████  █████  █████                                Satisfaction: 75%");
				}
				else if (satisfaction == 4)
				{
					Console.WriteLine("█████  █████  █████  █████                        Satisfaction: 100%");
				}
				Console.ResetColor();
			}
		}

		public static void Win()
		{
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine(@"
 ██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗ ██╗ ███╗   ██╗ ██╗
 ╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║ ██║ ████╗  ██║ ██║
  ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║ ██║ ██╔██╗ ██║ ██║
   ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║ ██║ ██║╚██╗██║ ╚═╝
    ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝ ██║ ██║ ╚████║ ██╗
    ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝  ╚═╝ ╚═╝  ╚═══╝ ╚═╝");
			Thread.Sleep(3000);
			StartGame.FinalMessage();
		}
		static void Main() //Logic for starting tutorial, and random customer order. "Master" method.
		{
			VHS_Sound?.Load();
			Console.Clear();
			Console.WriteLine("\x1b[3J");
			Intro_Music?.PlayLooping(); //Intro Music + Play Screen (aka menu)
			Console.WriteLine(@"
================================================================================
||                                                                            ||
||                 ███████╗ ██████╗ █████╗ ██████╗ ██╗   ██╗                  ||
||                 ██╔════╝██╔════╝██╔══██╗██╔══██╗╚██╗ ██╔╝                  ||
||                 ███████╗██║     ███████║██████╔╝ ╚████╔╝                   ||
||                 ╚════██║██║     ██╔══██║██╔══██╗  ╚██╔╝                    ||
||                 ███████║╚██████╗██║  ██║██║  ██║   ██║                     ||
||                 ╚══════╝ ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝                     ||
||                                                                            ||
||   ███████╗██╗  ██╗ █████╗ ██╗    ██╗ █████╗ ██████╗ ███╗   ███╗ █████╗     ||
||   ██╔════╝██║  ██║██╔══██╗██║    ██║██╔══██╗██╔══██╗████╗ ████║██╔══██╗    ||
||   ███████╗███████║███████║██║ █╗ ██║███████║██████╔╝██╔████╔██║███████║    ||
||   ╚════██║██╔══██║██╔══██║██║███╗██║██╔══██║██╔══██╗██║╚██╔╝██║██╔══██║    ||
||   ███████║██║  ██║██║  ██║╚███╔███╔╝██║  ██║██║  ██║██║ ╚═╝ ██║██║  ██║    ||
||   ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝ ╚══╝╚══╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝  ╚═╝    ||
||                                                                            ||
||           ██╗  ██╗  ██████╗  ██████╗  ██████╗  ██████╗  ██████╗            ||
||           ██║  ██║ ██╔═══██╗ ██╔══██╗ ██╔══██╗██╔═══██╗ ██╔══██╗           ||
||           ███████║ ██║   ██║ ██████╔╝ ██████╔╝██║   ██║ ██████╔╝           ||
||           ██╔══██║ ██║   ██║ ██╔══██╗ ██╔══██╗██║   ██║ ██╔══██╗           ||
||           ██║  ██║ ╚██████╔╝ ██║  ██║ ██║  ██║╚██████╔╝ ██║  ██║           ||
||           ╚═╝  ╚═╝  ╚═════╝  ╚═╝  ╚═╝ ╚═╝  ╚═╝ ╚═════╝  ╚═╝  ╚═╝           ||
||                                                                            ||
================================================================================
||                    [1] START NIGHT SHIFT    [2] TUTORIAL                   ||
================================================================================
			");
			//TypeWriter("Do you want to start the tutorial?"); //intro
			Console.WriteLine("Enter Number: ");
			int choice = int.Parse(Console.ReadLine()!);
			if (choice == 2)
			{
				Intro_Music?.Stop();
				Tutorial.StartTutorial();
			}

			Console.Clear();
			Intro_Music?.Stop(); //if user enters 1, skip tutorial.
			Console.WriteLine(@"
█▀▀▀ ▀▀█▀▀ █▀▀█ █▀▀█ ▀▀█▀▀ ▀█▀ █▄  █ █▀▀▀   █▀▀▀ █  █ ▀█▀ █▀▀▀ ▀▀█▀▀
▀▀▀█   █   █▄▄█ █▄▄▀   █    █  █ █ █ █ ▀█   ▀▀▀█ █▀▀█  █  █▀▀▀   █            
▀▀▀▀   ▀   ▀  ▀ ▀ ▀▀   ▀   ▀▀▀ ▀  ▀▀ ▀▀▀▀   ▀▀▀▀ ▀  ▀ ▀▀▀ ▀      ▀  ▀ ▀ ▀");

			Thread.Sleep(3000);
			Random rand = new Random();
			Game_Music?.PlayLooping();

			for (int i = 0; i < 18; i++)
			{

				if (!gameActive)
				{
					Game_Music?.Stop();
					break;
				}

				int encounterChance = rand.Next(1, 101);

				if (encounterChance <= 50)
				{
					// 1 to 60 (60% chance)
					StartGame.Normal();
				}
				else if (encounterChance <= 80)
				{
					StartGame.Abnormal();
				}
				else
				{
					StartGame.Vampire();
				}

				if (customer == 18)
				{
					Console.Clear();
					Console.WriteLine("\x1b[3J");
					Game_Music?.Stop();
					Win();
					break;
				}

			}

		}
	}
}