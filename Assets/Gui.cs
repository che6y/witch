using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {
	void OnGUI()
	{
		if (GUI.Button(new Rect(10,100,100,30), "Health Up"))
		{
			Player.player.Health+=10f;
		}
		if (GUI.Button(new Rect(10,140,100,30), "Health Down"))
		{
			Player.player.Health-=10f;
		}
		if (GUI.Button(new Rect(10,200,100,30), "Save"))
		{
			Player.player.Save();
		}
		if (GUI.Button(new Rect(10,240,100,30), "Load"))
		{
			Player.player.Load();
		}			
	}
}
