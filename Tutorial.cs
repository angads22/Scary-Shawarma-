//v0.1.1.

using System;
using System.Threading;
using System.Media;
using static ScaryShawarmaGame.Game;

namespace ScaryShawarmaGame
{
	public static class Tutorial
	{
		public static void StartTutorial() //Only commands that run methods and print text
		{
			PlayVhsGlitch();
			VHS_Sound?.PlayLooping();

			Console.WriteLine("======================================================");
			Console.WriteLine("              Scary Shawarma Night Shift              ");
			Console.WriteLine("======================================================");
			Console.WriteLine("                    May 17th, 1985                    ");

			Thread.Sleep(40);
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine();
			TypeWriter("Welcome to your automated training segment. This VHS "); //start
			TypeWriter("tape will outline standard protocols to follow.");
			TypeWriter("Survival depends entirely on your compliance during ");
			TypeWriter("this shift.");
			Console.WriteLine(Art.logo);
			Console.WriteLine();

			TypeWriter("Press any key if you want to begin employment.");
			Console.ReadKey();

			Console.Clear();
			Console.ResetColor();
			PlayVhsGlitch();
			VHS_Sound?.PlayLooping();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.DarkBlue;

			TypeWriter("Segment 1: Customer Interaction"); //segment 1 - customer interaction
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			TypeWriter("During your night shift, you will encounter a variety of");
			TypeWriter("customers.");

			Console.ResetColor();
			Console.WriteLine(Art.normalPerson);

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine();
			TypeWriter("Regardless of their appearance, you must address their");
			TypeWriter("needs quickly and with absolute precision.");

			Thread.Sleep(2000);

			VHS_Sound?.Stop();
			Quick_Scream?.Play();

			Console.Clear();
			Console.WriteLine("\x1b[3J");


			Console.ForegroundColor = ConsoleColor.DarkBlue; // Basically replacing all text here to reprint everything - with the exception of the jumpscare
			Console.WriteLine("Segment 1: Customer Interaction");
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("During your night shift, you will encounter a variety of");
			Console.WriteLine("customers.");

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(Art.scaryPerson); // The scary art replaces the normal art instantly

			Console.WriteLine();
			Console.WriteLine("Regardless of their appearance, you must address their");
			Console.WriteLine("needs quickly and with absolute precision.");

			Thread.Sleep(2000);
			Quick_Scream?.Stop();

			VHS_Sound?.PlayLooping();

			Console.Clear();
			Console.WriteLine("\x1b[3J");
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			TypeWriter("Address the situation and choose the best option possible.");
			TypeWriter("Failure to do so will result in consequences or potential");

			Console.SetCursorPosition(0, Console.CursorTop - 1);
			Console.ForegroundColor = ConsoleColor.Red;
			VHS_Distort?.Play();
			Console.WriteLine(@"
______  _____  ___ _____ _   _ 
|  _  \|  ___|/ _ \_   _| | | |
| | | || |__ / /_\ \| | | |_| |
| | | ||  __||  _  || | |  _  |
| |/ / | |___| | | || | | | | |
|___/  \____/\_| |_/\_/ \_| |_/
		");

			Thread.Sleep(1500);
			VHS_Distort?.Stop();
			VHS_Sound?.PlayLooping();
			Console.WriteLine();
			Satisfaction();
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			TypeWriter("A satisfaction bar will be displayed above. This tracks");
			TypeWriter("customer satisfaction. If it reaches 0%, you lose...");
			Thread.Sleep(2000);

			PlayVhsGlitch();
			VHS_Sound?.PlayLooping();
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			TypeWriter("Segment 2: Anomalous Creatures"); //segment 2 - anomalous creatures
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			TypeWriter("You will face happy, normal customers as well as terrifying, anomalous creatures.");
			TypeWriter("The camera system will be accessible at all times. Enter 'C' to enable it.");
			Thread.Sleep(2000);

			PlayVhsGlitch();
			Console.Clear();
			Console.WriteLine("\x1b[3J");



			Quick_Scream?.Stop();
			VHS_Sound?.Stop();
			VHS_Distort?.Stop();
			//Console.ReadKey();
		}
	}
}