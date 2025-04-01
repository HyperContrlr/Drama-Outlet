using NUnit.Framework;
using TMPro;
using UnityEngine;

public class GenericDisplayText<T> : MonoBehaviour
{
    public static void DisplayText<T>(TextMeshProUGUI textToDisplay, T value)
    {
        textToDisplay.text = string.Format($"{value}");
    }

    public static void DisplayTextWithExtra<T>(TextMeshProUGUI textToDisplay, T value, int whatOption)
    {
        if (whatOption == 0)
        {
            textToDisplay.text = string.Format($"${value}");
        }
        if (whatOption == 1)
        {
            textToDisplay.text = string.Format($"{value}");
        }
    }
}
