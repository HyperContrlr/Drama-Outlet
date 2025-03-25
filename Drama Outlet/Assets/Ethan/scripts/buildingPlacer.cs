using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class buildingPlacer : MonoBehaviour
{
    public LayerMask floorLayerMask;

    private GameObject buildingPrefab;
    private GameObject toBuild;

    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;
    private void Awake()
    {
        mainCamera = Camera.main;
        buildingPrefab = null;
    }
    private void Update()
    {
        if (buildingPrefab != null)
        { //if in Build mode

            //hide pointer when hovering UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (toBuild.activeSelf) toBuild.SetActive(false);
            }
            else
            {
                 if (!toBuild.activeSelf) toBuild.SetActive(true);
            }

            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f, floorLayerMask))
            {
                if (!toBuild.activeSelf) toBuild.SetActive(true);
                toBuild.transform.position = hit.point;
            }
            else if (toBuild.activeSelf) toBuild.SetActive(false);
        }
    }

    public void SetBuildingPrefab(GameObject prefab)
    {
        buildingPrefab = prefab;
        _PrepareBuilding();
    }
    
    private void _PrepareBuilding()
    {
        if(toBuild) Destroy(toBuild);

        toBuild = Instantiate(buildingPrefab);
        toBuild.SetActive(false);
    }
}
