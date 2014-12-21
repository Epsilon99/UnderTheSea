using UnityEngine;
using System.Collections;

public class InstantiateFood : MonoBehaviour {

    public GameObject AquariumPrefab;
    private GameObject curPrefab = null;
    private int curCost = 0;
    private int currentCurrency;
    private int currentItemID;

    public float OffsetX;
    public float OffsetY;

    private GameObject PlayerGO;
    private GameObject activeAquarium;
    private GameObject curAquarium;
    private GameObject gameHandlerGO;
    

    private float minX,maxX,minY,maxY;

    // Use this for initialization
    void Start()
    {
        ChangeSpawnCords();
        currentCurrency = gameObject.GetComponent<PlayerStats>().Currency;
        gameHandlerGO = GameObject.FindGameObjectWithTag("GameHandler");
    }

    // Update is called once per frame
    void Update()
    {
        activeAquarium = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>().ActiveAquarium;

        if (Input.GetMouseButtonDown(0) && curPrefab != null)
        {
            
            if (curPrefab.tag == "Fish") {
                SpawnFish(curPrefab);
            }
            else if (curPrefab.tag == "Cosmetic")
            {
                SpawnObject(curPrefab);
            }
            else
            {
                if ((currentCurrency - curCost) >= 0)
                {
                    SpawnFood(curPrefab);
                }
            }
                
        }

        if (curAquarium != activeAquarium) {
            ChangeSpawnCords();
        }
    }

    void SpawnFood(GameObject SpawningFood)
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 5f;
        var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (objectPos.x >= minX && objectPos.x <= maxX && objectPos.y >= minY && objectPos.y <= maxY)
        {
            GameObject clone = Instantiate(SpawningFood, new Vector3(objectPos.x, objectPos.y, objectPos.z), Quaternion.identity) as GameObject;
            gameObject.GetComponent<PlayerStats>().Currency -= curCost;
            currentCurrency = gameObject.GetComponent<PlayerStats>().Currency;
            clone.GetComponent<FoodScript>().MyAquarium = curAquarium;
            curAquarium.GetComponent<AquariumScript>().AddFoodToList(clone);
            curAquarium.GetComponent<AquariumScript>().AlertFishAboutFood();
        }
             
    }

    void SpawnFish(GameObject SpawningFish)
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 5f;
        var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (objectPos.x >= minX && objectPos.x <= maxX && objectPos.y >= minY && objectPos.y <= maxY)
        {
            if (gameObject.GetComponent<PlayerStats>().CountItemStashed(currentItemID) >= 1)
            {
            GameObject Fish = Instantiate(SpawningFish, new Vector3(objectPos.x, objectPos.y, objectPos.z), Quaternion.identity) as GameObject;
            gameObject.GetComponent<PlayerStats>().RemoveItemToStash(currentItemID);
            }
        }
    }

    void SpawnObject(GameObject SpawningFish)
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 5f;
        var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (objectPos.x >= minX && objectPos.x <= maxX && objectPos.y >= minY && objectPos.y <= maxY)
        {
            if (gameObject.GetComponent<PlayerStats>().CountItemStashed(currentItemID) >= 1)
            {
                GameObject Cosmetic = Instantiate(SpawningFish, new Vector3(objectPos.x, objectPos.y, objectPos.z), Quaternion.identity) as GameObject;
                gameObject.GetComponent<PlayerStats>().RemoveItemToStash(currentItemID);
            }
        }
    }

    public void AddAquariumToList() {
        GameObject LastAquarium = gameHandlerGO.GetComponent<GameHandler>().FetchLastAquarium();
        Vector3 SpawnPos = new Vector3(LastAquarium.transform.position.x + 16f,LastAquarium.transform.position.y,LastAquarium.transform.position.z);
        GameObject Aquarium = Instantiate(AquariumPrefab, SpawnPos, Quaternion.identity) as GameObject;
        gameHandlerGO.GetComponent<GameHandler>().FetchAllCurrentAquariums();
    }

    public void ChangeSpawnCords() {
        activeAquarium = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>().ActiveAquarium;
        curAquarium = activeAquarium;

        Transform aquaCord = activeAquarium.transform;

        minX = (aquaCord.position.x - (aquaCord.localScale.x / 2)) + OffsetX;
        maxX = (aquaCord.position.x + (aquaCord.localScale.x / 2)) - OffsetX;
        minY = (aquaCord.position.y - (aquaCord.localScale.y / 2)) + OffsetY;
        maxY = (aquaCord.position.y + (aquaCord.localScale.y / 2)) - OffsetY;
    }

    public void ChangInstantaiblePrefab(GameObject NewPrefab, int CostOfItem) {
        curPrefab = NewPrefab;
        curCost = CostOfItem;
    }

    public void ChangInstantaiblePrefab(GameObject NewPrefab, int CostOfItem, int ItemID)
    {
        curPrefab = NewPrefab;
        curCost = CostOfItem;
        currentItemID = ItemID;
    }

    public void ResetInstantaiblePrefab() {
        curPrefab = null;
        curCost = 0;
        currentItemID = 0;
    }
}
