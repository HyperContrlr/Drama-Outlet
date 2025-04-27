using UnityEngine;
using Pathfinding;

public class ObstacleUpdater : MonoBehaviour
{
   

    private void UpdateGraph()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            Bounds bounds = collider.bounds;
            AstarPath.active.UpdateGraphs(bounds);
        }
    }
}