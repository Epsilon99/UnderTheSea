using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public float MoveSpeed;
    private Vector3 destination;
    private bool areWeDone;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!areWeDone)
        {
            transform.position = Vector3.Lerp(transform.position, destination, MoveSpeed * Time.deltaTime);

            if (transform.position.x == destination.x && transform.position.y == destination.y)
            {
                areWeDone = true;  
            }
        }
	}

    public void ChangePosition(float Xcord,float Ycord){
        areWeDone = false;

        destination = new Vector3(Xcord,Ycord,transform.position.z);
    }
}
