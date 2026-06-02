// ts shawarma maxxing
// Scary Shawarma Simulator - a text based decision game
// Inspired by the Roblox game "Scary Shawarma Simulator"
// Built using only: arrays, if/else, for, while, do-while, methods and output (Console I/O).

using System;

class ScaryShawarmaSimulator
{
    // ---------- Game state ----------
    static int money = 0;
    static int decisionMeter = 100;     // 0 = you lose
    static int day = 1;
    static int customersServed = 0;
    static bool alive = true;
    static bool quit = false;
    static Random rng = new Random();

    // ---------- Static data (arrays) ----------
    static string[] customerNames =
    {
        "Ahmed", "Sara", "John", "Maya", "Liam", "Zara",
        "A Tall Stranger", "A Child In A Raincoat", "A Police Officer",
        "A Hooded Figure", "An Old Woman", "A Delivery Driver"
    };

    static string[] normalOrders =
    {
        "a chicken shawarma with extra garlic sauce",
        "a beef shawarma wrap with pickles",
        "a falafel shawarma, no onions",
        "a lamb shawarma plate with rice",
        "a small chicken shawarma with hot sauce",
        "a family combo of mixed shawarmas"
    };

    // ---------- Entry point ----------
    static void Main()
    {
        ShowTitle();
        WaitForEnter("Press ENTER to begin your shift...");

        RunTutorial();

        // Main game loop - keep playing days until the player dies, quits, or wins.
        do
        {
            PlayDay();
            if (alive && !quit)
            {
                day = day + 1;
            }
        } while (alive && !quit && day <= 5);

        EndGame();
    }

    // ---------- Presentation helpers ----------
    static void ShowTitle()
    {
        Console.WriteLine("============================================");
        Console.WriteLine("       SCARY SHAWARMA SIMULATOR");
        Console.WriteLine("              ~ Night Shift ~");
        Console.WriteLine("============================================");
        Console.WriteLine();
        Console.WriteLine("You are the new night-shift worker at a");
        Console.WriteLine("24 hour shawarma shop. Most customers are");
        Console.WriteLine("normal... most.");
        Console.WriteLine();
        Console.WriteLine("Serve real customers, refuse the things");
        Console.WriteLine("that only LOOK like customers, and survive");
        Console.WriteLine("five nights to keep your job.");
        Console.WriteLine();
    }

    static void ShowStats()
    {
        Console.WriteLine();
        Console.WriteLine("-------- STATUS --------");
        Console.WriteLine("Night:           " + day);
        Console.WriteLine("Money earned:    $" + money);
        Console.WriteLine("Customers done:  " + customersServed);
        Console.Write    ("Sanity meter:    [");
        // Draw a 20 segment bar using a for loop.
        int segments = decisionMeter / 5;
        for (int i = 0; i < 20; i = i + 1)
        {
            if (i < segments)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write("-");
            }
        }
        Console.WriteLine("] " + decisionMeter + "/100");
        Console.WriteLine("------------------------");
        Console.WriteLine();
    }

    static void WaitForEnter(string prompt)
    {
        Console.WriteLine(prompt);
        Console.ReadLine();
    }

    // Asks the user for a numbered choice between 1 and optionCount.
    // Uses a do-while loop to keep asking until valid input is given.
    static int AskChoice(string[] options)
    {
        int choice = 0;
        bool valid = false;
        do
        {
            Console.WriteLine();
            for (int i = 0; i < options.Length; i = i + 1)
            {
                Console.WriteLine("  " + (i + 1) + ") " + options[i]);
            }
            Console.Write("Your choice: ");
            string input = Console.ReadLine();

            if (input == null)
            {
                input = "";
            }

            // Convert manually using a for loop so we don't rely on extra parsing helpers.
            int parsed = 0;
            bool numeric = input.Length > 0;
            for (int i = 0; i < input.Length; i = i + 1)
            {
                char c = input[i];
                if (c >= '0' && c <= '9')
                {
                    parsed = parsed * 10 + (c - '0');
                }
                else
                {
                    numeric = false;
                }
            }

            if (numeric && parsed >= 1 && parsed <= options.Length)
            {
                choice = parsed;
                valid = true;
            }
            else
            {
                Console.WriteLine("Please type a number between 1 and " + options.Length + ".");
            }
        } while (!valid);

        return choice;
    }

    // ---------- Meter / money helpers ----------
    static void GoodDecision(int reward, string reason)
    {
        money = money + reward;
        decisionMeter = decisionMeter + 5;
        if (decisionMeter > 100)
        {
            decisionMeter = 100;
        }
        Console.WriteLine(">> " + reason);
        Console.WriteLine(">> +$" + reward + "   sanity steady.");
    }

    static void BadDecision(int penalty, string reason)
    {
        decisionMeter = decisionMeter - penalty;
        if (decisionMeter < 0)
        {
            decisionMeter = 0;
        }
        Console.WriteLine(">> " + reason);
        Console.WriteLine(">> sanity -" + penalty + ".");
    }

    static void InstantDeath(string reason)
    {
        alive = false;
        Console.WriteLine();
        Console.WriteLine("################################################");
        Console.WriteLine("  " + reason);
        Console.WriteLine("################################################");
    }

    // ---------- Tutorial ----------
    static void RunTutorial()
    {
        Console.WriteLine();
        Console.WriteLine("===== TRAINING NIGHT =====");
        Console.WriteLine("Your manager leaves you a sticky note:");
        Console.WriteLine();
        Console.WriteLine("  1. Serve customers who order from the menu.");
        Console.WriteLine("  2. REFUSE anyone whose face, shadow or voice");
        Console.WriteLine("     feels wrong. These are ANOMALIES.");
        Console.WriteLine("  3. Sometimes you will get a WARNING. Listen.");
        Console.WriteLine("  4. Bad calls hurt your sanity. At 0 you snap.");
        Console.WriteLine();
        WaitForEnter("Press ENTER to take your first order...");

        // --- Training scenario 1: a normal customer ---
        Console.WriteLine();
        Console.WriteLine("[TUTORIAL 1/3]");
        Console.WriteLine("Ahmed walks in and asks for " + normalOrders[0] + ".");
        Console.WriteLine("Nothing seems off about him.");
        string[] t1 = { "Serve him politely.", "Refuse and tell him to leave.", "Hide behind the counter." };
        int c1 = AskChoice(t1);
        if (c1 == 1)
        {
            GoodDecision(10, "Correct. A normal customer means easy money.");
        }
        else
        {
            BadDecision(10, "He was a regular paying customer. Don't scare them off.");
        }

        // --- Training scenario 2: an obvious anomaly ---
        Console.WriteLine();
        Console.WriteLine("[TUTORIAL 2/3]");
        Console.WriteLine("A tall man steps in. His shadow points the WRONG way.");
        Console.WriteLine("He whispers an order you can't quite hear.");
        string[] t2 = { "Serve him quickly so he leaves.", "Politely refuse service.", "Stare into his eyes." };
        int c2 = AskChoice(t2);
        if (c2 == 2)
        {
            GoodDecision(5, "Good. Refusing an anomaly is always safe.");
        }
        else if (c2 == 1)
        {
            BadDecision(20, "You handed food to something that wasn't human. It smiled.");
        }
        else
        {
            BadDecision(25, "Never look it in the eyes. You feel something crawl behind your ribs.");
        }

        // --- Training scenario 3: warned anomaly ---
        Console.WriteLine();
        Console.WriteLine("[TUTORIAL 3/3]");
        Console.WriteLine("The radio crackles: '... if the lights flicker three times,");
        Console.WriteLine(" do NOT look up. Just keep wrapping the shawarma.'");
        Console.WriteLine("The lights flicker. One... two... three.");
        string[] t3 = { "Look up.", "Keep your eyes down and keep wrapping.", "Run out the back door." };
        int c3 = AskChoice(t3);
        if (c3 == 2)
        {
            GoodDecision(15, "Whatever was on the ceiling moves on. You survive.");
        }
        else if (c3 == 1)
        {
            BadDecision(30, "It saw you see it. The kitchen goes cold.");
        }
        else
        {
            BadDecision(10, "Abandoning the post drops your sanity from guilt.");
        }

        Console.WriteLine();
        Console.WriteLine("===== TRAINING COMPLETE =====");
        ShowStats();
        WaitForEnter("Press ENTER to start your real shifts...");
    }

    // ---------- Main day loop ----------
    static void PlayDay()
    {
        Console.WriteLine();
        Console.WriteLine("================================");
        Console.WriteLine("        NIGHT " + day);
        Console.WriteLine("================================");

        // Each night you get a number of customers that grows with the day.
        int customersTonight = 3 + day;
        int handled = 0;

        // Use a while loop so the night can end early if the player dies.
        while (handled < customersTonight && alive && !quit)
        {
            // Pick a scenario type.
            int roll = rng.Next(0, 100);

            // Sometimes give a radio warning first.
            bool warned = false;
            if (roll >= 60 && roll < 85)
            {
                BroadcastWarning();
                warned = true;
            }

            if (roll < 50)
            {
                NormalCustomerScenario();
            }
            else if (roll < 80)
            {
                AnomalyScenario(warned);
            }
            else if (roll < 92)
            {
                RandomEventScenario();
            }
            else
            {
                BossAnomalyScenario();
            }

            handled = handled + 1;
            customersServed = customersServed + 1;

            if (decisionMeter <= 0 && alive)
            {
                InstantDeath("Your sanity hit zero. You start wrapping your own hand. End of shift.");
            }

            if (alive && !quit && handled < customersTonight)
            {
                Console.WriteLine();
                Console.WriteLine("(The next customer approaches the counter...)");
                WaitForEnter("Press ENTER to continue.");
            }
        }

        if (alive && !quit)
        {
            Console.WriteLine();
            Console.WriteLine("*** End of Night " + day + " ***");
            ShowStats();
            if (day < 5)
            {
                WaitForEnter("Press ENTER to start your next shift...");
            }
        }
    }

    // ---------- Scenarios ----------
    static void NormalCustomerScenario()
    {
        string name = customerNames[rng.Next(0, 6)]; // first 6 are normal-ish
        string order = normalOrders[rng.Next(0, normalOrders.Length)];

        Console.WriteLine();
        Console.WriteLine(name + " walks in and orders " + order + ".");
        Console.WriteLine("They make polite small talk while you cook.");

        string[] options =
        {
            "Serve them and take payment.",
            "Refuse service for no reason.",
            "Overcharge them by $10.",
            "Give the food away for free."
        };
        int c = AskChoice(options);

        if (c == 1)
        {
            GoodDecision(12, "Smooth transaction. They tip you.");
        }
        else if (c == 2)
        {
            BadDecision(10, "They leave a one-star review. Manager will hear about this.");
        }
        else if (c == 3)
        {
            // Greedy: more money, less sanity
            money = money + 20;
            BadDecision(15, "They pay but glare at you. The guilt eats at you.");
        }
        else
        {
            BadDecision(5, "Generous, but you lose money and the till is short.");
            money = money - 8;
            if (money < 0)
            {
                money = 0;
            }
        }
    }

    static void AnomalyScenario(bool warned)
    {
        // Build a small pool of anomalies in arrays.
        string[] descriptions =
        {
            "A man with no reflection in the front window orders 'whatever you ate yesterday'.",
            "A child stands at the counter. Their feet do not touch the floor.",
            "A customer's order keeps changing every time you blink.",
            "A police officer asks for a shawarma but their badge has no name on it.",
            "A hooded figure points at the menu. Every item they point at burns away.",
            "A woman orders, but her voice is coming from the kitchen, not her mouth."
        };
        string[] correctClues =
        {
            "no reflection",
            "feet not touching the floor",
            "order changing on its own",
            "blank badge",
            "burning menu",
            "voice from the wrong place"
        };

        int idx = rng.Next(0, descriptions.Length);

        Console.WriteLine();
        if (warned)
        {
            Console.WriteLine("(You remember the radio warning.)");
        }
        Console.WriteLine(descriptions[idx]);

        string[] options =
        {
            "Serve them like a normal customer.",
            "Politely refuse and ask them to leave.",
            "Call the police.",
            "Try to fight them with the cleaver."
        };
        int c = AskChoice(options);

        if (c == 2)
        {
            int bonus = 8;
            if (warned)
            {
                bonus = 14;
            }
            GoodDecision(bonus, "Good eye - you spotted the " + correctClues[idx] + ". They leave.");
        }
        else if (c == 1)
        {
            BadDecision(25, "You served the anomaly. It comes back later. They always do.");
        }
        else if (c == 3)
        {
            BadDecision(10, "Police don't come for things like this. You waste the call.");
        }
        else
        {
            // Fighting is a coin flip - this is the only "luck" branch.
            int luck = rng.Next(0, 2);
            if (luck == 0)
            {
                InstantDeath("You swung the cleaver. It caught you instead. The shop hires a new worker tomorrow.");
            }
            else
            {
                BadDecision(20, "You survived the swing but your hands won't stop shaking.");
            }
        }
    }

    static void BossAnomalyScenario()
    {
        Console.WriteLine();
        Console.WriteLine("!! THE SHAWARMA MAN WALKS IN !!");
        Console.WriteLine("Seven feet tall. Skin made of rotating meat.");
        Console.WriteLine("He sets a thousand dollars on the counter and grins.");

        string[] options =
        {
            "Take the money and serve him.",
            "Take the money and refuse to serve him.",
            "Refuse the money and run to the back.",
            "Offer him YOUR shawarma."
        };
        int c = AskChoice(options);

        if (c == 3)
        {
            GoodDecision(0, "You hid in the freezer until he left. Your sanity steadies.");
        }
        else if (c == 1)
        {
            money = money + 1000;
            InstantDeath("He eats the shawarma. Then he eats you. The money turns to leaves.");
        }
        else if (c == 2)
        {
            money = money + 1000;
            BadDecision(40, "He laughs. The money is real, but something in your reflection laughs back.");
        }
        else
        {
            // Offering your own shawarma - cheeky, risky
            int luck = rng.Next(0, 3);
            if (luck == 0)
            {
                GoodDecision(50, "He is amused. He drops a $50 tip and leaves.");
            }
            else
            {
                BadDecision(30, "He is NOT amused. He licks the counter and walks out.");
            }
        }
    }

    static void RandomEventScenario()
    {
        int pick = rng.Next(0, 4);

        if (pick == 0)
        {
            Console.WriteLine();
            Console.WriteLine("[EVENT] A masked robber bursts in waving a knife!");
            string[] opts = { "Hand over the cash drawer.", "Throw hot oil at them.", "Press the silent alarm and stall." };
            int c = AskChoice(opts);
            if (c == 3)
            {
                GoodDecision(15, "Police arrive. They give you a small reward.");
            }
            else if (c == 1)
            {
                int loss = 30;
                money = money - loss;
                if (money < 0)
                {
                    money = 0;
                }
                BadDecision(5, "Cash gone, but you live.");
            }
            else
            {
                int luck = rng.Next(0, 2);
                if (luck == 0)
                {
                    InstantDeath("The robber dodged the oil. The next thing you wrap is your final breath.");
                }
                else
                {
                    GoodDecision(20, "The robber screams and runs. You keep the cash.");
                }
            }
        }
        else if (pick == 1)
        {
            Console.WriteLine();
            Console.WriteLine("[EVENT] The health inspector arrives unannounced.");
            string[] opts = { "Show the (slightly dirty) kitchen honestly.", "Hide the rats under the counter.", "Offer a free shawarma 'gift'." };
            int c = AskChoice(opts);
            if (c == 1)
            {
                GoodDecision(10, "Honest enough. You pass with a warning.");
            }
            else if (c == 2)
            {
                BadDecision(15, "The rats come out during the inspection. Fine of $40.");
                money = money - 40;
                if (money < 0)
                {
                    money = 0;
                }
            }
            else
            {
                BadDecision(25, "She reports you for bribery. Sanity tanks from paperwork.");
            }
        }
        else if (pick == 2)
        {
            Console.WriteLine();
            Console.WriteLine("[EVENT] The lights cut out. You hear chewing in the dark.");
            string[] opts = { "Turn on the phone flashlight.", "Stay perfectly still.", "Yell 'WE'RE CLOSED'." };
            int c = AskChoice(opts);
            if (c == 2)
            {
                GoodDecision(10, "The chewing fades. The lights come back on by themselves.");
            }
            else if (c == 1)
            {
                BadDecision(20, "The light reveals nothing. But something saw your face.");
            }
            else
            {
                BadDecision(15, "Whatever it was, it heard you. The chewing got closer for a moment.");
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("[EVENT] A regular hands you a wrapped tip and whispers 'don't open it tonight'.");
            string[] opts = { "Open it now.", "Put it in your pocket for later.", "Throw it in the trash." };
            int c = AskChoice(opts);
            if (c == 2)
            {
                GoodDecision(25, "You opened it the next morning. Real money. Big tip.");
            }
            else if (c == 1)
            {
                BadDecision(20, "Inside is a tooth. Yours.");
            }
            else
            {
                BadDecision(5, "You'll never know what it was. The not-knowing nags at you.");
            }
        }
    }

    static void BroadcastWarning()
    {
        // Pick a flavor warning from an array.
        string[] warnings =
        {
            "RADIO: '... if the next customer compliments the salt, refuse them.'",
            "RADIO: '... avoid eye contact with anyone wearing two watches.'",
            "RADIO: '... the next order will sound right but feel wrong. Trust the feeling.'",
            "RADIO: '... if they pay with coins that are warm, they are not coins.'"
        };
        Console.WriteLine();
        Console.WriteLine(warnings[rng.Next(0, warnings.Length)]);
    }

    // ---------- End of game ----------
    static void EndGame()
    {
        Console.WriteLine();
        Console.WriteLine("============================================");
        Console.WriteLine("                 SHIFT OVER");
        Console.WriteLine("============================================");
        ShowStats();

        if (!alive)
        {
            Console.WriteLine("You did not make it through the week.");
            Console.WriteLine("The shop opens again tomorrow. With a new worker.");
        }
        else if (quit)
        {
            Console.WriteLine("You walked out mid-shift. Probably the smart move.");
        }
        else
        {
            // Decide ending by stats.
            if (decisionMeter >= 70 && money >= 150)
            {
                Console.WriteLine("LEGEND ENDING: Five nights, clear eyes, full wallet.");
                Console.WriteLine("You are promoted to day-shift manager.");
            }
            else if (decisionMeter >= 40)
            {
                Console.WriteLine("SURVIVOR ENDING: You made it. You'll never sleep the same.");
            }
            else
            {
                Console.WriteLine("BARELY-ALIVE ENDING: You finished the week, but something");
                Console.WriteLine("in the mirror still wraps shawarmas after you go home.");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Thanks for playing Scary Shawarma Simulator.");
    }
}
