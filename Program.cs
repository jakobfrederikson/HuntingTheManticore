// Game settings

bool vsComputer = Convert.ToBoolean(AskForNumberInRange("(0). Player vs Player | (1). Player vs Computer?", 0, 1));

int MANTICORE_DISTANCE;

if (vsComputer)
{
    Random random = new Random();
    MANTICORE_DISTANCE = random.Next(0, 101);
}
else
{
    MANTICORE_DISTANCE = AskForNumberInRange("Player 1, how far away from the city do you want to station the manticore?", 0, 100);

    // Clear screen, don't give away the game
    Console.Clear();
}

const int CITY_MAX_HEALTH = 15;
int cityHealth = CITY_MAX_HEALTH;

const int MANTICORE_MAX_HEALTH = 10;
int manticoreHealth = MANTICORE_MAX_HEALTH;

int round = 1;

// Game loop
while (true)
{
    // Check if manticore is dead
    if (manticoreHealth <= 0)
    {
        WriteLineWithColour("The Manticore has been destroyed! The city of Consolas has been saved!", ConsoleColor.Black, ConsoleColor.Green);
        break;
    }

    // Check if city is dead
    if (cityHealth <= 0)
    {
        WriteLineWithColour("The Manticore has destroyed the city! It roars in terror, whilst the townspeople flee for their lives...", ConsoleColor.Black, ConsoleColor.Red);
        break;
    }

    // Print new round dashes
    WriteLineWithColour(new string('-', 80), ConsoleColor.DarkBlue);

    DisplayStatus(round, cityHealth, manticoreHealth);    
    
    // Calculate and display cannon damage for the current round
    int cannonDamage = CalculateCannonDamage(round);
    System.Console.WriteLine($"The cannon is expected to deal {cannonDamage} damage this round.");

    // Determine and decide the shot outcome
    int desiredCannonRange = AskForNumberInRange("Enter desired cannon range:", 0, 100);

    if (CannonHitManticore(desiredCannonRange))
        manticoreHealth -= cannonDamage;

    // Manticore terrorizes city
    cityHealth--;

    round++;
}

void DisplayStatus(int currentRound, int currentCityHealth, int currentManticoreHealth)
{
    System.Console.Write($"STATUS: Round {round}  ");

    WriteWithColour("City: ", ConsoleColor.Cyan);
    if (cityHealth > 10)
    {
        WriteWithColour($"{cityHealth}/{CITY_MAX_HEALTH}  ", ConsoleColor.Green);
    }
    else if (cityHealth > 5 && cityHealth <= 10)
    {
        WriteWithColour($"{cityHealth}/{CITY_MAX_HEALTH}  ", ConsoleColor.Yellow);
    }
    else
    {
        WriteWithColour($"{cityHealth}/{CITY_MAX_HEALTH}  ", ConsoleColor.Red);
    }

    WriteWithColour("Manticore: ", ConsoleColor.DarkRed);
    if (manticoreHealth > 7)
    {
        WriteWithColour($"{manticoreHealth}/{MANTICORE_MAX_HEALTH}", ConsoleColor.Green);
    }
    else if (manticoreHealth > 4 && manticoreHealth <= 7)
    {
        WriteWithColour($"{manticoreHealth}/{MANTICORE_MAX_HEALTH}", ConsoleColor.Yellow);
    }
    else
    {
        WriteWithColour($"{manticoreHealth}/{MANTICORE_MAX_HEALTH}", ConsoleColor.Red);
    }

    // New line
    System.Console.WriteLine("");
}

int CalculateCannonDamage(int currentRound)
{
    if (currentRound % 3 == 0 && currentRound % 5 == 0)
    {
        // Fire-electric blast
        return 10;
    }        
    else if (currentRound % 3 == 0)
    {
        // Fire blast
        return 3;
    }        
    else if (currentRound % 5 == 0)
    {
        // Electric blast
        return 5;
    }        
    // Regular blast
    return 1;        
}

bool CannonHitManticore(int desiredCannonRange)
{
    if (desiredCannonRange == MANTICORE_DISTANCE)
    {
        WriteLineWithColour("That round was a DIRECT HIT!", ConsoleColor.Green);
        return true;
    }
    else if (desiredCannonRange > MANTICORE_DISTANCE)
    {
        WriteLineWithColour("That round OVERSHOT the target.", ConsoleColor.Yellow);
    }
        
    else if (desiredCannonRange < MANTICORE_DISTANCE)
    {
        WriteLineWithColour("That round FELL SHORT of the target.", ConsoleColor.Yellow);
    }        

    return false;
}

int AskForNumberInRange(string text, int min, int max)
{
    System.Console.Write($"{text} ");
    int number;

    try
    {
        number = Convert.ToInt32(Console.ReadLine());
        if (number < min || number > max)
        {
            System.Console.WriteLine($"The number needs to be between {min} and {max}. Try again.");
            number = AskForNumberInRange(text, min, max);
        }
    }
    catch
    {
        System.Console.WriteLine($"Did you write a number? Make sure the number you enter is between {min} and {max}. Try again.");
        number = AskForNumberInRange(text, min, max);
    }    

    return number;
}

void WriteWithColour(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
{
    ConsoleColor defaultForegroundColor = Console.ForegroundColor;
    ConsoleColor defaultBackgroundColor = Console.BackgroundColor;

    Console.ForegroundColor = foregroundColor;
    Console.BackgroundColor = backgroundColor;

    Console.Write(text);

    Console.ForegroundColor = defaultForegroundColor;
    Console.BackgroundColor = defaultBackgroundColor;
}

void WriteLineWithColour(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
{
    ConsoleColor defaultForegroundColor = Console.ForegroundColor;
    ConsoleColor defaultBackgroundColor = Console.BackgroundColor;

    Console.ForegroundColor = foregroundColor;
    Console.BackgroundColor = backgroundColor;

    Console.WriteLine(text);

    Console.ForegroundColor = defaultForegroundColor;
    Console.BackgroundColor = defaultBackgroundColor;
}