using UnityEngine;

public class CursorButtons : MonoBehaviour
{
    public CursorSet cursorSet;
   
    public void Set()
    {
        cursorSet.Selected();
    }

    public void Leave()
    {
        cursorSet.Left();
    }
}
