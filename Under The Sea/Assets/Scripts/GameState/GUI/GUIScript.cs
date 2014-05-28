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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
            SelecPlate.active = false;
            areFishSelected = false;
        }
    }

    void changeSelectMenu(bool shouldWeEnable){
        NamePlate.GetComponent<TextMesh>().text = ("Name: " + selectedFish.GetComponent<FishStats>().Name);
        AgePlate.GetComponent<TextMesh>().text = ("Age: " + selectedFish.GetComponent<FishStats>().CurAge.ToString());
        TypePlate.GetComponent<TextMesh>().text = ("Type: " + selectedFish.GetComponent<FishStats>().type);
        if (shouldWeEnable) {
            SelecPlate.active = true;
            NamePlate.active = true;
            AgePlate.active = true;
            TypePlate.active = true;
        }
    }
}
