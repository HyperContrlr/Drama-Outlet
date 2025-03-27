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
    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
        buildingPrefab = null;
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
                if (!toBuild.activeSelf) toBuild.SetActive(true);
                toBuild.transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition) + offset;

                if (Input.GetMouseButtonDown(0))
                {
                    BuildingManager m = toBuild.GetComponent<BuildingManager>();
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
        if(toBuild) Destroy(toBuild);

        toBuild = Instantiate(buildingPrefab);
        toBuild.SetActive(false);

        BuildingManager m = toBuild.GetComponent<BuildingManager>();
        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
    }
}
