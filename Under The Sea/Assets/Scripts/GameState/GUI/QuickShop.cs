using UnityEngine;
using System.Collections;

public class QuickShop : MonoBehaviour {

    public GameObject Food1Prefab;
    public int Food1Cost;
    public string Food1Name;

    public GameObject Food2Prefab;
    public int Food2Cost;
    public string Food2Name;

    public GameObject Object1Prefab;
    public int Object1ItemID;

    public GameObject Object2Prefab;
    public int Object2ItemID;

    public GameObject Object3Prefab;
    public int Object3ItemID;

    public GameObject Object4Prefab;
    public int Object4ItemID;

    private int currentPlacingID;

    private bool isFoodOpen = false;
    private bool isStashOpen = false;
    private bool areWePlacingObject = false;
    public bool AreQuickShopAvavible = true;

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
        if (AreQuickShopAvavible)
        {
        if (!isFoodOpen)
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Buy Food"))
            {
                isFoodOpen = !isFoodOpen;
                PlayerGO.GetComponent<InstantiateFood>().ResetInstantaiblePrefab();
            }

        }
        else
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Close Menu"))
            {
                isFoodOpen = !isFoodOpen;
                PlayerGO.GetComponent<InstantiateFood>().ResetInstantaiblePrefab();
                gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(StandardCursor);
            }
            if (GUI.Button(new Rect(110, 10, 120, 50), Food1Name + " " + Food1Cost + "$"))
            {
                PlayerGO.GetComponent<InstantiateFood>().ChangInstantaiblePrefab(Food1Prefab, Food1Cost);
                gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(foodCursor);
            }
            if (GUI.Button(new Rect(230, 10, 120, 50), Food2Name + " " + Food2Cost + "$"))
            {
                PlayerGO.GetComponent<InstantiateFood>().ChangInstantaiblePrefab(Food2Prefab, Food2Cost);
                gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(foodCursor);
            }
        }


        if (!isStashOpen)
        {
            if (GUI.Button(new Rect(10, 70, 100, 50), "Your Stash"))
                isStashOpen = !isStashOpen;
        }
        else
        {
            if (!areWePlacingObject && currentPlacingID == 0)
            {
                if (GUI.Button(new Rect(10, 70, 100, 50), "Close Stash"))
                {
                    isStashOpen = !isStashOpen;
                    PlayerGO.GetComponent<InstantiateFood>().ResetInstantaiblePrefab();
                }
            }

            if(areWePlacingObject && currentPlacingID != 0){
                if (GUI.Button(new Rect(10, 70, 100, 50), "Cancel"))
                {
                    PlayerGO.GetComponent<InstantiateFood>().ResetInstantaiblePrefab();
                    gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(StandardCursor);
                    areWePlacingObject = false;
                    currentPlacingID = 0;

                }
            }

            //Placing standard fish
            if (PlayerGO.GetComponent<PlayerStats>().basicFishToPlace >= 1 || currentPlacingID == Object1ItemID)
            {
                if (GUI.Button(new Rect(110, 70, 140, 50), "Gubby Fish: " + PlayerGO.GetComponent<PlayerStats>().basicFishToPlace))
                {
                    if (PlayerGO.GetComponent<PlayerStats>().basicFishToPlace >= 1)
                    {
                        PlayerGO.GetComponent<InstantiateFood>().ChangInstantaiblePrefab(Object1Prefab, 0,Object1ItemID);
                        gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(foodCursor);
                        currentPlacingID = Object1ItemID;
                        areWePlacingObject = true;
                    }
                }
            }

            //Placing Grim fish
            if (PlayerGO.GetComponent<PlayerStats>().grimFishToPlace >= 1 || currentPlacingID == Object2ItemID)
            {
                if (GUI.Button(new Rect(110, 121, 140, 50), "Grim Fish: " + PlayerGO.GetComponent<PlayerStats>().grimFishToPlace))
                {
                    if (PlayerGO.GetComponent<PlayerStats>().grimFishToPlace >= 1)
                    {
                        PlayerGO.GetComponent<InstantiateFood>().ChangInstantaiblePrefab(Object2Prefab, 0, Object2ItemID);
                        gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(foodCursor);
                        currentPlacingID = Object2ItemID;
                        areWePlacingObject = true;
                    }
                }
            }

            //Placing Chest cosmetic
            if (PlayerGO.GetComponent<PlayerStats>().ChestsToPlace >= 1 || currentPlacingID == Object3ItemID)
            {
                if (GUI.Button(new Rect(251, 70, 140, 50), "Chests: " + PlayerGO.GetComponent<PlayerStats>().ChestsToPlace))
                {
                    if (PlayerGO.GetComponent<PlayerStats>().ChestsToPlace >= 1)
                    {
                        PlayerGO.GetComponent<InstantiateFood>().ChangInstantaiblePrefab(Object3Prefab, 0, Object3ItemID);
                        gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(foodCursor);
                        currentPlacingID = Object3ItemID;
                        areWePlacingObject = true;
                    }
                }
            }

            //Placing Tang cosmetic
            if (PlayerGO.GetComponent<PlayerStats>().TangToPlace >= 1 || currentPlacingID == Object4ItemID)
            {
                if (GUI.Button(new Rect(251, 121, 140, 50), "Kelp: " + PlayerGO.GetComponent<PlayerStats>().TangToPlace))
                {
                    if (PlayerGO.GetComponent<PlayerStats>().TangToPlace >= 1)
                    {
                        PlayerGO.GetComponent<InstantiateFood>().ChangInstantaiblePrefab(Object4Prefab, 0, Object4ItemID);
                        gameObject.GetComponent<CursorScript>().ChangeTheCurrentCursor(foodCursor);
                        currentPlacingID = Object4ItemID;
                        areWePlacingObject = true;
                    }
                }
            }
        }
        }

        GUI.Box(new Rect(720, 10, 150, 20), "Wallet: " + PlayerGO.GetComponent<PlayerStats>().Currency + "$");
    }
}
