using UnityEngine;
using System.Collections;

public class InstantiateFood : MonoBehaviour {

    private GameObject curPrefab = null;
    private int curCost = 0;
    private int currentCurrency;

    public float OffsetX;
    public float OffsetY;

    private GameObject activeAquarium;

    private float minX,maxX,minY,maxY;

    // Use this for initialization
    void Start()
    {
        ChangeSpawnCords();
        currentCurrency = gameObject.GetComponent<PlayerStats>().Currency;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && curPrefab != null)
        {
            if ((currentCurrency - curCost) >= 0)
            {
                SpawnFood(curPrefab);  
            }
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
        }
             
    }

    public void ChangeSpawnCords() {
        activeAquarium = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>().ActiveAquarium;
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

    public void ResetInstantaiblePrefab() {
        curPrefab = null;
        curCost = 0;
    }
}
