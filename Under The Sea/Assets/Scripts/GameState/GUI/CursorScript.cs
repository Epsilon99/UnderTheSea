using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{

    public Texture2D StandardCursor;
    public Camera MainCamera;
    public GameObject GameHandlerGO;

    private Vector3 mousePos;
    private Vector2 cursorHotspot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    private GameObject selectedFishGO;

    private bool areFishSelected = false;
    private bool isFishLifted = false;
    private bool readyToPlace = false;
    private bool didWeTakeAction = false;

    private float storedZAxis;
    private float minX, maxX, minY, maxY;
    public float WallOffsetX, WallOffsetY;

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
                if (hit.collider.tag == "Fish" && !areFishSelected)
                {
                    gameObject.GetComponent<GUIScript>().SelectFish(hit.collider.gameObject);
                    selectedFishGO = hit.collider.gameObject;
                    areFishSelected = true;
                }
                else if (hit.collider.tag == "Fish" && areFishSelected) {
                    if (hit.collider.gameObject == selectedFishGO && !isFishLifted)
                        liftFish(selectedFishGO);
                    else {
                        gameObject.GetComponent<GUIScript>().SelectFish(hit.collider.gameObject);
                        selectedFishGO = hit.collider.gameObject;
                    }
                }
                else if (hit.collider.tag == "MenuLeftArrow" || hit.collider.tag == "MenuRightArrow")
                {

                    if (hit.collider.tag == "MenuLeftArrow")
                    {
                        Debug.Log("derpLeft");
                        if (!isFishLifted)
                            gameObject.GetComponent<GUIScript>().UnselectFish();

                        GameHandlerGO.GetComponent<GameHandler>().ChangeAqaurium(true);
                        didWeTakeAction = true;
                    }
                    else if (hit.collider.tag == "MenuRightArrow")
                    {
                        Debug.Log("derpRight");
                        if (!isFishLifted)
                            gameObject.GetComponent<GUIScript>().UnselectFish();

                        GameHandlerGO.GetComponent<GameHandler>().ChangeAqaurium(false);
                        didWeTakeAction = true;
                    }
                }
            }
            else if (!isFishLifted) { 
                gameObject.GetComponent<GUIScript>().UnselectFish();
                areFishSelected = false;
            }
        }

        
        if(Input.GetMouseButtonUp(0)){
            if(isFishLifted && !readyToPlace){
                readyToPlace = true;
            }
        }
        
        if (Input.GetMouseButtonDown(0) && readyToPlace){
            if (!didWeTakeAction)
                placeFish(selectedFishGO);
        }

        if (isFishLifted) {
            mousePos = Input.mousePosition;
            mousePos = MainCamera.ScreenToWorldPoint(mousePos);
            selectedFishGO.transform.position = new Vector3(mousePos.x, mousePos.y-(selectedFishGO.transform.localScale.x/2), MainCamera.transform.position.z + 1);
        }

        didWeTakeAction = false;
    }

    public void ChangeTheCurrentCursor(Texture2D newCursor) {
        StartCoroutine(SetNewCursor(newCursor,0.0f));

    }

    private void liftFish(GameObject FishToLift) {
        storedZAxis = FishToLift.transform.position.z;
        FishToLift.transform.Rotate(0, 0, -90, Space.World);
        FishToLift.GetComponent<FishBehaviour>().areWeInAqarium = false;
        isFishLifted = true;

    }

    private void placeFish(GameObject FishToPlace) {
        FetchAquariumCords();
        mousePos = Input.mousePosition;
        mousePos = MainCamera.ScreenToWorldPoint(mousePos);

        if (mousePos.x >= minX && mousePos.x <= maxX && mousePos.y >= minY && mousePos.y <= maxY)
        {
            FishToPlace.transform.Rotate(0, 0, 90, Space.World);
            FishToPlace.transform.position = new Vector3(mousePos.x, mousePos.y, storedZAxis);
            isFishLifted = false;
            storedZAxis = 0;
        }
        readyToPlace = false;
    }

    private void FetchAquariumCords() {

        float FishSizeX = selectedFishGO.transform.localScale.x / 2;
        float FishSizeY = selectedFishGO.transform.localScale.x / 2;

        minX = GameHandlerGO.GetComponent<GameHandler>().AquariumMinX - FishSizeX + WallOffsetX;
        maxX = GameHandlerGO.GetComponent<GameHandler>().AquariumMaxX + FishSizeX - WallOffsetX;
        minY = GameHandlerGO.GetComponent<GameHandler>().AquariumMinY - FishSizeY + WallOffsetY;
        maxY = GameHandlerGO.GetComponent<GameHandler>().AquariumMaxY + FishSizeY - WallOffsetY;
    }

    private IEnumerator SetNewCursor(Texture2D newCursor,float excecutionTime)
    {
        yield return new WaitForSeconds(excecutionTime);
        Cursor.SetCursor(newCursor, cursorHotspot, cursorMode);
    }
}