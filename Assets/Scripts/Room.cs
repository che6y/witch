using System;
using UnityEditor;
using UnityEngine;
[Serializable]
//Структура комнаты
public struct Room {
	[MultilineAttribute(2)]
	public string WelcomeText;
	public RoomElement[] RoomElements;

}

[Serializable]
public struct RoomElement {
	public string ButtonTag;
	public ElementType Type;
	public string Description;
	public bool IsDoorOpen;
	public int DoorLink;
	public string ClosedDoorText;

}

public enum ElementType {door, window, weapon, furniture};
