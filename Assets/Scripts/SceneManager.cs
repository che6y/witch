using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneManager : MonoBehaviour {
	
	public Text Txt;
	public GameObject Button;
	public GameObject ButtonsContainer;
	public Data GameMap;
	public Canvas GameCanvas;
	private int currentRoom;

	void Start () {
		EnterToTheRoom (0);
	}

	void Update () {
	}
	void EnterToTheRoom(int roomIndex){
		currentRoom = roomIndex;
		Txt.text = GameMap.Rooms [roomIndex].WelcomeText;
		if (GameMap.Rooms [roomIndex].RoomElements.Length == 0) {
			GameObject continueButton = Instantiate (Button) as GameObject;
			continueButton.transform.SetParent(ButtonsContainer.transform,false);
			continueButton.GetComponentInChildren<Text>().text = "Продолжить";
			continueButton.GetComponent<Button> ().onClick.AddListener ( () => EnterToTheRoom (roomIndex + 1) );
			//TODO
			//continueButton.SetActive(true);
		} else {
			WalkingAround();
		}
	}

	public void WalkingAround(){
		DeleteButtons ();

		for (int i = 0; i < GameMap.Rooms [currentRoom].RoomElements.Length; i++) {
			GameObject elementButton = Instantiate (Button) as GameObject;
			elementButton.transform.SetParent (ButtonsContainer.transform, false);
			elementButton.transform.position = new Vector3 (ButtonsContainer.transform.position.x - 150f + (i * 120f), ButtonsContainer.transform.position.y, ButtonsContainer.transform.position.z);
			elementButton.GetComponentInChildren<Text>().text = GameMap.Rooms [currentRoom].RoomElements[i].ButtonTag;
			AddListener (elementButton.GetComponent<Button> (), i);
			//Debug.Log ("Button " + elementButton.GetComponentInChildren<Text> ().text + "is created");
		}
		StartCoroutine("AlignButtons");
	}

	void UsingElement(int elementIndex){
		DeleteButtons();
		Txt.text = GameMap.Rooms [currentRoom].RoomElements[elementIndex].Description;
		GameObject returnButton = Instantiate (Button) as GameObject;
		returnButton.transform.SetParent (ButtonsContainer.transform, false);
		returnButton.GetComponentInChildren<Text> ().text = "Вернуться";
		//returnButton.GetComponent<Button> ().onClick.AddListener (DeleteButtons);
		returnButton.GetComponent<Button> ().onClick.AddListener (() => EnterToTheRoom(currentRoom));
	}

//	public void DeleteButton(GameObject but){
//		Destroy (but);
//	}

	void AddListener(Button but, int index){
		but.onClick.AddListener ( () => UsingElement(index) );
	}

	void DeleteButtons(){
		Button[] buttons = GameCanvas.GetComponentsInChildren<Button> ();
		foreach (var b in buttons) {
			Destroy (b.gameObject);
			Destroy (b);
			//Debug.Log ("Deleted " + b.GetComponentInChildren<Text>().text);
		}
	}
	IEnumerator AlignButtons () {
		yield return 0;
		Button[] buttons = ButtonsContainer.GetComponentsInChildren<Button> ();

		float buttonWidth = Button.GetComponent<RectTransform> ().rect.width;
		float distance = 10f;
		float containerWidth = distance * (buttons.Length - 1) + buttons.Length * buttonWidth;
		ButtonsContainer.GetComponent<RectTransform> ().sizeDelta = new Vector2 (containerWidth, 100f);

		Debug.Log (buttons.Length);

		for (int i=0; i<buttons.Length; i++){
			//Debug.Log (buttons[i].GetComponentInChildren<Text>().text);
			buttons[i].GetComponent<RectTransform>().localPosition = new Vector3(
				(-containerWidth+ buttonWidth) / 2 + (distance + buttonWidth) * i,
				0,0);
			//TODO
			//buttons[i].gameObject.SetActive(true);
		}
		Debug.Log ("Align Done");
	}
}
