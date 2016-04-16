using UnityEngine;
//using System.Collections;
//using System;


[CreateAssetMenu(fileName = "Map", menuName = "Witch/Map", order = 1)]
public class Map : ScriptableObject {
	public ScriptableObject[] Rooms;
}