using System.Data.SqlTypes;
using UnityEngine;

public class Statics : MonoBehaviour
{
    public static System.Random randyTheRandom = new();

    public static float money;

    public static float day;

    public static float approvalValue;

    public static float securityValue;
    
    public static string rejectionText1 = "Oh Jimminy Crickets I don't have enough money for that Item.";
    
    public static string rejectionText2 = "I already have that one... I'm such a clutz";

    public static string rejectionText3 = "Hmm seems we can't buy this one yet. Mayhaps we need another license or higher ratings?";

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
}
