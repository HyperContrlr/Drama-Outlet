using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    public static SaveDataController Instance;

    public string fileName;

    public SaveDataAsset defaultData;

    private SaveData _currentData;
    public ref SaveData CurrentData => ref _currentData;

    [SerializeField] private List<products> allProducts;
    [SerializeField] private List<buildings> allBuildings;
    [SerializeField] private List<License> allLicenses;
    public GameObject tutorial;
    public void Awake()
    {
        Instance = this;
        Load();
    }

    public void OnDestroy()
    {
        Save();
    }
    public void Save()
    {
        Serializer.Save(_currentData, $"{Application.persistentDataPath}/SaveData", fileName);
    }

    public void Load()
    {
        _currentData = Serializer.Load($"{Application.persistentDataPath}/SaveData", fileName, defaultData.value);
        ResetObjects();
    }

    public void ResetObjects()
    {
        if (_currentData.isNewGame == true)
        {
            tutorial.SetActive(true);
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
