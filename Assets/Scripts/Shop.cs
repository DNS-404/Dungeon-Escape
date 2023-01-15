using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopMenu = null;
    [SerializeField] GameObject _playerGameObject = null;

    Player _player = null;
    int selectedItem = 0;
    int[] priceOfItems = { 100, 60, 150 };

    private void Start()
    {
        _player = _playerGameObject.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                player.isInShop = true;
                UIManager.Instance.OpenShop(player.gems);
            }
            shopMenu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
                player.isInShop = false;
            shopMenu.SetActive(false);
        }
    }

    public void SelectItem(int itemIndex)
    {
        selectedItem = itemIndex;
        Debug.Log("Item selected " + itemIndex);
        switch (itemIndex)
        {
            case 0: // Flame Sword
                UIManager.Instance.UpdateShopSelection(58);
                break;
            case 1: // Boots of Flight
                UIManager.Instance.UpdateShopSelection(-40);
                break;
            case 2: // Key to Castle
                UIManager.Instance.UpdateShopSelection(-134);
                break;
        }
    }

    public void Buy()
    {
        if (_player == null) return;
        int playerGems = _player.gems;
        if (priceOfItems[selectedItem] <= playerGems)
        {
            if(selectedItem == 2) // key
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            playerGems -= priceOfItems[selectedItem];
        }
        UIManager.Instance.playerGemCountText.text = playerGems.ToString() + " GEMS";
        UIManager.Instance.gemCount.text = playerGems.ToString();
        _player.gems = playerGems;
    }
}
