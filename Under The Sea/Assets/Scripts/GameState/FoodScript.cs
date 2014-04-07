using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour {

    public float Size;
    public float Speed;
    public float FeedValue;
    public string FoodType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.Translate(Vector3.down*Speed*Time.deltaTime)
	}

    void OnTriggerEnger(Collider other) {
        if (other.tag == "FloorTrigger") {
            StartCoroutine(DestroyObjectAfterTime(2f));
        }
    }

    public IEnumerator DestroyObjectAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }   
}
