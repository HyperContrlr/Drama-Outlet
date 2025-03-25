using Unity.VisualScripting;
using UnityEngine;

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
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, 1000f, floorLayerMask);
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
