using UnityEngine;
using System.Collections;

public class TutorialScreen : MonoBehaviour {
    public Vector2 CanvasSize;
    public Camera Main2DCamera;
    public GameObject SecondScreen;
    public int CurrentScreen = 1;
    public float speed;
    public bool hover;
    public bool areWeInPosition;
    public bool didWeTakeAnAction;


    void Awake() {
#if UNITY_WEBPLAYER
        // Execute javascript in iframe to keep the player centred
        string javaScript = @"
           window.onresize = function() {

             var unity = UnityObject2.instances[0].getUnity();

             var unityDiv = document.getElementById(""unityPlayerEmbed"");

             var width =  window.innerWidth;
             var height = window.innerHeight;

             var appWidth = " + CanvasSize.x + @";
             var appHeight = " + CanvasSize.y + @";

             unity.style.width = appWidth + ""px"";
             unity.style.height = appHeight + ""px"";

             unityDiv.style.marginLeft = (width - appWidth)/2 + ""px"";
             unityDiv.style.marginTop = (height - appHeight)/2 + ""px"";
             unityDiv.style.marginRight = (width - appWidth)/2 + ""px"";
             unityDiv.style.marginBottom = (height - appHeight)/2 + ""px"";

           }
           window.onresize(); // force it to resize now";
        Application.ExternalCall(javaScript);

#endif
    }

	// Use this for initialization
	void Start () {
        areWeInPosition = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && hover && CurrentScreen == 1 && !didWeTakeAnAction)
        {
            didWeTakeAnAction = true;
            areWeInPosition = false;
        }

        if (Input.GetMouseButtonDown(0) && hover && areWeInPosition && CurrentScreen == 2)
        {
            if (areWeInPosition && !didWeTakeAnAction)
            {
                Application.LoadLevel("SetupScene");
            }
        }

        if (Input.GetMouseButtonUp(0) && didWeTakeAnAction)
            didWeTakeAnAction = false;

        if (!areWeInPosition)
            {
                Vector3 destination = SecondScreen.transform.position;
                Vector3 dir = destination - Main2DCamera.transform.position;
                dir.z = 0;
                dir.Normalize();

                if (Main2DCamera.transform.position.x < destination.x)
                {
                    Main2DCamera.transform.Translate(dir * speed * Time.deltaTime);
                }
                else if (Main2DCamera.transform.position.x >= destination.x)
                {
                    areWeInPosition = true;
                    CurrentScreen += 1;
                }
        }
	}

    void OnMouseEnter() {
        hover = true;
    }

    void OnMouseExit() {
        hover = false;
    }

}
