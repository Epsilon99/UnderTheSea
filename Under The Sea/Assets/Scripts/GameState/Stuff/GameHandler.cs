using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

    public GameObject ActiveAquarium;
    public GameObject Camera;

    public List<GameObject> Aquariums = new List<GameObject>();
    private Transform thisTransform;

    private int curListNumber = 0;

    void Awake() {
        thisTransform = transform;
        FetchAllCurrentAquariums();
        SortList();
        ActiveAquarium = Aquariums[0];
        ChangeCameraPosition();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FetchAllCurrentAquariums() {
        foreach (var Aquaria in GameObject.FindGameObjectsWithTag("Aquarium")) {
            Aquariums.Add(Aquaria);
        }
    }

    void SortList() {
        Aquariums.Sort(delegate(GameObject c1, GameObject c2)
        {
            return Vector3.Distance(thisTransform.position, c1.transform.position).CompareTo((Vector3.Distance(thisTransform.position, c2.transform.position)));
        
        });
    }

    void ChangeCameraPosition() {
        Camera.GetComponent<CameraScript>().ChangePosition(ActiveAquarium.transform.position.x, ActiveAquarium.transform.position.y);
    }

    public void ChangeAqaurium(bool left) {
        if (left)
        {
            if (curListNumber >= 1)
            {
                ActiveAquarium = Aquariums[(curListNumber - 1)];
                curListNumber--;
                ChangeCameraPosition();
            }
        }
        else {
            if (curListNumber <= (Aquariums.Count - 2)) {
                ActiveAquarium = Aquariums[(curListNumber + 1)];
                curListNumber++;
                ChangeCameraPosition();
            }
        }
    }
}
