using UnityEngine;
using System.Collections;

public class InstantiateFood : MonoBehaviour {

    public GameObject FoodPrefab;
    public float OffsetX;
    public float OffsetY;

    private GameObject activeAquarium;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    // Use this for initialization
    void Start()
    {
        ChangeSpawnCords();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnFood(FoodPrefab);
            Debug.Log("min X " + minX);
            Debug.Log("max X " + maxX);
            Debug.Log("min Y " + minY);
            Debug.Log("max Y " + maxY);
        }
    }

    void SpawnFood(GameObject SpawningFood)
    {
            var mousePos = Input.mousePosition;
            mousePos.z = 5f;
            var objectPos = Camera.main.ScreenToWorldPoint(mousePos);


            GameObject clone = Instantiate(SpawningFood, new Vector3(Mathf.Clamp(objectPos.x, minX, maxX), Mathf.Clamp(objectPos.y, minY, maxY), objectPos.z), Quaternion.identity) as GameObject;
    }

    public void ChangeSpawnCords() {
        activeAquarium = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<GameHandler>().ActiveAquarium;
        Transform aquaCord = activeAquarium.transform;

        minX = (aquaCord.position.x - (aquaCord.localScale.x / 2)) + OffsetX;
        maxX = (aquaCord.position.x + (aquaCord.localScale.x / 2)) - OffsetX;
        minY = (aquaCord.position.y - (aquaCord.localScale.y / 2)) + OffsetY;
        maxY = (aquaCord.position.y + (aquaCord.localScale.y / 2)) - OffsetY;
    }
}
