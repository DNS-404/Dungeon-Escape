using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is null!");
            }
            return _instance;
        }
    }

    public Text playerGemCountText;
    public Image selectionImg;
    public Text gemCount;
    public Image[] lifeUnits;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = gemCount.ToString() + " GEMS";
    }

    public void UpdateShopSelection(int yPos)
    {
        Vector2 newPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
        selectionImg.rectTransform.anchoredPosition = newPosition;
    }

    public void UpdateGems(int currentGemCount)
    {
        gemCount.text = currentGemCount.ToString();
    }

    public void UpdateLife(int health)
    {
        if (health >= 0 && health < lifeUnits.Length)
            lifeUnits[health].enabled = false;
    }
}
