using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class ButtonedPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RSIButtons buttons;
    public bool restock;
    public bool sell;
    public float timer;
    public bool heldDown;
    public KeyCode buttonKey;
    public void Update()
    {
        if (heldDown == true)
        {
            timer += Time.deltaTime;
        }
        if (restock == true && timer >= 3 && buttons.managerSelected == false)
        {
            RestockAll();
        }
        if (sell == true && timer >= 3 && buttons.managerSelected == false)
        {
            SellAll();
        }
        BKey();
    }

    public void BKey()
    {
        if (Input.GetKey(buttonKey) == true)
        {
            heldDown = true;
            if (restock == true && buttons.managerSelected == true)
            {
                buttons.currentSelectedObject.Restock();
            }
            else if (sell == true && buttons.managerSelected == true)
            {
                buttons.currentSelectedObject.Sell();
            }
            //else if (restock == true && timer >= 3 && buttons.managerSelected == false)
            //{
            //    timer = 0;
            //    heldDown = false;
            //    RestockAll();
            //}
            else if (sell == true && timer >= 3 && buttons.managerSelected == false)
            {
                SellAll();
            }
        }
        else if (Input.GetKeyUp(buttonKey)) 
        {
            heldDown = false;
            timer = 0;
        }
    }
    public void Restock()
    {
        if (buttons.managerSelected == true && buttons.currentSelectedObject.isProduct == true)
        {
            buttons.currentSelectedObject.Restock();
            buttons.managerSelected = false;
        }
        else
        {
            return;
        }
    }
    public void RestockAll()
    {
        buttons.RestockAll();
    }

    public void SellAll()
    {
        buttons.SellAll();
    }
    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        heldDown = true;
    }

    void IPointerUpHandler.OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        heldDown = false;
        timer = 0;
    }
}
