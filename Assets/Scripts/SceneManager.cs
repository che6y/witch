using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class SceneManager : MonoBehaviour {
	public Room FirstRoom;
	public Text Txt;
	public GameObject Button;
	public GameObject ButtonsContainer;
	public Canvas GameCanvas;
	public float ButtonDistance = 10f;
	private Room currentRoom;
	private float buttonWidth;
	public List<InventoryItem> playerPocket = new List<InventoryItem>();
	void Start () {
		buttonWidth = Button.GetComponent<RectTransform> ().rect.width;
		EnterToTheRoom (FirstRoom);
	}
	void EnterToTheRoom(Room room){
		currentRoom = room;
		Txt.text = room.WelcomeText;
		WalkingAround();
	}
	public void WalkingAround(){
		DeleteButtons ();
		Txt.text = currentRoom.WelcomeText;
		int bAmount = currentRoom.RoomElements.Length;
		for (int i = 0; i < bAmount; i++) {
			var butt = MakeButton(currentRoom.RoomElements[i].ButtonTag, bAmount, i);
			var element = currentRoom.RoomElements[i];
			butt.GetComponent<Button> ().onClick.AddListener (() => UsingElement(element));
		}
	}
	void UsingElement(RoomElement element){
		DeleteButtons();
		Txt.text = element.Description;
		string elementType = element.GetType().ToString();
		switch (elementType)
		{
			case "Continue":
				Continue cont = element as Continue;
				EnterToTheRoom(cont.Link);
				break;
			case "Door":
				OpenDoor(element as Door);
				break;
			case "Window":
				MakeButton("Вернуться", 1, 0).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (currentRoom) );
				break;
			case "Furniture":
				MakeButton("Заглянуть", 2, 0).GetComponent<Button> ().onClick.AddListener ( () => LookInsideFurniture (element as Furniture) );
				MakeButton("Вернуться", 2, 1).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (currentRoom) );
				break;
			default :
				MakeButton("Вернуться", 1, 0).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (currentRoom) );
				break;
		}
	}
	void DeleteButtons(){
		Button[] buttons = GameCanvas.GetComponentsInChildren<Button> ();
		foreach (var b in buttons) {
			Destroy (b.gameObject);
		}
	}
	GameObject MakeButton (string txt, int bAmount = 1, int bNumber = 0) {
		GameObject tempButton = Instantiate (Button) as GameObject;
		tempButton.transform.SetParent (ButtonsContainer.transform, false);
		tempButton.GetComponentInChildren<Text>().text = txt;
		float buttonContainerWidth = ButtonDistance * (bAmount - 1) + bAmount * buttonWidth;
		tempButton.GetComponent<RectTransform>().localPosition = new Vector3(
				(-buttonContainerWidth + buttonWidth) / 2 + (ButtonDistance + buttonWidth) * bNumber, 0, 0);
        return tempButton;
	}
	void OpenDoor(Door door) {
		if (door.IsDoorOpen) {
			MakeButton("Зайти", 2, 0).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (door.Link) );
			MakeButton("Вернуться", 2, 1).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (currentRoom) );
		} else {
			Txt.text = door.ClosedDescription;
			MakeButton("Вернуться", 1, 0).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (currentRoom) );
		}
	}
	void LookInsideFurniture (Furniture furniture) {
		DeleteButtons();
		var text = "Вы увидели ";
		var divider = "";
		if (furniture.InventoryItems.Length > 1)
			divider = ", ";
		foreach ( var item in furniture.InventoryItems)
		{
			text += item.Description + divider;
		}
		Txt.text = text;
		MakeButton("Взять", 2, 0).GetComponent<Button> ().onClick.AddListener ( () => TakeAnItems (furniture.InventoryItems) );
		MakeButton("Вернуться", 2, 1).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (currentRoom) );
	}
	void TakeAnItems(InventoryItem[] items) {
		foreach (InventoryItem item in items)
		{
			playerPocket.Add(item);
		}
		Array.Clear (items, 0, items.Length); 
		
		foreach (InventoryItem item in playerPocket) {
			Debug.Log(item.Description);
		}
	}
}
