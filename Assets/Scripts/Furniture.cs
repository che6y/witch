using UnityEngine;
//using System.Collections;
//using System;


[CreateAssetMenu(fileName = "furniture", menuName = "Witch/Furniture", order = 1)]
public class Furniture : RoomElement {
	public bool IsOpen;
    public InventoryItem[] InventoryItems;
    
}