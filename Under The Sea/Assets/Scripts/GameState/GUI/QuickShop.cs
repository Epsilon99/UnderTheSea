using UnityEngine;
using System.Collections;

public class QuickShop : MonoBehaviour {

    public GameObject FT1Prefab;
    public int FT1Cost;

    private bool isFoodOpen = false;

    public GameObject PlayerGO;

    void OnAwake() {
        
    }

	// Use this for initialization
	void Start () {
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
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
            }
            if (GUI.Button(new Rect(110, 10, 100, 50), "Sawdust 5$"))
                PlayerGO.GetComponent<InstantiateFood>().ChangInstantaiblePrefab(FT1Prefab, FT1Cost);
        }
    }
}
