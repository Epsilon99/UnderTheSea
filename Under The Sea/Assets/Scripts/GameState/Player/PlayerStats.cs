using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public int Currency;

    public int basicFishToPlace;
    public int grimFishToPlace;
    public int ChestsToPlace;
    public int TangToPlace;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddItemToStash(int ItemID) { 
        switch(ItemID){
            case(1):
                basicFishToPlace += 1;
                break;
            case (2):
                grimFishToPlace += 1;
                break;

            case (3):
                ChestsToPlace += 1;
                break;

            case (4):
                TangToPlace += 1;
                break;

            case(5):
                gameObject.GetComponent<InstantiateFood>().AddAquariumToList();
                break;
        }
    }

    public void RemoveItemToStash(int ItemID)
    {
        switch (ItemID)
        {
            case (1):
                basicFishToPlace -= 1;
                break;

            case (2):
                grimFishToPlace -= 1;
                break;

            case (3):
                ChestsToPlace -= 1;
                break;

            case (4):
                TangToPlace -= 1;
                break;

        }
    }

    public int CountItemStashed(int ItemID)
    {
        switch (ItemID)
        {
            case (1):
                return basicFishToPlace;
                break;

            case (2):
                return grimFishToPlace;
                break;

            case (3):
                return ChestsToPlace;
                break;

            case (4):
                return TangToPlace;
                break;
        }
        return(0);
    }
}
