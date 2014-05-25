using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AquariumScript : MonoBehaviour {

    private List<GameObject> foodInAquarium = new List<GameObject>();
    private List<GameObject> fishInAquarium = new List<GameObject>();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddFishToList(GameObject FishToAdd){
        fishInAquarium.Add(FishToAdd);
    }

    public void RemoveFishFromList(GameObject FishToRemove)
    {
        fishInAquarium.Remove(FishToRemove);
    }

    public void AddFoodToList(GameObject FoodToAdd)
    {
        foodInAquarium.Add(FoodToAdd);
    }

    public void RemoveFoodToList(GameObject FoodToRemove)
    {
        foodInAquarium.Remove(FoodToRemove);
    }

    public void AlertFishAboutFood(){
        foreach (var fish in fishInAquarium) {
            fish.GetComponent<FishBehaviour>().FoodIsServed();
        }
    }

    public GameObject FindClosestFoodToYou(Transform YourCurrentPositon) {
        
        foodInAquarium.Sort(delegate(GameObject c1, GameObject c2){
            return Vector2.Distance(YourCurrentPositon.position, c1.transform.position).CompareTo((Vector3.Distance(YourCurrentPositon.position, c2.transform.position)));
            });

        if (foodInAquarium.Count >= 1)
            return (foodInAquarium[0]);
        else
            return (null);
    }
}
