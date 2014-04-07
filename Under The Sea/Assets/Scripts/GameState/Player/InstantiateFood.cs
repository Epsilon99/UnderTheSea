using UnityEngine;
using System.Collections;

public class InstantiateFood : MonoBehaviour {

    public GameObject FoodPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnFood(FoodPrefab);
        }
    }

    void SpawnFood(GameObject SpawningFood)
    {
            var mousePos = Input.mousePosition;
            mousePos.z = 5f;
            var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject clone = Instantiate(SpawningFood, objectPos, Quaternion.identity) as GameObject;
    }
}
