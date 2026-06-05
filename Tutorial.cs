//v0.1.0

using System;
using System.Threading;
using System.Media;
using static ScaryShawarmaGame.Game;

namespace ScaryShawarmaGame
{
    public static class Tutorial
    {
        // FIX: We wrapped everything inside this public static method container
        // We use 'ref' so it continuously updates your game's main satisfaction tracker!
        public static void StartTutorial()
        {
            PlayVhsGlitch();
		VHS_Sound.PlayLooping();

		Console.WriteLine("======================================================");
		Console.WriteLine("              Scary Shawarma Night Shift              ");
		Console.WriteLine("======================================================");
		Thread.Sleep(40);
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		TypeWriter("Welcome to your automated training segment. This VHS "); //start
		TypeWriter("tape will outline standard protocols to follow.");
		TypeWriter("Survival depends entirely on your compliance during ");
		TypeWriter("this shift.");
		TypeWriter("Press any key if you want to begin employment.");

		Console.ReadKey();
		Console.Clear();

		Console.ResetColor();
		PlayVhsGlitch();
		VHS_Sound.PlayLooping();
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.DarkBlue;
		TypeWriter("Segment 1: Customer Interaction"); //segment 1 - customer interaction
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		TypeWriter("During your night shift, you will encounter a variety of");
		TypeWriter("customers.");
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		TypeWriter("Failing to fulfill an order correctly will directly");
		TypeWriter("impact your metrics.");

		Console.ResetColor();
		int pointer = Console.CursorTop;
		Console.WriteLine(Art.normalPerson);

		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine();
		TypeWriter("Regardless of their appearance, you must address their");
		TypeWriter("needs quickly and with absolute precision.");
		
		Thread.Sleep(2000);
		int artTopLeft = Console.CursorTop - 39;
        if (artTopLeft < 0) artTopLeft = 0; // Guard against crashing if window is small
        Console.SetCursorPosition(0, artTopLeft);

		VHS_Sound.Stop();
		Quick_Scream.Play();
        Console.WriteLine(Art.scaryPerson);
		Thread.Sleep(2000);
		Quick_Scream.Stop();
		VHS_Sound.PlayLooping();

		Console.Clear();
		Console.WriteLine("\x1b[3J");
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		TypeWriter("Address the situation and choose the best option possible.");
		TypeWriter("Failure to do so will result in consequences or potential");

		Console.SetCursorPosition(0, Console.CursorTop - 1);
		Console.ForegroundColor = ConsoleColor.Red;
		VHS_Distort.Play();
		Console.WriteLine(@"
______  _____  ___ _____ _   _ 
|  _  \|  ___|/ _ \_   _| | | |
| | | || |__ / /_\ \| | | |_| |
| | | ||  __||  _  || | |  _  |
| |/ / | |___| | | || | | | | |
|___/  \____/\_| |_/\_/ \_| |_/
		");

		Thread.Sleep(1500);
		VHS_Distort.Stop();
		VHS_Sound.PlayLooping();
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Cyan;
		Satisfaction();
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		TypeWriter("A satisfaction bar will be displayed above. This tracks");
		TypeWriter("customer satisfaction. If it reaches 0%, you will be fired...");
		Thread.Sleep(2000);

		PlayVhsGlitch();
		VHS_Sound.PlayLooping();
		Console.ForegroundColor = ConsoleColor.DarkBlue;
		TypeWriter("Segment 2: Anomalous Creatures"); //segment 2 - anomalous creatures
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.DarkGreen;
		TypeWriter("You will face happy, normal customers as well as terrifying, anomalous creatures.");
		TypeWriter("The same protocols apply to both. Do not let your guard down.");
		TypeWriter("The camera system will be accessible at all times. Make sure to use it well.");


		Quick_Scream.Stop();
		VHS_Sound.Stop();
        //Console.ReadKey();
        }
    }
}