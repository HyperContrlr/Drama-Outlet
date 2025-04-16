using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class Extensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> list)
    {
        List<T> shuffledList = list.OrderBy(x => Statics.randyTheRandom.Next()).ToList();
        return shuffledList;
    }
}
