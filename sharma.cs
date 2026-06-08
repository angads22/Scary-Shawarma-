//sharmaos v0.3.0.
// ts shawarma maxxing
// Scary Shawarma Simulator - a text based decision game
// Inspired by the Roblox game "Scary Shawarma Simulator".
// Built using only: arrays, if/else, for, while, do-while, methods, console I/O.
// ASCII art is loaded from Art.cs (compiled together with this file).
using System;
using System.Threading;
using System.Media;
using System.Runtime.Versioning;

[assembly: SupportedOSPlatform("windows")]

namespace ScaryShawarmaGame
{

	class Game
	{

		// variables (to be used by many methods)
		public static Random glitch = new Random(); // random generator for vhs glitch effect
		public static int customer = 0; // number of customers served
		public static int money = 0; // money earned
		public static string time; // 
		public static bool camera = false; // whether the night vision camera is on or off
		public static int satisfaction = 4; //4 = 100%, 3 = 75%, 2 = 50%, 1 = 25%, 0 = 0% (game over)
		public static SoundPlayer? VHS_Sound;
		public static SoundPlayer? Quick_Scream;
		public static SoundPlayer? VHS_Static;
		public static SoundPlayer? VHS_Distort;

		static Game()
		{
			if (OperatingSystem.IsWindows())
			{
				VHS_Sound = new SoundPlayer("VHS_Sound.wav");
				Quick_Scream = new SoundPlayer("Quick_Scream.wav");
				VHS_Static = new SoundPlayer("VHS_Static.wav");
				VHS_Distort = new SoundPlayer("VHS_Distort.wav");
			}
		}

		// VHS Glitch Effect
		public static void PlayVhsGlitch()
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
					Console.WriteLine("█████                           	                Satisfaction: 25%");
				}
				else if (satisfaction == 2)
				{
					Console.WriteLine("█████  █████                                      Satisfaction: 50%");
				}
				else if (satisfaction == 3)
				{
					Console.WriteLine("█████  █████  █████                               Satisfaction: 75%");
				}
				else if (satisfaction == 4)
				{
					Console.WriteLine("█████  █████  █████  █████                        Satisfaction: 100%");
				}
				Console.ResetColor();
			}
		}
		static void Main()
		{
			VHS_Sound?.Load();
			Console.Clear();
			Console.WriteLine("\x1b[3J");
			TypeWriter("Do you want to start the tutorial?"); //intro
			Console.WriteLine("Enter Number: 1. Yes            2. No");
			int choice = int.Parse(Console.ReadLine()!);
			if (choice == 1)
			{
				Tutorial.StartTutorial();
				Console.Clear();
				Console.WriteLine(@"
█▀▀▀ ▀▀█▀▀ █▀▀█ █▀▀█ ▀▀█▀▀ ▀█▀ █▄  █ █▀▀▀   █▀▀▀ █  █ ▀█▀ █▀▀▀ ▀▀█▀▀
▀▀▀█   █   █▄▄█ █▄▄▀   █    █  █ █ █ █ ▀█   ▀▀▀█ █▀▀█  █  █▀▀▀   █            
▀▀▀▀   ▀   ▀  ▀ ▀ ▀▀   ▀   ▀▀▀ ▀  ▀▀ ▀▀▀▀   ▀▀▀▀ ▀  ▀ ▀▀▀ ▀      ▀  ▀ ▀ ▀");
				Thread.Sleep(3000);
				Random rand = new Random();
				int encounterChance = rand.Next(1, 101);

			//StartGame.Vampire();

			if (encounterChance <= 60)
			{
				StartGame.Normal();
			}
			else
			{
				StartGame.Abnormal();
			}
		}
	}
}
}