using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour
{
    public float Lifetime;
    public float FeeddValue;
    public float Speed;

    private bool shallISink;

    public GameObject MyAquarium;
    private GameObject Particles;

    private Transform thisTransform;

    void OnEnable()
    {
        thisTransform = transform;
        shallISink = true;
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

    
    void OnCollisionEnter(Collision other)
    {
        DestroyOurself();
    }

    void DestroyOurself() {
        MyAquarium.GetComponent<AquariumScript>().RemoveFoodToList(gameObject);
        Destroy(gameObject);
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(Lifetime);
        DestroyOurself();
    }
}