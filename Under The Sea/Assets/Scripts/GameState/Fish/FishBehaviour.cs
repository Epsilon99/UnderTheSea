    
using UnityEngine;
using System.Collections;

public class FishBehaviour : MonoBehaviour {


    //For calculating different chances for the phases
    public int ChanceForBreedPhase = 10;
    public int ChanceForIdlePhase = 20;
    public int ChanceForMovePhase = 30;


    //How fast we can swim
    public float SwimSpeed;

    //Public variables to control time of different phases... This should be calculated later
    public float MaxIdlePhase;
    public float MinIdlePhase;
    public float MaxMovePhase;
    public float MinMovePhase;
    public float MaxBreedPhase;
    public float MinBreedPhase;

    public float OffsetX;
    public float OffsetY;

    //Floats regarding movementrestriction
    public float maxX, minX, maxY, minY;

    //Timer for phases
    private float phaseClock;

    //Floats regarding movement
    private float tChange = 0; //force new direction in the first update
    private float randomX;
    private float randomY;

    public bool areWeInAqarium = false;
    public bool areWeHungry = true;

    //Bools to keep track of phases
    private bool weAreEating;
    private bool ShouldWeMove;
    private bool ShouldWeBreed;
    private bool ShouldWeIdle;

    private GameObject ourAquarium;
    private Transform thisTransform;


    void Awake(){
        thisTransform = transform;
        ResetPhases();
        PickAPhase();
    }
	
	// Update is called once per frame
	void Update () {
	    if(Time.time < phaseClock && areWeInAqarium){
            if(ShouldWeMove == true){
                Movement();
            }
        }
        else if(!weAreEating)
        {
            ResetPhases();
            PickAPhase();
        }

        if (weAreEating && areWeInAqarium) {
            GameObject NearestFood = ourAquarium.GetComponent<AquariumScript>().FindClosestFoodToYou(thisTransform);
            
            if (NearestFood == null)
            {
                weAreEating = false;
                ResetPhases();
                PickAPhase();
            }
            else {
                transform.position = Vector3.Lerp(transform.position, NearestFood.transform.position, SwimSpeed * Time.deltaTime);

            }
        }
	}

    void StartIdlePhase() { 
        ShouldWeIdle = true;
        phaseClock = Time.time + RandomizePhasetime(MinIdlePhase,MaxIdlePhase);
    }

    void StartBreedPhase() { 
        ShouldWeBreed = true;
        phaseClock = Time.time + RandomizePhasetime(MinBreedPhase,MaxBreedPhase);
    }

    void StartMovePhase() { 
        ShouldWeMove = true;
        phaseClock = Time.time + RandomizePhasetime(MinMovePhase,MaxMovePhase);
    }

    void Movement(){
        if(Time.time >= tChange){
            randomX = Random.Range(-2.0f,2.0f); // with float parameters, a random float
            randomY = Random.Range(-2.0f,2.0f); //  between -2.0 and 2.0 is returned
            // set a random interval between 0.5 and 1.5
            tChange = Time.time + Random.Range(0.5f,1.5f);
        }

        transform.Translate(new Vector3(randomX, randomY, 0) * SwimSpeed * Time.deltaTime);

        // if object reached any border, revert the appropriate direction
        if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            randomX = -randomX;
        }
        

        if (transform.position.y >= maxY || transform.position.y <= minY){
           randomY = -randomY;
        }
        

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
    }

    //Stupid naming, but checks if we're hungry when we find food
    public void FoodIsServed() {
        if (areWeHungry) {
            weAreEating = true;
        }
    }

    private float RandomizePhasetime(float minValue, float maxValue){
        float returnValue = Random.Range(minValue, maxValue);
        return returnValue;
    }

    void ResetPhases(){
        ShouldWeBreed = false;
        ShouldWeIdle = false;
        ShouldWeMove = false;
    }

    void PickAPhase() {
        int TotalPhaseAmount = ChanceForBreedPhase + ChanceForIdlePhase + ChanceForMovePhase;
        int phaseValue = Random.Range(0, TotalPhaseAmount);

        if (phaseValue > 0 && phaseValue < ChanceForBreedPhase)
            StartBreedPhase();
        else if (phaseValue > ChanceForBreedPhase && phaseValue < (ChanceForBreedPhase + ChanceForIdlePhase))
            StartIdlePhase();
        else if (phaseValue > (ChanceForBreedPhase + ChanceForIdlePhase))
            StartMovePhase();
    }

    
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Aquarium" && !areWeInAqarium)
        {
            ourAquarium = other.gameObject;

            float curOffsetX = (OffsetX + (transform.localScale.x / 2f));
            float curOffsetY =(OffsetY + (transform.localScale.y / 2f));

            minX = (other.transform.position.x - (other.transform.localScale.x / 2) + curOffsetX);
            maxX = (other.transform.position.x + (other.transform.localScale.x / 2) - curOffsetX);
            minY = (other.transform.position.y - (other.transform.localScale.y / 2) + curOffsetY);
            maxY = (other.transform.position.y + (other.transform.localScale.y / 2) - curOffsetY);
            areWeInAqarium = true;

            other.gameObject.GetComponent<AquariumScript>().AddFishToList(gameObject);
            
        }

    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Aquarium" && !areWeInAqarium)
        {
            ourAquarium = null;
            areWeInAqarium = false;
            other.gameObject.GetComponent<AquariumScript>().RemoveFishFromList(gameObject);
        }
    }
          
    }


