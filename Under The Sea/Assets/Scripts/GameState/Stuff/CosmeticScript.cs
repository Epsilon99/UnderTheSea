using UnityEngine;
using System.Collections;

public class CosmeticScript : MonoBehaviour {

    public GameObject OurSelectParticle;
    public GameObject MyAquarium;

    public Material LeftTex;
    public Material RightTex;

    public float Speed;

    public bool shallISink = true;
    private bool right = true;

    private Transform thisTransform;

    void Awake() {
        thisTransform = transform;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (shallISink == true)
        {
            thisTransform.Translate(Vector3.down * Speed * Time.deltaTime);
        }
	}

    public void FlipTexture() {
        if (right)
            renderer.material = LeftTex;
        else
            renderer.material = RightTex;

        right = !right;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AquariumCosmeticArea")
            {
            shallISink = false;
        }
        if (other.gameObject.tag == "Aquarium") {
            MyAquarium = other.gameObject;
        }



    }

    void OnTriggerExit(Collider other)
    {
        if (other.collider.tag == "Aquarium")
        {
            MyAquarium = other.gameObject;
        }
    }
}
