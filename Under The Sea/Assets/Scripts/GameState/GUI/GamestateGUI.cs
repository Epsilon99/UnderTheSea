using UnityEngine;
using System.Collections;

public class GamestateGUI : MonoBehaviour {

    public Texture2D StandardCursor;

    private Vector2 cursorHotspot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

	// Use this for initialization
	void Start () {
        StartCoroutine(SetNewCursor(StandardCursor));
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public IEnumerator SetNewCursor(Texture2D newCursor){
        yield return new WaitForSeconds(0.1f);
        Cursor.SetCursor(newCursor, cursorHotspot, cursorMode);
    }   
}
