using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image[] itemSlot = new Image[20];
    [SerializeField] private GameObject eodScreen;

    private bool isActive = false;
    private BaseItem[] itemList;
    private int slotIndex;
    private Player player;

    void Start(){
        itemList = ItemList.GetItemList().itemList;
        if(itemList == null){
            Debug.Log("Item List Not Set");
        }
        player = Player.GetPlayer();
        if(player == null){
            Debug.Log("Player Not Set");
        }

        foreach(Image slot in itemSlot){
            //slot.SetActive(false);
        }
        slotIndex = 0;
    }

    public void DisplayInventory(){
        if(isActive) return;
        if(player == null) return;
        if(eodScreen == null){
			Debug.Log("No EOD screen");
			return;
		}
        if(itemList == null){
			Debug.Log("No itemList");
			return;
		}
        List<string> items = player.GetInventory();
        foreach(string itemName in items){
            Debug.Log(itemName);
            foreach(BaseItem baseItem in itemList){
                if(baseItem == null) continue;
                Debug.Log(baseItem.GetName());
                if(string.Compare(baseItem.GetName(), itemName) == 0){
                    Debug.Log("Changing Sprite to " + baseItem.GetName());
                    itemSlot[slotIndex++].sprite = baseItem.GetSprite();
                    //itemSlot[slotIndex].SetActive(true);
                }
            }
        }
        for(int i = slotIndex; i < 20; i++){
            Debug.Log("Destroy");
            Destroy(itemSlot[i]);
        }

        eodScreen.SetActive(true);
        isActive= true;
    }

    public void HideInventory(){
        if(eodScreen == null){
			Debug.Log("No EOD screen");
			return;
		}
        eodScreen.SetActive(false);
        isActive = false;
    }
}
