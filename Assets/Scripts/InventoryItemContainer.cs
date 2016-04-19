using UnityEngine;
using System.Collections.Generic;


//[CreateAssetMenu(fileName = "furniture", menuName = "Witch/Furniture", order = 1)]
public class InventoryItemContainer : ScriptableObject {
	public List<InventoryItem> Inventories = new List<InventoryItem>();
	
}