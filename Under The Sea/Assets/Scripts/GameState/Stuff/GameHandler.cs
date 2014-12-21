using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

    public GameObject ActiveAquarium;
    public GameObject Camera;
    public GameObject SFXHandler;
    public AudioClip SoundArrow;

    public GameObject LeftArrow;
    public GameObject RightArrow;

    public Vector2 CanvasSize; 

    public List<GameObject> Aquariums = new List<GameObject>();
    private Transform thisTransform;

    private int curListNumber = 0;

    public float AquariumMinX, AquariumMaxX, AquariumMinY, AquariumMaxY;

    void Awake() {
        thisTransform = transform;
        FetchAllCurrentAquariums();
        SortList();
        ActiveAquarium = Aquariums[0];
        ChangeCameraPosition();

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
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FetchAllCurrentAquariums() {
        Aquariums.Clear();

        foreach (var Aquaria in GameObject.FindGameObjectsWithTag("Aquarium")) {
            Aquariums.Add(Aquaria);
        }

        if (Aquariums.Count == 2 && RightArrow.active != true)
            RightArrow.active = true;
    }

    public GameObject FetchLastAquarium() {
        int lastEntry = Aquariums.Count-1;
        return Aquariums[lastEntry];
    }

    void SortList() {
        Aquariums.Sort(delegate(GameObject c1, GameObject c2)
        {
            return Vector3.Distance(thisTransform.position, c1.transform.position).CompareTo((Vector3.Distance(thisTransform.position, c2.transform.position)));
        
        });
    }

    void ChangeCameraPosition() {
        Camera.GetComponent<CameraScript>().ChangePosition(ActiveAquarium.transform.position.x, ActiveAquarium.transform.position.y);

        Transform aquaCord = ActiveAquarium.transform;

        AquariumMinX = (aquaCord.position.x - (aquaCord.localScale.x / 2));
        AquariumMaxX = (aquaCord.position.x + (aquaCord.localScale.x / 2));
        AquariumMinY = (aquaCord.position.y - (aquaCord.localScale.y / 2));
        AquariumMaxY = (aquaCord.position.y + (aquaCord.localScale.y / 2));
    }

    public void ChangeAqaurium(bool left) {
        if (left)
        {
            if (curListNumber >= 1)
            {
                playSound(SoundArrow, false);
                ActiveAquarium = Aquariums[(curListNumber - 1)];
                curListNumber--;
                ChangeCameraPosition();
                if (curListNumber == 0)
                    LeftArrow.active = false;

                if (RightArrow.active == false)
                    RightArrow.active = true;
            }
        }
        else {
            if (curListNumber <= (Aquariums.Count - 2)) {
                playSound(SoundArrow, false);
                ActiveAquarium = Aquariums[(curListNumber + 1)];
                curListNumber++;
                ChangeCameraPosition();

                if (curListNumber+1 == Aquariums.Count)
                    RightArrow.active = false;

                if (LeftArrow.active == false)
                    LeftArrow.active = true;
            }
        }
    }

    private void playSound(AudioClip audioToPlay, bool loop)
    {
        SFXHandler.audio.clip = audioToPlay;
        if (loop)
            SFXHandler.audio.loop = true;
        SFXHandler.audio.Play();
    }
}
