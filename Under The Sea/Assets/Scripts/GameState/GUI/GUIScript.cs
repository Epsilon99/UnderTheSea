using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    public GameObject GameHandlerGO;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() { 
        if (GUI.Button(new Rect(10, Screen.height - 60, 100, 50), "LEFT"))
            GameHandlerGO.GetComponent<GameHandler>().ChangeAqaurium(true);

        if (GUI.Button(new Rect(Screen.width - 110, Screen.height - 60, 100, 50), "RIGHT"))
            GameHandlerGO.GetComponent<GameHandler>().ChangeAqaurium(false);
    }
}
