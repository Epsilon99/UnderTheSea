using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishNamerScript : MonoBehaviour
{
    new List<string> fishNames = new List<string> { "Max ", ",Bella ", "Bailey", "Lucy", "Charlie", "Molly", "Buddy", "Daisy", "Rocky", "Maggie", "Jake", "Sophie", "Jack", "Sadie", "Toby", "Chloe", "Cody", "Lola", "Buster", "Zoe", "Duke", "Cooper", "Abbey", "Riley", "Ginger", "Harley", "Roxy", "Bear", "Gracie", "Tucker", "Coco", "Murphy", "Sasha", "Lucky", "Lily", "Oliver", "Angel", "Sam", "Princess", "Oscar", "Emma", "Teddy", "Annie", "Winston", "Rosie", "Sammy", "Ruby", "Rusty", "Lady", "Shadow", "Missy", "Gizmo", "Lilly", "Bentley", "Mia", "Zeus", "Katie", "Jackson", "Zoey", "Baxter", "Madison", "Bandit", "Stella", "Gus", "Penny", "Samson", "Belle", "Milo", "Casey", "Rudy", "Samantha", "Louie", "Holly", "Hunter", "Lexi", "Lulu", "Rocco", "Brandy", "Sparky", "Jasmine", "Joey", "Shelby", "Bruno", "Sandy", "Beau", "Roxie", "Dakota", "Pepper", "Maximus", "Heidi", "Romeo", "Luna", "Boomer", "Dixie", "Luke", "Honey", "Henry", "Nemo", "Jolt", "Magnus", "Mick", "Daniel", "Rasmsu", "Lasse", "Bubbles", "Crimson", "Sushi", "Flash", "Ace", "Earl", "Ernie", "Dusty", "Fred", "Gabby", "Brat", "Gilbert", "Goldie", "Seuss", "Drago", "Bob", "Chip", "Chap", "Ratchet", "Elvis", "Deb", "Franky", "Hydra", "Rocket", "Sam", "Spud", "Stripes", "Alpha", "Beta", "Gamma" };
    new List<string> usedNames = new List<string>();

    private bool didWeEmptyListOfNames = false;

	// Update is called once per frame
	void Update () {
        if (!didWeEmptyListOfNames && fishNames.Count == 0) {
            didWeEmptyListOfNames = true;
        }

        if (didWeEmptyListOfNames && usedNames.Count == 0) {
            didWeEmptyListOfNames = false;
        }
	}

    public string NameMe(){
        if (!didWeEmptyListOfNames)
        {
            int WhichName = Random.Range(1, fishNames.Count + 1);
            string chosenName = fishNames[WhichName - 1];
            usedNames.Add(fishNames[WhichName - 1]);
            fishNames.Remove(fishNames[WhichName - 1]);

            return (chosenName);
            

        }
        else {
            int WhichName = Random.Range(1, usedNames.Count + 1);
            string chosenName = usedNames[WhichName - 1];
            fishNames.Add(fishNames[WhichName - 1]);
            usedNames.Remove(fishNames[WhichName - 1]);

            return (chosenName);
        }
    }
}
