using System.Data.SqlTypes;
using UnityEngine;

public class Statics : MonoBehaviour
{
    public static System.Random randyTheRandom = new();

    public static float money;

    public static float day;

    public static float starsGained;

    public static float approvalValue;

    public static float securityValue;
    
    public static string rejectionText1 = "Oh Jimminy Crickets I don't have enough money for that Item.";
    
    public static string rejectionText2 = "I already have that one... I'm such a clutz";

    public static string rejectionText3 = "<size=30>Hmm seems we can't buy this one yet. " +
        "Mayhap we need another license or higher ratings?";
    public static void ReadRejection1()
    {
        FindFirstObjectByType<ComedyDialogue>().ReadDescription(rejectionText1);
    }

    public static void ReadRejection2()
    {
        FindFirstObjectByType<ComedyDialogue>().ReadDescription(rejectionText2);
    }

    public static void ReadRejection3()
    {
        FindFirstObjectByType<ComedyDialogue>().ReadDescription(rejectionText3);
    }

    public static int FlipACoin()
    {
        int result = Random.Range(0, 2);
        return result;
    }

    public static int RollADice(int whichDice)
    {
        int result = 0;
        //d4
        if (whichDice == 0)
        {
            result = Random.Range(1, 5);
        }
        //d6
        if (whichDice == 1)
        {
            result = Random.Range(1, 7);
        }
        //d8
        if (whichDice == 2)
        {
            result = Random.Range(1, 9);
        }
        //d12
        if (whichDice == 3)
        {
            result = Random.Range(1, 13);
        }
        //d20
        if (whichDice == 4)
        {
            result = Random.Range(1, 21);
        }
        return result;
    }
}
