using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour
{
    public float Lifetime;
    public float FeeddValue;
    public float Speed;

    private bool shallISink;

    private GameObject Particles;

    private Transform thisTransform;

    void OnEnable()
    {
        shallISink = true;
        thisTransform = transform;
        //SetFoodProps();
    }

    // Use this for initialization
    void Start()
    {
        var bubbles = transform.FindChild("Bubbles");
        Particles = bubbles.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (shallISink == true)
            thisTransform.Translate(Vector3.down * Speed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FloorTrigger")
        {
            shallISink = false;
            if (Particles != null)
            {
                Particles.GetComponent<ParticleSystem>().loop = false;
                Particles.GetComponent<ParticleSystem>().emissionRate = 0;
            }
            StartCoroutine(DelayDeath());
        }
        

    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }
}