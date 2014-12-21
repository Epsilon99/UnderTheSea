using UnityEngine;
using System.Collections;

public class SelectPlateScript : MonoBehaviour {

    public Material FishVeryHappy;
    public Material FishHappy;
    public Material FishNormal;
    public Material FishSad;
    public Material FishVerySad;

    public GameObject Graphics;

    public GameObject FishWeHaveSelected;
    private bool AreFishSelected;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (AreFishSelected && FishWeHaveSelected != null) {
            int FishHappinessLevel = FishWeHaveSelected.GetComponent<FishStats>().HappinessLevel;
            setMaterial(FishHappinessLevel);
        }
	}

    public void SelectFishNow(GameObject FishToSelect) {
        FishWeHaveSelected = FishToSelect;
        AreFishSelected = true;
    }

    public void DeselectFishNow() {
        FishWeHaveSelected = null;
        AreFishSelected = false;
    }

    public void setMaterial(int whichMaterial) {
        switch (whichMaterial) { 
            case(5):
                Graphics.renderer.material = FishVeryHappy;
                break;

            case(4):
                Graphics.renderer.material = FishHappy;
                break;

            case(3):
                Graphics.renderer.material = FishNormal;
                break;

            case(2):
                Graphics.renderer.material = FishSad;
                break;
            
            case(1):
                Graphics.renderer.material = FishVerySad;
                break;
        
        }
    }
}
