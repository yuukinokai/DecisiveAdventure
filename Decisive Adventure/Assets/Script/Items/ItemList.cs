using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public BaseItem[] itemList = new BaseItem[20];
    public BaseItem[] heroList = new BaseItem[5];

    static private ItemList itemListInstance;

    static public ItemList GetItemList(){
		if(itemListInstance == null){
			Debug.Log("Error : No itemList instance.");	
		}
		return itemListInstance;
	}

    public BaseItem GetItem(string itemName){
        foreach(BaseItem baseItem in itemList){
            if(string.Compare(baseItem.GetName(), itemName) == 0){
                return baseItem;
            }
        }
        Debug.Log("No game item of this name");
        return null;
    }

    void Awake(){
        itemListInstance = this;
    }

}
