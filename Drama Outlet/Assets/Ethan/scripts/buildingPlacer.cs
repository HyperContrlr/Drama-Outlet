using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class buildingPlacer : MonoBehaviour
{
    public static buildingPlacer instance;

    public bool hasProductManager;
    public bool hasStore;
    public GameObject floor;
    public LayerMask floorLayerMask;
    public LayerMask furnLayerMask;

    public Vector3 offset;

    public TilemapCollider2D collider;
    private GameObject buildingPrefab;
    public InventoryItem item;
    private GameObject toBuild;

    private Camera mainCamera;

    private Ray2D ray;
    private RaycastHit2D hit;

    public Vector2 actualBoxThang;

    public bool flipped;
    [HideInInspector] private bool devTools = false;
    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
        buildingPrefab = null;
        flipped = false;
    }
    private void Update()
    {
        if (buildingPrefab != null)
        { //if in Build mode
            actualBoxThang = toBuild.GetComponent<BuildingManager>().boxThang;

            if (Input.GetKeyDown(KeyCode.E))
            {
                
                if (item == null)
                {
                    Destroy(toBuild);
                    toBuild = null;
                    buildingPrefab = null;
                    return;
                }
                else
                {
                    Destroy(toBuild);
                    toBuild = null;
                    buildingPrefab = null;
                    return;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                toBuild.transform.Rotate(Vector3.up, 180);
                flipped = !flipped;
            }
            //hide pointer when hovering UI
            if (floor.GetComponent<level>().mouseOver)
            {
                if (toBuild.activeSelf) toBuild.SetActive(false);
            }
            else
            {
                 if (!toBuild.activeSelf) toBuild.SetActive(true);
            }
            RaycastHit2D furnHit = Physics2D.BoxCast(Camera.main.ScreenToWorldPoint(Input.mousePosition), actualBoxThang, 45f, Vector2.zero, 100f, furnLayerMask);
            //RaycastHit2D floorHit = Physics2D.BoxCast(Camera.main.ScreenToWorldPoint(Input.mousePosition), actualBoxThang, 45f, Vector2.zero, 100f, floorLayerMask);
            BuildingManager m = toBuild.GetComponent<BuildingManager>();
            if (furnHit.collider != null)
            {
                m.SetPlacementMode(PlacementMode.Invalid);
            }
            else 
            {
                m.SetPlacementMode(PlacementMode.Valid);
            }
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100f, floorLayerMask);
            if (hit.collider != null)
            {


                ////finds each object with the tag "furniture" and the script itemCollision, and gets specifically the itemCollision script
                //var furn = GameObject.FindGameObjectsWithTag("furniture");
                //foreach (var item in furn.Where(item2 => item2.GetComponent<itemCollision>()).Select(item2 => item2.GetComponent<itemCollision>()))
                //{
                //    if (item.GetComponent<itemCollision>().isOver == false)
                //    {
                //        m.SetPlacementMode(PlacementMode.Valid);
                //    }
                //    else if (item.GetComponent<itemCollision>().isOver == true)
                //    {
                //        m.SetPlacementMode(PlacementMode.Invalid);
                //    }
                //}
                if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.S))
                {
                    Debug.Log("Dev Tools Enabled");
                    devTools = true;
                }
                if (Input.GetKeyDown(KeyCode.P) && devTools)
                {
                    m.SetPlacementMode(PlacementMode.Invalid);
                }
                if (Input.GetKeyDown(KeyCode.O) && devTools)
                {
                    m.SetPlacementMode(PlacementMode.Valid);
                }
                if (Input.GetKey(KeyCode.M))
                    if(Input.GetKey(KeyCode.B))
                        if(Input.GetKeyDown(KeyCode.Keypad2))
                        {
                            Debug.Log("Micro-Byte 2 when???");
                        }
                if (!toBuild.activeSelf) toBuild.SetActive(true);
                toBuild.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;
                if (Input.GetMouseButtonDown(0))
                {
                    if (m.hasValidPlacement && item.stock >=1)
                    {
                        m.SetPlacementMode(PlacementMode.Fixed);
                        item.SubtractFromStock();

                        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && item.stock >= 1)
                        {
                            toBuild = null;
                            PrepareBuilding();
                            return;
                        }


                        buildingPrefab = null;
                        toBuild = null;
                        flipped = false;
                        collider.enabled = false;
                    }
                }
            }
            else if (toBuild.activeSelf) toBuild.SetActive(false);
            
        }
    }
    public void SetItem(InventoryItem item)
    {
        this.item = item;
    }
    public void SetBuildingPrefab(GameObject prefab)
    {
        collider.enabled = true;
        //buildingPrefab.GetComponent<InventoryItem>().stock--;
        buildingPrefab = prefab;
        PrepareBuilding();
    }
    
    private void PrepareBuilding()
    {
        //toBuild.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
        if(toBuild) Destroy(toBuild);

        toBuild = Instantiate(buildingPrefab);
        toBuild.SetActive(false);

        BuildingManager m = toBuild.GetComponent<BuildingManager>();
        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
    }
}
