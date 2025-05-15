using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SaveData
{
    public enum Time { EarlyMorning, Morning, Afternoon, Evening, Night, Midnight }

    public Time timeOfDay;

    public float money;

    public float day;

    public bool isNewGame;

    public float starsGained;

    public float approvalValue;

    public float securityValue;

    public List<FurnitureData> furniturePositions;

    public List<InventoryItem> inventoryItems;

    public List<int> inventoryStocks;
}