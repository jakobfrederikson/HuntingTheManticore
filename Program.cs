// Game settings

bool vsComputer = Convert.ToBoolean(AskForNumberInRange("(0). Player vs Player | (1). Player vs Computer?", 0, 1));

int MANTICORE_DISTANCE;

if (vsComputer)
{
    Random random = new Random();
    MANTICORE_DISTANCE = random.Next(-1, 101);
}
else
{
    MANTICORE_DISTANCE = AskForNumberInRange("Player 1, how far away from the city do you want to station the manticore?", 0, 100);
}

const int CITY_MAX_HEALTH = 15;
int cityHealth = 15;

const int MANTICORE_MAX_HEALTH = 10;
int manticoreHealth = 10;

int round = 1;

// Game loop
while (true)
{
    // Print new round dashes
    System.Console.WriteLine(new string('-', 80));

    // Display current game status
    System.Console.WriteLine($"STATUS: Round {round}  City: {cityHealth}/{CITY_MAX_HEALTH}  Manticore: {manticoreHealth}/{MANTICORE_MAX_HEALTH}");
    
    // Calculate and display cannon damage for the current round
    int cannonDamage = CalculateCannonDamage(round);
    System.Console.WriteLine($"The cannon is expected to deal {cannonDamage} this round.");

    // Determine and decide the shot outcome
    int desiredCannonRange = AskForNumberInRange("Enter desired cannon range:", 0, 100);

    if (CannonHitManticore(desiredCannonRange))
        manticoreHealth -= cannonDamage;

    // Check if manticore is dead
    if (manticoreHealth <= 0)
    {
        System.Console.WriteLine("The Manticore has been destroyed! The city of Consolas has been saved!");
        break;
    }

    // Check if city is dead
    if (cityHealth == 0)
    {
        System.Console.WriteLine("The Manticore has destroyed the city! It roars in terror, whilst the towns people flee for their lives...");
        break;
    }

    // Manticore terrorizes city
    cityHealth--;

    round++;
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
        System.Console.WriteLine("That round was a DIRECT HIT!");
        return true;
    }
    else if (desiredCannonRange > MANTICORE_DISTANCE)
    {
        System.Console.WriteLine("That round OVERSHOT the target.");
    }
        
    else if (desiredCannonRange < MANTICORE_DISTANCE)
    {
        System.Console.WriteLine("That round FELL SHORT of the target.");
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