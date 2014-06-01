using UnityEngine;
using System.Collections;

public class QuickShop : MonoBehaviour {

    public GameObject Food1Prefab;
    public int Food1Cost;
    public string Food1Name;
    public GameObject Food2Prefab;
    public int Food2Cost;
    public string Food2Name;

    private bool isFoodOpen = false;

    public GameObject PlayerGO;

    public Texture2D StandardCursor;
    public Texture2D foodCursor;

    void OnAwake() {
        
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnGUI() {
        GUI.Box(new Rect(800, 10, 150, 20), "Wallet: " + PlayerGO.GetComponent<PlayerStats>().Currency + "$");

        if (!isFoodOpen)
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Buy Food"))
                isFoodOpen = !isFoodOpen;
        }
        else {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Close Menu"))
            {
                isFoodOpen = !isFoodOpen;
                PlayerGO.GetComponent<InstantiateFood>().ResetInstantaiblePrefab();
                gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(StandardCursor);
            }
            if (GUI.Button(new Rect(110, 10, 120, 50), Food1Name + " " + Food1Cost +"$")){
                PlayerGO.GetComponent<InstantiateFood>().ChangInstantaiblePrefab(Food1Prefab, Food1Cost);
                gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(foodCursor);
            }
            if (GUI.Button(new Rect(230, 10, 120, 50), Food2Name + " " + Food2Cost + "$"))
            {
                PlayerGO.GetComponent<InstantiateFood>().ChangInstantaiblePrefab(Food2Prefab, Food2Cost);
                gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(foodCursor);
            }
        }
    }
}
