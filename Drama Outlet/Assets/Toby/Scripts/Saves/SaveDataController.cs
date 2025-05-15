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

    public bool isTitleScreen;
    public void Awake()
    {
        Instance = this;
        Load();
    }

    public void OnDestroy()
    {
        if (isTitleScreen == false)
        {
            _currentData.inventoryStocks.Clear();
            foreach (var item in _currentData.inventoryItems)
            {
                int stock = item.stock;
                _currentData.inventoryStocks.Add(stock);
            }
        }
        Save();
    }
    public void Save()
    {
        Serializer.Save(_currentData, $"{Application.persistentDataPath}/SaveData", fileName);
    }

    public void Load()
    {
        _currentData = Serializer.Load($"{Application.persistentDataPath}/SaveData", fileName, defaultData.value);
        if (isTitleScreen == false)
        {
            _currentData.inventoryItems = (Resources.FindObjectsOfTypeAll(typeof(InventoryItem)) as InventoryItem[]).ToList();
            foreach (var item in _currentData.inventoryStocks)
            {
                _currentData.inventoryItems[0].stock = item;
                _currentData.inventoryItems.Remove(_currentData.inventoryItems[0]);
            }
            _currentData.inventoryItems = (Resources.FindObjectsOfTypeAll(typeof(InventoryItem)) as InventoryItem[]).ToList();
        }
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
