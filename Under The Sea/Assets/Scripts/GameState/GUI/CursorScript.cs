using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{

    public Texture2D StandardCursor;
    public Camera MainCamera;
    public GameObject GameHandlerGO;

    private Vector3 mousePos;
    private Vector2 cursorHotspot = Vector2.zero;
    private Quaternion fishStartRotation;
    private CursorMode cursorMode = CursorMode.Auto;

    private GameObject selectedFishGO;
    private bool areFishSelected = false;
    private bool isFishLifted = false;

    private GameObject selectedCosmeticGO;
    private bool ObjectIsSelected = false;
    private bool isOBjectLifted = false;

    private bool readyToPlace = false;
    private bool didWeTakeAction = false;


    private float storedZAxis;
    private float minX, maxX, minY, maxY;
    public float WallOffsetX, WallOffsetY, CosmeticOffsetX, CosmeticOffsetY;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SetNewCursor(StandardCursor,0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 7))
            {
                //Select and lift Fish
                if (hit.collider.tag == "Fish" && !areFishSelected && !isOBjectLifted)
                {
                    if (ObjectIsSelected) {
                        selectedCosmeticGO.GetComponent<CosmeticScript>().OurSelectParticle.active = false;
                        selectedCosmeticGO = null;
                        ObjectIsSelected = false;
                    }
                    gameObject.GetComponent<GUIScript>().SelectFish(hit.collider.gameObject);
                    selectedFishGO = hit.collider.gameObject;
                    selectedFishGO.GetComponent<FishBehaviour>().OurSelectParticle.active = true;
                    areFishSelected = true;
                }
                else if (hit.collider.tag == "Fish" && areFishSelected && selectedFishGO.GetComponent<FishBehaviour>().areWePlayingAnimation == false) {
                    if (hit.collider.gameObject == selectedFishGO && !isFishLifted)
                        liftFish(selectedFishGO);
                    else {
                        selectedFishGO.GetComponent<FishBehaviour>().OurSelectParticle.active = false;
                        gameObject.GetComponent<GUIScript>().SelectFish(hit.collider.gameObject);
                        selectedFishGO = hit.collider.gameObject;
                        selectedFishGO.GetComponent<FishBehaviour>().OurSelectParticle.active = true;
                    }
                }

                //Select and lift Object
                if (hit.collider.tag == "Cosmetic" && !ObjectIsSelected && !isFishLifted)
                {
                    if (areFishSelected)
                    {
                        gameObject.GetComponent<GUIScript>().UnselectFish();
                        selectedFishGO.GetComponent<FishBehaviour>().OurSelectParticle.active = false;
                        areFishSelected = false;
                    }
                    selectedCosmeticGO = hit.collider.gameObject;
                    selectedCosmeticGO.GetComponent<CosmeticScript>().OurSelectParticle.active = true;
                    ObjectIsSelected = true;
                }
                else if (hit.collider.tag == "Cosmetic" && ObjectIsSelected)
                {
                    if (hit.collider.gameObject == selectedCosmeticGO && !isOBjectLifted)
                        liftObject(selectedCosmeticGO);
                    else
                    {
                        selectedCosmeticGO.GetComponent<CosmeticScript>().OurSelectParticle.active = false;
                        selectedCosmeticGO = hit.collider.gameObject;
                        selectedCosmeticGO.GetComponent<CosmeticScript>().OurSelectParticle.active = true;
                    }
                }


                //Click Menu arrows
                else if (hit.collider.tag == "MenuLeftArrow" || hit.collider.tag == "MenuRightArrow")
                {

                    if (hit.collider.tag == "MenuLeftArrow")
                    {
                        if (!isFishLifted)
                        {
                            gameObject.GetComponent<GUIScript>().UnselectFish();
                            if (areFishSelected)
                                selectedFishGO.GetComponent<FishBehaviour>().OurSelectParticle.active = false;
                        }

                        GameHandlerGO.GetComponent<GameHandler>().ChangeAqaurium(true);
                        didWeTakeAction = true;
                    }
                    else if (hit.collider.tag == "MenuRightArrow")
                    {
                        if (!isFishLifted)
                        {
                            gameObject.GetComponent<GUIScript>().UnselectFish();
                            if (areFishSelected)
                                selectedFishGO.GetComponent<FishBehaviour>().OurSelectParticle.active = false;
                        }

                        GameHandlerGO.GetComponent<GameHandler>().ChangeAqaurium(false);
                        didWeTakeAction = true;
                    }
                }
            }

            else if (!isOBjectLifted && ObjectIsSelected) {
                selectedCosmeticGO.GetComponent<CosmeticScript>().OurSelectParticle.active = false;
                isOBjectLifted = false;
            }

            else if (!isFishLifted && areFishSelected)
            {
                gameObject.GetComponent<GUIScript>().UnselectFish();
                selectedFishGO.GetComponent<FishBehaviour>().OurSelectParticle.active = false;
                areFishSelected = false;
            }
        }

        
        if(Input.GetMouseButtonUp(0)){
            if(isFishLifted && !readyToPlace){
                readyToPlace = true;
            }
            else if (isOBjectLifted && !readyToPlace){
                readyToPlace = true;
            }
        }


        
        if (Input.GetMouseButtonDown(0) && readyToPlace){
            if (!didWeTakeAction)
            {
                if (isFishLifted)
                    placeFish(selectedFishGO);
                if (isOBjectLifted)
                    placeObject(selectedCosmeticGO);
            }
        }

        if (isFishLifted) {
            mousePos = Input.mousePosition;
            mousePos = MainCamera.ScreenToWorldPoint(mousePos);
            selectedFishGO.transform.position = new Vector3(mousePos.x, mousePos.y-(selectedFishGO.transform.localScale.x/2), MainCamera.transform.position.z + 1);
        }
        if (isOBjectLifted) {
            mousePos = Input.mousePosition;
            mousePos = MainCamera.ScreenToWorldPoint(mousePos);
            selectedCosmeticGO.transform.position = new Vector3(mousePos.x, mousePos.y, MainCamera.transform.position.z + 1);
        }

        if (Input.GetMouseButtonDown(1) && isOBjectLifted) {
            selectedCosmeticGO.GetComponent<CosmeticScript>().FlipTexture();
        }

        didWeTakeAction = false;
    }

    public void ChangeTheCurrentCursor(Texture2D newCursor) {
        StartCoroutine(SetNewCursor(newCursor,0.0f));

    }

    private void liftFish(GameObject FishToLift) {
        storedZAxis = FishToLift.transform.position.z;
        fishStartRotation = FishToLift.transform.rotation;

        if(FishToLift.GetComponent<FishBehaviour>().facingRight == true)
            FishToLift.transform.Rotate(0, 0, -90, Space.World);
        else
            FishToLift.transform.Rotate(0, 0, 90, Space.World);

        FishToLift.GetComponent<FishBehaviour>().areWeInAqarium = false;
        FishToLift.GetComponent<FishBehaviour>().PlayPickupAnimation();
        isFishLifted = true;

    }

    private void placeFish(GameObject FishToPlace) {
        FetchAquariumCords();
        mousePos = Input.mousePosition;
        mousePos = MainCamera.ScreenToWorldPoint(mousePos);

        if (mousePos.x >= minX && mousePos.x <= maxX && mousePos.y >= minY && mousePos.y <= maxY)
        {

            FishToPlace.transform.rotation = fishStartRotation;
            FishToPlace.transform.position = new Vector3(mousePos.x, mousePos.y, storedZAxis);
            isFishLifted = false;
            storedZAxis = 0;
        }
        readyToPlace = false;
    }

    private void liftObject(GameObject ObjectToLift) {
        storedZAxis = ObjectToLift.transform.position.z;
        ObjectToLift.GetComponent<CosmeticScript>().shallISink = false;
        isOBjectLifted = true;
    }

    private void placeObject(GameObject ObjectToPlace) {
        FetchAquariumCords();
        mousePos = Input.mousePosition;
        mousePos = MainCamera.ScreenToWorldPoint(mousePos);

        if (mousePos.x >= minX && mousePos.x <= maxX && mousePos.y >= minY && mousePos.y <= maxY)
        {
            ObjectToPlace.transform.position = new Vector3(mousePos.x, mousePos.y, storedZAxis);
            ObjectToPlace.GetComponent<CosmeticScript>().shallISink = true;
            isOBjectLifted = false;
            storedZAxis = 0;
        }
        readyToPlace = false;

    }

    private void FetchAquariumCords() {
        float ObjectSizeX = 0;
        float ObjectSizeY = 0;

        if (isFishLifted)
        {
            ObjectSizeX = selectedFishGO.transform.localScale.x / 2;
            ObjectSizeY = selectedFishGO.transform.localScale.x / 2;
        }
        
        if (isOBjectLifted) {
            ObjectSizeX = selectedCosmeticGO.transform.localScale.x/2 - CosmeticOffsetX;
            ObjectSizeY = selectedCosmeticGO.transform.localScale.x/2 - CosmeticOffsetY;
        }

        minX = GameHandlerGO.GetComponent<GameHandler>().AquariumMinX - ObjectSizeX + WallOffsetX;
        maxX = GameHandlerGO.GetComponent<GameHandler>().AquariumMaxX + ObjectSizeX - WallOffsetX;
        minY = GameHandlerGO.GetComponent<GameHandler>().AquariumMinY - ObjectSizeY + WallOffsetY;
        maxY = GameHandlerGO.GetComponent<GameHandler>().AquariumMaxY + ObjectSizeY - WallOffsetY;
    }

    private IEnumerator SetNewCursor(Texture2D newCursor,float excecutionTime)
    {
        yield return new WaitForSeconds(excecutionTime);
        Cursor.SetCursor(newCursor, cursorHotspot, cursorMode);
    }
}