using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour
{
    public GameObject GameHandlerGO;

    private bool areFishSelected = false;
    private GameObject selectedFish;

    public GameObject SelecPlate;
    public GameObject NamePlate;
    public GameObject AgePlate;
    public GameObject TypePlate;
    public GameObject HungerPlate;
    public GameObject HappinesPlate;

    private bool isFishSelected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (areFishSelected && selectedFish != null)
        {
            AgePlate.GetComponent<TextMesh>().text = (selectedFish.GetComponent<FishStats>().currentAge.ToString());
            HappinesPlate.GetComponent<TextMesh>().text = (int)selectedFish.GetComponent<FishStats>().CurHappiness + "%";
            HungerPlate.GetComponent<TextMesh>().text = (int)selectedFish.GetComponent<FishStats>().CurStomach + "/" + selectedFish.GetComponent<FishStats>().RaceStomachSize;
        }
	}

    public void SelectFish(GameObject FishToSelect) {

        if (areFishSelected == true)
        {
            if (selectedFish != FishToSelect)
            {
                areFishSelected = true;
                selectedFish = FishToSelect;
                changeSelectMenu(false);
            }
        }
        else {
            areFishSelected = true;
            selectedFish = FishToSelect;
            changeSelectMenu(true);
        }
        
    }

    public void UnselectFish() {
        if (areFishSelected) {
            SelecPlate.GetComponent<SelectPlateScript>().DeselectFishNow();
            SelecPlate.active = false;
            areFishSelected = false;
        }
    }

    void changeSelectMenu(bool shouldWeEnable){
        SelecPlate.GetComponent<SelectPlateScript>().SelectFishNow(selectedFish);
        NamePlate.GetComponent<TextMesh>().text = (selectedFish.GetComponent<FishStats>().Name);
        AgePlate.GetComponent<TextMesh>().text = (selectedFish.GetComponent<FishStats>().currentAge.ToString());
        TypePlate.GetComponent<TextMesh>().text = (selectedFish.GetComponent<FishStats>().type);
        HappinesPlate.GetComponent<TextMesh>().text = (int)selectedFish.GetComponent<FishStats>().CurHappiness + "%";
        HungerPlate.GetComponent<TextMesh>().text = (int)selectedFish.GetComponent<FishStats>().CurStomach + "/" + selectedFish.GetComponent<FishStats>().RaceStomachSize;
        if (shouldWeEnable) {
            SelecPlate.active = true;
            NamePlate.active = true;
            AgePlate.active = true;
            TypePlate.active = true;
        }
    }
}
