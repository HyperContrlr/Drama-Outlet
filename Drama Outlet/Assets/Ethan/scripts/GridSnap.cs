using UnityEngine;
public class GridSnap : MonoBehaviour
{
    private Grid grid;
    public void Start()
    {
        grid = Grid.FindFirstObjectByType<Grid>(); 
    } 
    public void Update() 
    { 
        Vector3Int yurr = grid.LocalToCell(transform.localPosition); 
        transform.localPosition = grid.GetCellCenterLocal(yurr); 
    }
}
