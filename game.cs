//v1.0.0

using System;
using System.Threading;
using System.Media;
using static ScaryShawarmaGame.Game;

namespace ScaryShawarmaGame
{
    public static class StartGame
    {
        static int choice;
        public static int GetShawarmaPrice() //method that returns value
        {
            return 15;
        }

        public static void GameOver1() //if satisfaction drops to zero
        {
            Game_Music?.Stop();
            gameActive = false;
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Satisfaction has dropped to zero...");
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine(@"
 ██╗   ██╗ ██████╗ ██╗   ██╗    ██╗      ██████╗  ██████╗███████╗    ██╗ 
 ╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║     ██╔═══██╗██╔════╝██╔════╝    ██║ 
  ╚████╔╝ ██║   ██║██║   ██║    ██║     ██║   ██║╚█████╗ █████╗      ██║ 
   ╚██╔╝  ██║   ██║██║   ██║    ██║     ██║   ██║ ╚═══██╗██╔══╝      ╚═╝ 
    ██║   ╚██████╔╝╚██████╔╝    ███████╗╚██████╔╝██████╔╝███████╗    ██╗ 
    ╚═╝    ╚═════╝  ╚═════╝     ╚══════╝ ╚═════╝ ╚═════╝ ╚══════╝    ╚═╝");
            Thread.Sleep(3000);
            FinalMessage();
        }

        public static void GameOver2() //if player dies to an anomaly
        {
            Game_Music?.Stop();
            gameActive = false;
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
 ██╗   ██╗ ██████╗ ██╗   ██╗    ██████╗ ██╗███████╗██████╗  ██╗
 ╚██╗ ██╔╝██╔═══██╗██║   ██║    ██╔══██╗██║██╔════╝██╔══██╗ ██║
  ╚████╔╝ ██║   ██║██║   ██║    ██║  ██║██║█████╗  ██║  ██║ ██║
   ╚██╔╝  ██║   ██║██║   ██║    ██║  ██║██║██╔══╝  ██║  ██║ ╚═╝
    ██║   ╚██████╔╝╚██████╔╝    ██████╔╝██║███████╗██████╔╝ ██╗
    ╚═╝    ╚═════╝  ╚═════╝     ╚═════╝ ╚═╝╚══════╝╚═════╝  ╚═╝");
            Thread.Sleep(3000);
            FinalMessage();
        }
        public static void Time() //time tracker
        {
            switch (customer)
            {
                case 0: time = "10:00 PM"; break;
                case 1: time = "10:20 PM"; break;
                case 2: time = "10:40 PM"; break;
                case 3: time = "11:00 PM"; break;
                case 4: time = "11:20 PM"; break;
                case 5: time = "11:40 PM"; break;
                case 6: time = "12:00 AM"; break;
                case 7: time = "12:20 AM"; break;
                case 8: time = "12:40 AM"; break;
                case 9: time = "1:00 AM"; break;
                case 10: time = "1:20 AM"; break;
                case 11: time = "1:40 AM"; break;
                case 12: time = "2:00 AM"; break;
                case 13: time = "2:20 AM"; break;
                case 14: time = "2:40 AM"; break;
                case 15: time = "3:00 AM"; break;
                case 16: time = "3:20 AM"; break;
                case 17: time = "3:40 AM"; break;
                case 18: time = "4:00 AM"; break;
            }
        }

        public static void Normal() //Normal Customer
        {
            customer++;
            camera = false;
            Time();
            string[] orders = { "a shawarma", "a shawarma and a drink", "a shawarma with extra chicken" };
            Random rand = new Random();
            int option = rand.Next(orders.Length);
            void DrawScreen() //Created earlier for easier printing when switching cameras.
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Game.Satisfaction();
                Console.Write("Customer #: " + customer);
                Console.Write("                          Night-Vision Camera (Enter C)");
                Console.WriteLine();
                Console.Write("Money earned: $" + money);
                Console.Write("                                      Time: " + time);
                Console.WriteLine();

                if (camera)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(Art.normalPerson);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(Art.normalPerson);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("A customer approaches the counter. ");
                Console.WriteLine("\"I'd like " + orders[option] + " please.\"");
                Console.WriteLine();
                Console.WriteLine("1. Serve the customer.");
                Console.WriteLine("2. Refuse to serve the customer.");
                Console.WriteLine("3. Close the service window.");
                Console.WriteLine("4. Lie to the customer, saying you have ran out of ingredients.");
            }

            DrawScreen();

            while (true)
            {
                string input = Console.ReadLine()!;

                if (input == "C" || input == "c") //check for valid/invalid input
                {
                    camera = !camera;
                    DrawScreen();
                    continue;
                }

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 4)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter 1, 2, 3, 4, or C.");
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("The customer leaves satisfied.");
                    money += GetShawarmaPrice();
                    break;
                case 2:
                    Console.WriteLine("The customer leaves, promising to leave a bad review.");
                    satisfaction--;
                    break;
                case 3:
                    Console.WriteLine("The customer leaves angrily, swearing to never return.");
                    satisfaction--;
                    break;
                case 4:
                    Console.WriteLine("The customer sees the chicken behind and calls you a liar, slowly walking away.");
                    satisfaction--;
                    break;
            }
            Thread.Sleep(3000);
            if (satisfaction == 0)
            {
                GameOver1();
                return;
            }

        }

        public static void Abnormal() //Hidden Anomaly, requires camera to reveal. Only goal here is to survive.
        {
            customer++;
            camera = false;
            Time();
            string[] orders = { "a shawarma", "a shawarma and a drink", "a shawarma with extra chicken" };
            Random rand = new Random();
            int option = rand.Next(orders.Length);
            void DrawScreen()
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Game.Satisfaction();
                Console.Write("Customer #: " + customer);
                Console.Write("                          Night-Vision Camera (Enter C)");
                Console.WriteLine();
                Console.Write("Money earned: $" + money);
                Console.Write("                                      Time: " + time);
                Console.WriteLine();

                if (camera)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(Art.cameraPerson);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(Art.normalPerson);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("A customer approaches the counter. ");
                Console.WriteLine("\"I'd like " + orders[option] + " please.\"");
                Console.WriteLine();
                Console.WriteLine("1. Serve the customer.");
                Console.WriteLine("2. Refuse to serve the customer.");
                Console.WriteLine("3. Close the service window.");
                Console.WriteLine("4. Lie to the customer, saying you have ran out of ingredients.");
            }

            DrawScreen();

            while (true)
            {
                string input = Console.ReadLine()!;

                if (input == "C" || input == "c")
                {
                    camera = !camera;
                    DrawScreen();
                    continue;
                }

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 4)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter 1, 2, 3, 4, or C.");
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("The customer grabs you by the arm. They drag you out with a creepy smile on their face.");
                    Thread.Sleep(3000);
                    GameOver2();
                    break;
                case 2:
                    Console.WriteLine("The customer lets out a terrifyingly loud scream, and climbs into the food truck.");
                    Thread.Sleep(3000);
                    GameOver2();
                    break;
                case 3:
                    Console.WriteLine("The customer bangs on the service window. However, you are safe inside.");
                    break;
                case 4:
                    Console.WriteLine("The entity is smarter than you thought. It jumps inside the truck, grabbing you and dragging you out with a weird smile on its face.");
                    Thread.Sleep(3000);
                    GameOver2();
                    break;
            }

            Thread.Sleep(3000);

        }

        public static void Vampire() //vampire - you can earn cash, lose satisfaction, or lose life
        {
            customer++;
            camera = false;
            Time();
            void DrawScreen()
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Game.Satisfaction();
                Console.Write("Customer #: " + customer);
                Console.Write("                          Night-Vision Camera (Enter C)");
                Console.WriteLine();
                Console.Write("Money earned: $" + money);
                Console.Write("                                      Time: " + time);
                Console.WriteLine();

                if (camera)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(Art.vampire);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(Art.vampire);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("A customer approaches the counter. ");
                Console.WriteLine("\"I'd like a shawarma, with NO garlic.\"");
                Console.WriteLine();
                Console.WriteLine("1. Give regular shawarma.");
                Console.WriteLine("2. Serve shawarma with no sauce.");
                Console.WriteLine("3. Serve shawarma with tahini sauce instead.");
                Console.WriteLine("4. Close the service window.");
            }

            DrawScreen();

            while (true)
            {
                string input = Console.ReadLine()!;

                if (input == "C" || input == "c")
                {
                    camera = !camera;
                    DrawScreen();
                    continue;
                }

                if (int.TryParse(input, out choice) && choice >= 1 && choice <= 4)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter 1, 2, 3, 4, or C.");
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("The customer grabs you by the arm and sinks their teeth into your neck.");
                    Thread.Sleep(3000);
                    GameOver2();
                    break;
                case 2:
                    Console.WriteLine("The customer does not like the lack of sauce.");
                    satisfaction--;
                    break;
                case 3:
                    Console.WriteLine("The customer loves the tahini sauce. They walk away happily.");
                    money += GetShawarmaPrice();
                    break;
                case 4:
                    Console.WriteLine("The lights start to flicker.");
                    Thread.Sleep(1500);
                    Console.WriteLine("The vampire suddenly appears in front of you.");
                    Thread.Sleep(1500);
                    Console.WriteLine("You feel a sharp pain in your neck.");
                    Thread.Sleep(3000);
                    GameOver2();
                    break;
            }

            Thread.Sleep(3000);
            if (satisfaction == 0)
            {
                GameOver1();
                return;
            }

        }

        public static void FinalMessage() // a final message about computing in society/environment
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=======================================================");
            Console.WriteLine("                     FINAL MESSAGE                     ");
            Console.WriteLine("=======================================================");
            Console.WriteLine();
            TypeWriter("Today, you used a camera to survive. In our modern");
            TypeWriter("world, surveillance systems and Artificial");
            TypeWriter("Intelligence is changing everything.");
            Console.WriteLine();
            TypeWriter("- Real-time monitoring of public spaces, detecting ");
            TypeWriter("  accidents and preventing hazards");
            Console.WriteLine();
            TypeWriter("- Tracking of endangered species and gathering data to");
            TypeWriter("  ensure their survival and fend off illegal poachers.");
            Console.WriteLine();
            TypeWriter("- Dangers and concerns: privacy & data use, ");
            TypeWriter("  algorithmic bias, and increasing use of AI data");
            TypeWriter("  centers that use freshwater and disrupt ecosystems.");
        }

    }
}