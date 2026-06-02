// ts shawarma maxxing
// Scary Shawarma Simulator - a text based decision game
// Inspired by the Roblox game "Scary Shawarma Simulator".
// Built using only: arrays, if/else, for, while, do-while, methods, console I/O.
// ASCII art is loaded from ShawarmaArt.cs (compiled together with this file).

using System;

class ScaryShawarmaSimulator
{
    // ---------- Game state ----------
    static int money = 0;
    static int decisionMeter = 100;   // 0 = you lose
    static int day = 1;
    static int customersServed = 0;
    static int correctTonight = 0;    // good calls this shift (used for quota)
    static int streak = 0;            // consecutive good calls -> tip bonus
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

    // For each normalOrders entry, the "correct" sauce index in the sauces array below.
    static int[] correctSauce = { 0, 1, 2, 3, 4, 5 };
    static string[] sauces =
    {
        "garlic", "pickle brine", "tahini", "yogurt-mint", "hot chilli", "house mix"
    };

    // ---------- Entry point ----------
    static void Main()
    {
        ShowTitle();
        WaitForEnter("Press ENTER to begin your shift...");

        RunTutorial();

        // Main game loop - one iteration per night.
        do
        {
            correctTonight = 0;
            PlayDay();
            if (alive && !quit)
            {
                EnforceNightlyQuota();
            }
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
        ShawarmaArt.PrintTitle();
        Console.WriteLine("============================================");
        ShawarmaArt.PrintShawarma();
        Console.WriteLine();
        Console.WriteLine("You are the new night-shift worker at a");
        Console.WriteLine("24 hour shawarma shop. Most customers are");
        Console.WriteLine("normal... most.");
        Console.WriteLine();
        Console.WriteLine("Serve real customers, refuse the things");
        Console.WriteLine("that only LOOK like customers, and survive");
        Console.WriteLine("five nights to keep your job.");
        Console.WriteLine();
        Console.WriteLine("Manager's rules (memorize these):");
        Console.WriteLine("  * Each night has a QUOTA of correct calls.");
        Console.WriteLine("  * The radio sometimes LIES. Don't trust it blindly.");
        Console.WriteLine("  * Sanity only refills slowly. Don't burn it.");
        Console.WriteLine("  * Debt is real - if you owe money, sleep is hard.");
        Console.WriteLine();
    }

    static void ShowStats()
    {
        Console.WriteLine();
        Console.WriteLine("-------- STATUS --------");
        Console.WriteLine("Night:           " + day);
        Console.WriteLine("Money:           $" + money);
        Console.WriteLine("Customers done:  " + customersServed);
        Console.WriteLine("Good calls (tonight): " + correctTonight);
        Console.WriteLine("Streak bonus:    " + streak);
        Console.Write    ("Sanity meter:    [");
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

    // Asks the user for a numbered choice between 1 and options.Length.
    // Trims whitespace, rejects junk, and re-asks via do-while.
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
            string raw = Console.ReadLine();
            if (raw == null)
            {
                raw = "";
            }

            // Trim leading/trailing whitespace manually using a for loop.
            int startIdx = 0;
            int endIdx = raw.Length - 1;
            while (startIdx <= endIdx && (raw[startIdx] == ' ' || raw[startIdx] == '\t'))
            {
                startIdx = startIdx + 1;
            }
            while (endIdx >= startIdx && (raw[endIdx] == ' ' || raw[endIdx] == '\t'))
            {
                endIdx = endIdx - 1;
            }

            int parsed = 0;
            bool numeric = endIdx >= startIdx;
            for (int i = startIdx; i <= endIdx; i = i + 1)
            {
                char c = raw[i];
                if (c >= '0' && c <= '9')
                {
                    parsed = parsed * 10 + (c - '0');
                    // Overflow guard - any input bigger than the option count is junk anyway.
                    if (parsed > 10000)
                    {
                        numeric = false;
                        i = endIdx + 1; // exit loop
                    }
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
    // Sanity regen is small AND capped at 80 unless you've cleared tonight's quota.
    static void GoodDecision(int reward, string reason)
    {
        // Streak tip bonus - every 3rd consecutive correct gives +$5.
        streak = streak + 1;
        correctTonight = correctTonight + 1;
        int tip = 0;
        if (streak > 0 && streak % 3 == 0)
        {
            tip = 5;
            Console.WriteLine(">> STREAK BONUS x" + streak + " - +$5 tip!");
        }

        money = money + reward + tip;

        // Soft regen. Caps differently depending on quota progress.
        int regen = 2;
        int cap = 80;
        if (correctTonight >= 2 + day)
        {
            cap = 100;
            regen = 3;
        }
        if (decisionMeter < cap)
        {
            decisionMeter = decisionMeter + regen;
            if (decisionMeter > cap)
            {
                decisionMeter = cap;
            }
        }

        Console.WriteLine(">> " + reason);
        Console.WriteLine(">> +$" + reward + ".");
    }

    // Day-scaled penalty so later nights bite harder.
    static void BadDecision(int basePenalty, string reason)
    {
        streak = 0;
        int penalty = basePenalty + (day - 1) * 2;
        decisionMeter = decisionMeter - penalty;
        if (decisionMeter < 0)
        {
            decisionMeter = 0;
        }
        Console.WriteLine(">> " + reason);
        Console.WriteLine(">> sanity -" + penalty + ".");
    }

    // Losing money no longer clamps to zero - you can go into debt, AND debt
    // converts unpaid loss into sanity damage so being broke isn't a shield.
    static void LoseMoney(int amount)
    {
        if (amount <= 0) { return; }
        int before = money;
        money = money - amount;
        Console.WriteLine(">> -$" + amount + " (balance: $" + money + ").");
        if (before < 0 || money < 0)
        {
            // You were already broke (or went broke) - the stress shows.
            int extra = 5;
            if (money < -20) { extra = 10; }
            if (money < -50) { extra = 15; }
            decisionMeter = decisionMeter - extra;
            if (decisionMeter < 0) { decisionMeter = 0; }
            Console.WriteLine(">> Debt gnaws at you. Sanity -" + extra + ".");
        }
    }

    static void InstantDeath(string reason)
    {
        alive = false;
        Console.WriteLine();
        Console.WriteLine("################################################");
        Console.WriteLine("  " + reason);
        Console.WriteLine("################################################");
        ShawarmaArt.PrintSkull();
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
        Console.WriteLine("  3. The radio gives WARNINGS - but it lies about");
        Console.WriteLine("     1 in 3. Don't refuse customers blindly.");
        Console.WriteLine("  4. Bad calls hurt your sanity. At 0 you snap.");
        Console.WriteLine("  5. You MUST clear a nightly quota or sanity drops.");
        Console.WriteLine();
        WaitForEnter("Press ENTER to take your first order...");

        // --- Training scenario 1: a normal customer + sauce mini-choice ---
        Console.WriteLine();
        Console.WriteLine("[TUTORIAL 1/3]");
        Console.WriteLine("Ahmed walks in and orders " + normalOrders[0] + ".");
        Console.WriteLine("Nothing seems off about him.");
        string[] t1 = { "Serve him politely.", "Refuse and tell him to leave.", "Hide behind the counter." };
        int c1 = AskChoice(t1);
        if (c1 == 1)
        {
            // Sauce mini-game.
            Console.WriteLine("Which sauce does he want with his order?");
            int picked = AskChoice(sauces);
            if (picked - 1 == correctSauce[0])
            {
                GoodDecision(15, "Right sauce, smooth service. A great start.");
            }
            else
            {
                BadDecision(5, "Wrong sauce. He pays but doesn't tip.");
                money = money + 5;
                Console.WriteLine(">> +$5 (partial payment).");
            }
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
        ShawarmaArt.PrintGhost();
        string[] t2 = { "Serve him quickly so he leaves.", "Politely refuse service.", "Stare into his eyes." };
        int c2 = AskChoice(t2);
        if (c2 == 2)
        {
            GoodDecision(5, "Good. Refusing a clear anomaly is always safe.");
        }
        else if (c2 == 1)
        {
            BadDecision(20, "You handed food to something that wasn't human. It smiled.");
        }
        else
        {
            BadDecision(25, "Never look it in the eyes. Something crawls behind your ribs.");
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
        Console.WriteLine("        Quota: " + (2 + day) + " good calls.");
        Console.WriteLine("================================");

        int customersTonight = 4 + day;   // 5, 6, 7, 8, 9 across nights 1..5
        int handled = 0;

        while (handled < customersTonight && alive && !quit)
        {
            // Roll up the next scenario.
            int roll = rng.Next(0, 100);

            // 30% of warnings will be lies - tracked so anomaly logic knows.
            bool warned = false;
            bool warningIsReal = true;
            if (roll >= 55 && roll < 88)
            {
                warned = true;
                warningIsReal = rng.Next(0, 100) >= 30;
                BroadcastWarning(warningIsReal);
            }

            if (roll < 45)
            {
                NormalCustomerScenario(warned, warningIsReal);
            }
            else if (roll < 75)
            {
                AnomalyScenario(warned, warningIsReal);
            }
            else if (roll < 88)
            {
                RandomEventScenario();
            }
            else if (roll < 95)
            {
                RushHourScenario();
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
        }
    }

    static void EnforceNightlyQuota()
    {
        int quota = 2 + day;
        if (correctTonight < quota)
        {
            int shortBy = quota - correctTonight;
            int penalty = 8 + shortBy * 4;
            decisionMeter = decisionMeter - penalty;
            if (decisionMeter < 0) { decisionMeter = 0; }
            Console.WriteLine(">> QUOTA MISSED by " + shortBy + ". Manager docks your sanity -" + penalty + ".");
            if (decisionMeter == 0 && alive)
            {
                InstantDeath("You snapped under quota pressure. You start eating the menu.");
            }
        }
        else
        {
            Console.WriteLine(">> Quota cleared. You earn a $10 shift bonus.");
            money = money + 10;
        }
        if (day < 5 && alive && !quit)
        {
            WaitForEnter("Press ENTER to start your next shift...");
        }
    }

    // ---------- Scenarios ----------
    static void NormalCustomerScenario(bool warned, bool warningIsReal)
    {
        int nameIdx = rng.Next(0, 6); // normal-ish names only
        int orderIdx = rng.Next(0, normalOrders.Length);
        string name = customerNames[nameIdx];
        string order = normalOrders[orderIdx];

        Console.WriteLine();
        Console.WriteLine(name + " walks in and orders " + order + ".");
        Console.WriteLine("They make polite small talk while you cook.");

        // If the radio gave a real warning, a normal customer here is the red herring path.
        // Refusing them is the WRONG move; serving them is right.

        string[] options =
        {
            "Serve them and take payment.",
            "Refuse service.",
            "Overcharge them by $10.",
            "Give the food away for free."
        };
        int c = AskChoice(options);

        if (c == 1)
        {
            Console.WriteLine("Which sauce do they want?");
            int picked = AskChoice(sauces);
            if (picked - 1 == correctSauce[orderIdx])
            {
                GoodDecision(12, "Order correct. They leave happy.");
            }
            else
            {
                BadDecision(4, "Wrong sauce. They pay but glare.");
                money = money + 6;
                Console.WriteLine(">> +$6 (no tip).");
            }
        }
        else if (c == 2)
        {
            if (warned && !warningIsReal)
            {
                BadDecision(15, "You refused a paying customer because of a FAKE radio warning. Manager fines you.");
                LoseMoney(10);
            }
            else
            {
                BadDecision(10, "They leave a one-star review. Manager will hear about this.");
            }
        }
        else if (c == 3)
        {
            // Overcharge: net-bad. Adds only $4 over honest service, costs sanity.
            money = money + 16;
            BadDecision(18, "They notice the markup and glare. The guilt sticks.");
        }
        else
        {
            // Free food: ALWAYS costs you money, even if you have to take it from your own pocket.
            LoseMoney(10);
            BadDecision(8, "The till is short. You ate the cost.");
        }
    }

    static void AnomalyScenario(bool warned, bool warningIsReal)
    {
        string[] descriptions =
        {
            "A man with no reflection in the front window orders 'whatever you ate yesterday'.",
            "A child stands at the counter. Their feet do not touch the floor.",
            "A customer's order keeps changing every time you blink.",
            "A police officer asks for a shawarma but their badge has no name on it.",
            "A hooded figure points at the menu. Every item they point at burns away.",
            "A woman orders, but her voice is coming from the kitchen, not her mouth.",
            "You see YOURSELF walk in. Same uniform. Different smile.",
            "The phone rings - a delivery for '404 Nowhere Street'. The voice asks for YOUR name."
        };
        string[] correctClues =
        {
            "no reflection",
            "feet not touching the floor",
            "order changing on its own",
            "blank badge",
            "burning menu",
            "voice from the wrong place",
            "your own mirrored self",
            "an address that does not exist"
        };

        int idx = rng.Next(0, descriptions.Length);

        Console.WriteLine();
        if (warned && warningIsReal)
        {
            Console.WriteLine("(The radio warning matches what you see.)");
        }
        else if (warned && !warningIsReal)
        {
            Console.WriteLine("(The radio described something different. But this is clearly wrong too.)");
        }
        ShawarmaArt.PrintGhost();
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
            int bonus = 6;
            if (warned && warningIsReal) { bonus = 12; }
            GoodDecision(bonus, "Good eye - you spotted the " + correctClues[idx] + ". They leave.");
        }
        else if (c == 1)
        {
            BadDecision(28, "You served the anomaly. It comes back later. They always do.");
        }
        else if (c == 3)
        {
            BadDecision(12, "Police don't come for things like this. You waste the call.");
            LoseMoney(5);
        }
        else
        {
            // Cleaver swing is now ALWAYS lethal. Scary game = swinging at monsters = death.
            InstantDeath("You swung the cleaver. It caught you instead. The shop hires a new worker tomorrow.");
        }
    }

    static void BossAnomalyScenario()
    {
        Console.WriteLine();
        Console.WriteLine("!! THE SHAWARMA MAN WALKS IN !!");
        ShawarmaArt.PrintShawarmaMan();
        Console.WriteLine("Seven feet tall. Skin made of rotating meat.");
        Console.WriteLine("He sets a bundle of cash on the counter and grins.");

        string[] options =
        {
            "Take the money and serve him.",
            "Take the money and refuse to serve him.",
            "Refuse the money and hide in the freezer.",
            "Offer him YOUR shawarma."
        };
        int c = AskChoice(options);

        if (c == 3)
        {
            // The actual safe option - but no money.
            streak = streak + 1;
            correctTonight = correctTonight + 1;
            Console.WriteLine(">> You hid in the freezer until he left. You earn no money but you live.");
        }
        else if (c == 1)
        {
            money = money + 200;
            InstantDeath("He eats the shawarma. Then he eats you. The money turns to teeth in your pocket.");
        }
        else if (c == 2)
        {
            // Used to be the exploit. Now: small money, MASSIVE sanity hit.
            money = money + 50;
            BadDecision(60, "He laughs. The money is real, but your reflection now laughs back at random times.");
            if (decisionMeter <= 0 && alive)
            {
                InstantDeath("Taking his money cracked you. You join him at the next table.");
            }
        }
        else
        {
            // Offering your own shawarma - cheeky, risky.
            int luck = rng.Next(0, 3);
            if (luck == 0)
            {
                GoodDecision(40, "He is amused. He drops a $40 tip and leaves.");
            }
            else
            {
                BadDecision(35, "He is NOT amused. He licks the counter and walks out.");
            }
        }
    }

    static void RandomEventScenario()
    {
        int pick = rng.Next(0, 5);

        if (pick == 0)
        {
            Console.WriteLine();
            Console.WriteLine("[EVENT] A masked robber bursts in waving a knife!");
            ShawarmaArt.PrintPolice();
            string[] opts = { "Hand over the cash drawer.", "Throw hot oil at them.", "Press the silent alarm and stall." };
            int c = AskChoice(opts);
            if (c == 3)
            {
                GoodDecision(15, "Police arrive. They give you a small reward.");
            }
            else if (c == 1)
            {
                LoseMoney(30);
                BadDecision(8, "Cash gone, but you live.");
            }
            else
            {
                int luck = rng.Next(0, 3);
                if (luck < 2)
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
                GoodDecision(0, "Honest enough. You pass with a warning.");
            }
            else if (c == 2)
            {
                BadDecision(15, "The rats come out during the inspection.");
                LoseMoney(40);
            }
            else
            {
                BadDecision(25, "She reports you for bribery. Paperwork all night.");
                LoseMoney(20);
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
        else if (pick == 3)
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
                BadDecision(28, "Inside is a tooth. Yours.");
            }
            else
            {
                // Throwing it away isn't 'free' anymore - the regular sees and stops tipping.
                BadDecision(8, "The regular sees. You just lost a recurring customer.");
                LoseMoney(5);
            }
        }
        else
        {
            // Phone call scenario - delivery order. Could be normal or anomaly.
            Console.WriteLine();
            Console.WriteLine("[EVENT] The phone rings. A polite voice orders 5 shawarmas for delivery.");
            bool isFake = rng.Next(0, 2) == 0;
            if (isFake)
            {
                Console.WriteLine("They give an address downtown - but you hear breathing on the line that isn't yours.");
            }
            else
            {
                Console.WriteLine("They sound completely normal. Address checks out.");
            }
            string[] opts = { "Accept the order and start cooking.", "Confirm address by calling back.", "Refuse the order." };
            int c = AskChoice(opts);
            if (c == 2)
            {
                if (isFake)
                {
                    GoodDecision(8, "Number disconnected. You dodged a setup.");
                }
                else
                {
                    GoodDecision(20, "Confirmed. Big order, big tip.");
                }
            }
            else if (c == 1)
            {
                if (isFake)
                {
                    BadDecision(25, "The 'address' was an empty lot. Something was waiting there.");
                }
                else
                {
                    GoodDecision(25, "Delivery went out clean. Huge tip.");
                }
            }
            else
            {
                if (isFake)
                {
                    GoodDecision(5, "You hung up. Good instinct.");
                }
                else
                {
                    BadDecision(10, "That was a real customer. The shop loses business.");
                    LoseMoney(15);
                }
            }
        }
    }

    static void RushHourScenario()
    {
        Console.WriteLine();
        Console.WriteLine("[RUSH] Three customers slam the counter at once!");
        // Each rush customer is either normal or anomaly with reduced detail - tougher to read.
        for (int i = 0; i < 3; i = i + 1)
        {
            if (!alive) { i = 3; continue; }
            bool isAnomaly = rng.Next(0, 2) == 0;
            Console.WriteLine();
            Console.WriteLine("RUSH #" + (i + 1) + ":");
            if (isAnomaly)
            {
                string[] hints =
                {
                    "Customer #" + (i + 1) + " - their reflection in the chrome counter doesn't blink.",
                    "Customer #" + (i + 1) + " - they ordered before they walked in.",
                    "Customer #" + (i + 1) + " - they're already chewing."
                };
                Console.WriteLine(hints[rng.Next(0, hints.Length)]);
            }
            else
            {
                Console.WriteLine("Customer #" + (i + 1) + " - looks like a hungry office worker.");
            }

            string[] opts = { "Serve quickly.", "Refuse." };
            int c = AskChoice(opts);
            if (isAnomaly)
            {
                if (c == 2) { GoodDecision(8, "Spotted under pressure. Good."); }
                else { BadDecision(18, "Rushed and served the wrong thing."); }
            }
            else
            {
                if (c == 1) { GoodDecision(10, "Fast service, happy customer."); }
                else { BadDecision(10, "You refused a real customer. They go to the rival shop."); }
            }
        }
    }

    static void BroadcastWarning(bool real)
    {
        string[] warningsReal =
        {
            "RADIO: '... if the next customer compliments the salt, refuse them.'",
            "RADIO: '... avoid eye contact with anyone wearing two watches.'",
            "RADIO: '... the next order will sound right but feel wrong.'",
            "RADIO: '... if they pay with coins that are warm, they are not coins.'"
        };
        string[] warningsFake =
        {
            "RADIO: '... refuse ANYONE in a red shirt.' (...static. Sounds wrong.)",
            "RADIO: '... do not serve left-handed customers tonight.'",
            "RADIO: '... only women between 8 and 9pm.'",
            "RADIO: '... refuse anyone whose first name starts with A.'"
        };
        Console.WriteLine();
        if (real)
        {
            Console.WriteLine(warningsReal[rng.Next(0, warningsReal.Length)]);
        }
        else
        {
            Console.WriteLine(warningsFake[rng.Next(0, warningsFake.Length)]);
        }
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
            ShawarmaArt.PrintGameOver();
            Console.WriteLine("You did not make it through the week.");
            Console.WriteLine("The shop opens again tomorrow. With a new worker.");
        }
        else if (quit)
        {
            Console.WriteLine("You walked out mid-shift. Probably the smart move.");
        }
        else
        {
            // Decide ending. Debt MATTERS now.
            if (decisionMeter >= 70 && money >= 200)
            {
                ShawarmaArt.PrintWin();
                ShawarmaArt.PrintMoney();
                Console.WriteLine("LEGEND ENDING: Five nights, clear eyes, full wallet.");
                Console.WriteLine("You are promoted to day-shift manager.");
            }
            else if (decisionMeter >= 40 && money >= 0)
            {
                ShawarmaArt.PrintWin();
                Console.WriteLine("SURVIVOR ENDING: You made it. You'll never sleep the same.");
            }
            else if (money < 0)
            {
                Console.WriteLine("INDENTURED ENDING: You survived, but you owe the shop $" + (-money) + ".");
                Console.WriteLine("You'll be working off the debt for a long, long time.");
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
