using UnityEngine;

public class NewGame : MonoBehaviour
{
    public GameObject continueButton;
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
        SaveDataController.Instance.CurrentData.isNewGame = true;
        SaveDataController.Instance.CurrentData.furniturePositions.Clear();
    }
}
