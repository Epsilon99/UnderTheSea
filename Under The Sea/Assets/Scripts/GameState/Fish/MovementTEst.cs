using UnityEngine;
using System.Collections;

public class MovementTEst : MonoBehaviour {

    public float SwimSpeed;

    public float randomX;
    public float randomY;
    private float tChange;

    public float maxX, minX, maxY, minY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Time.time >= tChange)
        {
            randomX = Random.Range(-2.0f, 2.0f); // with float parameters, a random float
            randomY = Random.Range(-2.0f, 2.0f); //  between -2.0 and 2.0 is returned

            tChange = Time.time + Random.Range(4f, 6f);
        }
        */

        transform.Translate(new Vector3(randomX, randomY, 0) * SwimSpeed * Time.deltaTime);

        if (transform.position.x >= maxX || transform.position.x <= minX)
            randomX = -randomX;

        if (transform.position.y >= maxY || transform.position.y <= minY)
            randomY = -randomY;


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
	}
}
