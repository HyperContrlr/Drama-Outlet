
using System.Collections.Generic;
using UnityEngine;


public enum PlacementMode
{
    Fixed,
    Valid,
    Invalid
}
public class BuildingManager : MonoBehaviour
{
    public buildings thisBuilding;
    public Material validPlaceMat;
    public Material invalidPlaceMat;
    public Material defaultPlaceMat;

    public MeshRenderer[] meshComps;
    private Dictionary<MeshRenderer, List<Material>> initialMaterials;

    [HideInInspector] public bool hasValidPlacement;
    [HideInInspector] public bool isFixed;

    public Vector2 boxThang;
    public GameObject parteecles;
    public GameObject hitbox;

    private int nObstacles;

    public void Update()
    {
        Statics.UpdateGraph(this.gameObject);
    }

    public void OnDisable()
    {
        if (thisBuilding.isSecurity == true)
        {
            SaveDataController.Instance.CurrentData.securityValue -= thisBuilding.securityBonus;
        }
    }
    private void Awake()
    {
        hasValidPlacement = true;
        isFixed = true;
        nObstacles = 0;
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;

        InitializeMaterials();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isFixed) return;
        if (IsGround(other.gameObject)) return;

        nObstacles++;
        SetPlacementMode(PlacementMode.Invalid);

    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        InitializeMaterials();
    }
#endif

    public void SetPlacementMode(PlacementMode mode)
    {
        if (mode == PlacementMode.Fixed)
        {
            isFixed = true;
            hasValidPlacement = true;
            this.gameObject.GetComponentInChildren<SpriteRenderer>().material = defaultPlaceMat;
            if (thisBuilding.isSecurity == true)
            {
                SaveDataController.Instance.CurrentData.securityValue += thisBuilding.securityBonus;
            }
            Invoke("SaveThang", 1f);
        }
        else if (mode == PlacementMode.Valid)
        {
            Debug.Log("Valid Placement");
            this.gameObject.GetComponentInChildren<SpriteRenderer>().material = validPlaceMat;
            hasValidPlacement = true;
        }
        else if (mode == PlacementMode.Invalid)
        {
            Debug.Log("Invalid Placement");
            this.gameObject.GetComponentInChildren<SpriteRenderer>().material = invalidPlaceMat;
            hasValidPlacement = false;
        }
        SetMaterial(mode);
    }
    public void SetMaterial(PlacementMode mode)
    {
        if (mode == PlacementMode.Fixed)
        {
            foreach (MeshRenderer r in meshComps)
                r.sharedMaterials = initialMaterials[r].ToArray();
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            parteecles.SetActive(true);
            hitbox.SetActive(true);
            this.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            Material matToApply = mode == PlacementMode.Valid
                ? validPlaceMat : invalidPlaceMat;

            Material[] m; int nMaterials;
            foreach (MeshRenderer r in meshComps)
            {
                nMaterials = initialMaterials[r].Count;
                m = new Material[nMaterials];
                for (int i = 0; i < nMaterials; i++)
                    m[i] = matToApply;
                r.sharedMaterials = m;
            }
        }

    }
    private void InitializeMaterials()
    {
        if (initialMaterials == null)
            initialMaterials = new Dictionary<MeshRenderer, List<Material>>();
        if (initialMaterials.Count > 0)
        {
            foreach (var l in initialMaterials) l.Value.Clear();
            initialMaterials.Clear();
        }

        foreach (MeshRenderer r in meshComps)
        {
            initialMaterials[r] = new List<Material>(r.sharedMaterials);
        }
    }

    private bool IsGround(GameObject o)
    {
        return ((1 << o.layer) & buildingPlacer.instance.floorLayerMask.value) != 0;
    }
    public void SaveThang()
    {
        FurnitureData data = new()
        {
            position = this.transform.position,
            rotation = this.transform.rotation,
            thisBuilding = thisBuilding,
        };
        SaveDataController.Instance.CurrentData.furniturePositions.Add(data);
    }
}
