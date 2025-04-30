using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class onEveryButtonClick : MonoBehaviour
{
    public UnityEvent onClickEvent;
    private void Awake()
    {
        foreach (Button button in FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            button.onClick.AddListener(() => onClickEvent.Invoke());
        }
    }
}
