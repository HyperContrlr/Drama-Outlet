using UnityEngine;
using UnityEngine.UIElements;

public class CursorSet : MonoBehaviour
{
    public Texture2D cursorTexture1;
    public Texture2D cursorTexture2;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSptot = Vector2.zero;
    void OnMouseEnter()
    {
        UnityEngine.Cursor.SetCursor(cursorTexture1, hotSptot, cursorMode);
    }

    private void OnMouseExit()
    {
        UnityEngine.Cursor.SetCursor(cursorTexture2, hotSptot, cursorMode);
    }

    public void Selected()
    {
        UnityEngine.Cursor.SetCursor(cursorTexture1, hotSptot, cursorMode);
    }

    public void Left()
    {
        UnityEngine.Cursor.SetCursor(cursorTexture2, hotSptot, cursorMode);
    }

    [ContextMenu("Cheat")]
    public void Cheat()
    {
        SaveDataController.Instance.CurrentData.money = 1000000;
        SaveDataController.Instance.CurrentData.approvalValue += 150;
    }
}

