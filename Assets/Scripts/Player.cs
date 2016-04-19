using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour {
    
    public static Player player;
    public float Health;
    SceneManager SM;
    public List<InventoryItem> Pocket;

    void Awake() 
    {
        if (player == null) 
        {
            DontDestroyOnLoad(gameObject);
            player = this;
        }
        else if (player != this)
        {
          Destroy(gameObject);  
        }
    
    } 
    IEnumerator Start () {
        SM = GetComponent<SceneManager>();
        yield return new WaitForEndOfFrame();
        foreach (InventoryItem item in SM.playerPocket)
        {
            Pocket.Add(item);
        } 
    }
    void OnGUI () 
    {
        GUI.Label(new Rect(10,10,100,30), "Health: " + Health);
    }
   public void Save () 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath+"/playerInfo.dat");
        
        PlayerData data = new PlayerData();
        data.Health = Health;
        data.Pocket = Pocket;
        bf.Serialize(file,data);
        file.Close();
    }
   public void Load () 
    {
        if(File.Exists(Application.persistentDataPath+"/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath+"/playerInfo.dat",FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            
            Health = data.Health;
            Pocket = data.Pocket;
        }
        foreach (InventoryItem item in Pocket) {
			Debug.Log(item.Description);
		}
    }

}
[Serializable]
class PlayerData 
{
    public float Health;
    public List<InventoryItem> Pocket;
}