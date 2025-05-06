using UnityEngine;

public class NewGame : MonoBehaviour
{
    public GameObject continueButton;
    public SaveDataAsset defaultData;
    void Start()
    {
        if (SaveDataController.Instance.CurrentData.isNewGame == true)
        {
            continueButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(true);
        }
    }

    public void New()
    {
        SaveDataController.Instance.CurrentData = defaultData.value;
        SaveDataController.Instance.CurrentData.isNewGame = true;
        SaveDataController.Instance.CurrentData.furniturePositions.Clear();
    }
}
