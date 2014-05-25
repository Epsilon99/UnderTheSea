using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{

    public Texture2D StandardCursor;

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