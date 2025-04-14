using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class buildingPlacer : MonoBehaviour
{
    public static buildingPlacer instance;

    public GameObject floor;
    public LayerMask floorLayerMask;

    public Vector3 offset;

    private GameObject buildingPrefab;
    private GameObject toBuild;

    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;

    public bool flipped;
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

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(toBuild);
                toBuild = null;
                buildingPrefab = null;
                return;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                toBuild.transform.Rotate(Vector3.up, 180);
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
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, floorLayerMask) || floor.GetComponent<level>().mouseOver)
            {
                BuildingManager m = toBuild.GetComponent<BuildingManager>();

                //finds each object with the tag "furniture" and the script itemCollision, and gets specifically the itemCollision script
                var furn = GameObject.FindGameObjectsWithTag("furniture");
                foreach (var item in furn.Where(item2 => item2.GetComponent<itemCollision>()).Select(item2 => item2.GetComponent<itemCollision>()))
                {
                    if (item.GetComponent<itemCollision>().isOver == false)
                    {
                        m.SetPlacementMode(PlacementMode.Valid);
                    }
                    else if (item.GetComponent<itemCollision>().isOver == true)
                    {
                        m.SetPlacementMode(PlacementMode.Invalid);
                    }
                }

                if (Input.GetKeyDown(KeyCode.P))
                {
                    m.SetPlacementMode(PlacementMode.Invalid);
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    m.SetPlacementMode(PlacementMode.Valid);
                }
                if (!toBuild.activeSelf) toBuild.SetActive(true);
                toBuild.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;

                if (Input.GetMouseButtonDown(0))
                {
                    if (m.hasValidPlacement)
                    {
                        m.SetPlacementMode(PlacementMode.Fixed);

                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            toBuild = null;
                            PrepareBuilding();
                            return;
                        }


                        buildingPrefab = null;
                        toBuild = null;
                    }
                }
            }
            else if (toBuild.activeSelf) toBuild.SetActive(false);
            
        }
    }
    public void SetBuildingPrefab(GameObject prefab)
    {
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
