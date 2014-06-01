using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour
{
    public float Lifetime;
    public float FeeddValue;
    public float Speed;
    public GameObject myGraphicGO;

    private bool shallISink;

    public GameObject MyAquarium;
    private GameObject Particles;

    private Transform thisTransform;

    void OnEnable()
    {
        myGraphicGO.transform.Rotate(new Vector3(Random.RandomRange(0, 361), Random.RandomRange(0, 361), Random.RandomRange(0, 361)));
        thisTransform = transform;
        shallISink = true;
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
        if (other.collider.tag == "Fish")
        {
            other.gameObject.GetComponent<FishBehaviour>().StartEatingNow();
            DestroyOurself();
        }
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