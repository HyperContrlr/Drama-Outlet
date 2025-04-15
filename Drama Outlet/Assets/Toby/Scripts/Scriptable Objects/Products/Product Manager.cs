using UnityEngine;

public class ProductManager : MonoBehaviour
{
    [SerializeField] public products thisProduct;
    
    public void Buy(float itemsBought)
    {
        if (itemsBought > thisProduct.currentStock)
        {
            for (float i = thisProduct.currentStock; i == 0; i--)
            {
                thisProduct.stockBought += i;
            }
            thisProduct.currentStock = 0;
        }
        else
        {
            thisProduct.stockBought = itemsBought;
            thisProduct.currentStock -= itemsBought;
        }
        thisProduct.stockBought = 0;
        if (thisProduct.currentStock == 0)
        {
            //alert player to lack of stock
        }
    }
}
