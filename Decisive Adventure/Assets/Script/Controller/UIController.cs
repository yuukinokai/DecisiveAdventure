using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject[] itemSlot = new GameObject[20];
    [SerializeField] private GameObject[] partySlot = new GameObject[5];
    [SerializeField] private GameObject eodScreen;

    [SerializeField] private GameObject[] inventorySlot = new GameObject[20];
    [SerializeField] private GameObject inventoryScreen;

    private bool isActive = false;
    private bool isGiveActive = false;
    private BaseItem[] itemList;
    private BaseItem[] heroList;
    private int slotIndex;
    private int partyIndex;
    private int inventoryIndex;
    private Player player;

    void Start(){
        itemList = ItemList.GetItemList().itemList;
        heroList = ItemList.GetItemList().heroList;
        if(itemList == null || heroList == null){
            Debug.Log("UICONTROLLER : Item List or Hero List Not Set");
        }
        player = Player.GetPlayer();
        if(player == null){
            Debug.Log("UICONTROLLER : Player Not Set");
        }

        slotIndex = 0;
        inventoryIndex = 0;
        inventoryScreen.SetActive(false);
        eodScreen.SetActive(false);
        ResetInventory();
    }

    public void Click(int i)
    {
        if (isGiveActive)
        {
            Debug.Log(" UICONTROLLER : Clicked on " + inventorySlot[i].name);
            EventManager.TriggerEvent("Give" + inventorySlot[i].name);
            Refresh();
        }
    }

    public void Refresh()
    {
        ResetInventory();
        isGiveActive = false;
    }

    public void ResetInventory()
    {
        foreach(GameObject item in itemSlot)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in inventorySlot)
        {
            item.SetActive(false);
        }
        foreach(GameObject party in partySlot)
        {
            party.SetActive(false);
        }
    }

    public void DisplayGive()
    {
        if (isGiveActive) return;
        if (player == null) return;
        if (inventoryScreen == null)
        {
            Debug.Log("UICONTROLLER : No Inventory screen");
            return;
        }
        if (itemList == null)
        {
            Debug.Log("UICONTROLLER : No itemList");
            return;
        }
        List<string> items = player.GetInventory();
        foreach (string itemName in items)
        {
            foreach (BaseItem baseItem in itemList)
            {
                if (baseItem == null) continue;
                if (string.Compare(baseItem.GetName(), itemName) == 0)
                {
                    Debug.Log("UICONTROLLER : Changing Sprite to " + baseItem.GetName());
                    inventorySlot[inventoryIndex].name = baseItem.GetName();
                    inventorySlot[inventoryIndex].GetComponent<Image>().sprite = baseItem.GetSprite();
                    int amount = player.GetAmount(itemName);
                    Debug.Log(amount.ToString());
                    Text newText = inventorySlot[inventoryIndex].GetComponentsInChildren<Text>()[0];
                    newText.text = amount.ToString();
                    inventorySlot[inventoryIndex++].SetActive(true);
                }
            }
        }
        /*
        for (int i = inventoryIndex; i < 20; i++)
        {
            //Debug.Log("Destroy");
            //Destroy(itemSlot[i]);
            inventorySlot[i].SetActive(false);
        }*/
        inventoryIndex = 0;

        inventoryScreen.SetActive(true);
        isGiveActive = true;
    }

    public void HideGive()
    {
        if (inventoryScreen == null)
        {
            Debug.Log(" UICONTROLLER : No inventory screen");
            return;
        }
        EventManager.TriggerEvent("DoneGiving");
        inventoryScreen.SetActive(false);
        isGiveActive = false;
    }

    /// <summary>
    /// Used for End of Day, sorry for my bad naming 
    /// </summary>
    public void DisplayInventory(){
        if(isActive) return;
        if(player == null) return;
        if(eodScreen == null){
			Debug.Log("UICONTROLLER : No EOD screen");
			return;
		}
        if(itemList == null){
			Debug.Log("UICONTROLLER : No itemList");
			return;
		}
        List<string> items = player.GetInventory();
        foreach(string itemName in items){
            foreach(BaseItem baseItem in itemList){
                if(baseItem == null) continue;
                if(string.Compare(baseItem.GetName(), itemName) == 0){
                    Debug.Log("UICONTROLLER : Changing Sprite to " + baseItem.GetName());
                    itemSlot[slotIndex].GetComponent<Image>().sprite = baseItem.GetSprite();
                    int amount = player.GetAmount(itemName);
                    itemSlot[slotIndex].GetComponent<ItemImage>().text.text = amount.ToString();
                    itemSlot[slotIndex++].SetActive(true);
                }
            }
        }
        List<Hero> party = player.GetParty();
        foreach(Hero partyMember in party)
        {
            foreach(BaseItem ally in heroList)
            {
                if (ally == null) continue;
                Debug.Log(partyMember.GetName());
                if (string.Compare(ally.GetName(), partyMember.GetName()) == 0)
                {
                    Debug.Log("UICONTROLLER : Changing Sprite to " + ally.GetName());
                    partySlot[partyIndex].GetComponent<Image>().sprite = ally.GetSprite();
                    partySlot[partyIndex++].SetActive(true);
                }
            }
        }
        /*
        for(int i = slotIndex; i < 20; i++){
            //Debug.Log("Destroy");
            //Destroy(itemSlot[i]);
            itemSlot[i].SetActive(false);
        }*/

        eodScreen.SetActive(true);
        isActive= true;
    }

    public void HideInventory(){
        if(eodScreen == null){
			Debug.Log("UICONTROLLER : No EOD screen");
			return;
		}
        eodScreen.SetActive(false);
        isActive = false;
    }
}
