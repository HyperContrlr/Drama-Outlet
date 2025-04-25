using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectReset : MonoBehaviour
{
    [SerializeField] private List<products> allProducts;
    [SerializeField] private List<buildings> allBuildings;
    [SerializeField] private List<License> allLicenses;
    public void OnDestroy()
    {
        if (Statics.isNewGame == true)
        {
            foreach (var product in allProducts)
            {
                product.ResetValues();
            }
            foreach (var building in allBuildings)
            {
                building.ResetValues();
            }
            foreach (var license in allLicenses)
            {
                license.ResetValues();
            }
        }
    }
}
