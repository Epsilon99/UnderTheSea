using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AquariumScript : MonoBehaviour {

    private List<GameObject> foodInAquarium = new List<GameObject>();
    private List<GameObject> fishInAquarium = new List<GameObject>();
    public List<GameObject> datingList = new List<GameObject>();

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

    public void AddFishToDating(GameObject FishToAdd)
    {
        datingList.Add(FishToAdd);
    }

    public void RemoveFishFromDating(GameObject FishToRemove)
    {
        datingList.Remove(FishToRemove);
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

    public GameObject FindBreedingMate(GameObject SerchingFish) {
        if (datingList.Count >= 1)
        {
            int breedingMate = Random.Range(1, datingList.Count);
            datingList[breedingMate-1].GetComponent<FishBehaviour>().MyWaifu = SerchingFish;
            datingList[breedingMate-1].GetComponent<FishBehaviour>().weAreMating = true;
            return (datingList[breedingMate-1]);
        }
        else
            return (null);
    }
}
