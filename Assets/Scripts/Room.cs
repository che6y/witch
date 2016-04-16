using UnityEngine;
//using System.Collections;
//using System;


[CreateAssetMenu(fileName = "Room", menuName = "Witch/Room", order = 1)]
public class Room : ScriptableObject {
	
		public string WelcomeText;
		public RoomElement[] RoomElements;

	
}