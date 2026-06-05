//sharmaos v0.2.0.
// ts shawarma maxxing
// Scary Shawarma Simulator - a text based decision game
// Inspired by the Roblox game "Scary Shawarma Simulator".
// Built using only: arrays, if/else, for, while, do-while, methods, console I/O.
// ASCII art is loaded from Art.cs (compiled together with this file).
using System;
using System.Threading;
using System.Media;

namespace ScaryShawarmaGame {

class Game {

	// variables (to be used by many methods)
	public static Random glitch = new Random();
	public static int satisfaction = 4; //4 = 100%, 3 = 75%, 2 = 50%, 1 = 25%, 0 = 0% (game over)
	public static SoundPlayer VHS_Sound = new SoundPlayer("VHS_Sound.wav");
    public static SoundPlayer Quick_Scream = new SoundPlayer("Quick_Scream.wav");
	public static SoundPlayer VHS_Static = new SoundPlayer("VHS_Static.wav");
	public static SoundPlayer VHS_Distort = new SoundPlayer("VHS_Distort.wav");

	// VHS Glitch Effect
	public static void PlayVhsGlitch()
	{
		char[] glitchSymbols = {' ', '-', '=', '~', '#', '%', '░', '▒', '█'}; // characters for vhs glitch effect
		VHS_Static.Play();
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
			Thread.Sleep(150);
		}
		Console.Clear();
		Console.WriteLine("\x1b[3J"); //clear whole console - make previous terminal output inaccessible by scrolling
		VHS_Static.Stop();
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
				Console.WriteLine("Customer Satisfaction: 0%");		
			}

			else
			{
				for (int i = 0; i < satisfaction; i++)
				{
					Console.Write("█████  ");
				}
				Console.Write("         Customer Satisfaction: " + (satisfaction * 25) + "%");
				Console.WriteLine();
			}
		}
	static void Main() {

		Tutorial.StartTutorial();
	}
}
}