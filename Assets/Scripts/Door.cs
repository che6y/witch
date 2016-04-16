using UnityEngine;
//using System.Collections;
//using System;


[CreateAssetMenu(fileName = "door", menuName = "Witch/Door", order = 1)]
public class Door : RoomElement {

	public Room Link;
	public bool IsDoorOpen;
	public string ClosedDescription;
	
}