using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{

    public Texture2D StandardCursor;
    public Camera MainCamera;

    private Vector2 cursorHotspot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

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
                if (hit.collider.tag == "Fish")
                {
                    gameObject.GetComponent<GUIScript>().SelectFish(hit.collider.gameObject);
                }
            }
            else
                gameObject.GetComponent<GUIScript>().UnselectFish();
            
        }
    }

    public void ChangeTheCurrentCursor(Texture2D newCursor) {
        StartCoroutine(SetNewCursor(newCursor,0.0f));

    }

    private IEnumerator SetNewCursor(Texture2D newCursor,float excecutionTime)
    {
        yield return new WaitForSeconds(excecutionTime);
        Cursor.SetCursor(newCursor, cursorHotspot, cursorMode);
    }
}