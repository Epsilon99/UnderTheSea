using UnityEngine;
using System.Collections;

public class EnterExitScript : MonoBehaviour {
    
    private bool hover;
    public GameObject ShopHandler;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && hover) {
            ShopHandler.GetComponent<ShopHandler>().MoveTheShop();
        }
	}

    void OnMouseEnter()
    {
        hover = true;
    }

    void OnMouseExit()
    {
        hover = false;
    }
}
