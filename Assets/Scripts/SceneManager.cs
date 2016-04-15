using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SceneManager : MonoBehaviour {
	
	public Text Txt;
	public GameObject Button;
	public GameObject ButtonsContainer;
	public Data GameMap;
	public Canvas GameCanvas;
	public float ButtonDistance = 10f;
	private int currentRoom;
	private float buttonWidth;

	void Start () {
		buttonWidth = Button.GetComponent<RectTransform> ().rect.width;
		EnterToTheRoom (0);
	}

	void Update () {
	}
	void EnterToTheRoom(int roomIndex){
		currentRoom = roomIndex;
		Txt.text = GameMap.Rooms [roomIndex].WelcomeText;
		if (GameMap.Rooms [roomIndex].RoomElements.Length == 0) {
			MakeButton("Продолжить").GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (roomIndex + 1) );
		} else {
			WalkingAround();
		}
	}

	public void WalkingAround(){
		DeleteButtons ();
		int bAmount = GameMap.Rooms [currentRoom].RoomElements.Length;
		for (int i = 0; i < bAmount; i++) {
			var butt = MakeButton(GameMap.Rooms [currentRoom].RoomElements[i].ButtonTag, bAmount, i);
			AddListenetToButton (butt.GetComponent<Button> (), i);
		}
	}

	void UsingElement(int elementIndex){
		DeleteButtons();
		Txt.text = GameMap.Rooms [currentRoom].RoomElements[elementIndex].Description;
		ElementType elementType =  GameMap.Rooms [currentRoom].RoomElements[elementIndex].Type;
		//int bAmount = 1;
		switch (elementType)
		{
			case ElementType.door:
				//bAmount = 2;
				OpenDoor(GameMap.Rooms [currentRoom].RoomElements[elementIndex]);
				//MakeButton("Зайти", 2, 0).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (GameMap.Rooms [currentRoom].RoomElements[elementIndex].DoorLink) );
				break;
			case ElementType.window:
				break;
			case ElementType.weapon:
				break;
			case ElementType.furniture:
				break;
			default :
				MakeButton("Вернуться", 1,0).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (currentRoom) );
				break;
		}
		
	}
    // TODO Удалить
    void AddListenetToButton(Button but, int index){
		but.onClick.AddListener ( () => UsingElement(index) );
	}

	void DeleteButtons(){
		Button[] buttons = GameCanvas.GetComponentsInChildren<Button> ();
		foreach (var b in buttons) {
			Destroy (b.gameObject);
			Destroy (b);
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
	
	void OpenDoor(RoomElement door) {
		if (door.IsDoorOpen) {
			
			MakeButton("Зайти", 2, 0).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (door.DoorLink) );
			MakeButton("Вернуться", 2, 1).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (currentRoom) );
		} else {
			
			Txt.text = door.ClosedDoorText;
			MakeButton("Вернуться", 1, 0).GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (currentRoom) );
		}
	}

}
