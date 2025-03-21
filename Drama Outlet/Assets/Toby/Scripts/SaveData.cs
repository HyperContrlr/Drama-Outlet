using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct SaveData
{
    public bool isNewGame;
    
    public float money;

    public int day;

    public int stars;

    public float approvalBar;

    public float securityBar;

    public List<GameObject> Inventory;

    //public List<Licenses> licenses;

    public int shopLevel;

    public List<Vector3> shopLayout;

}

