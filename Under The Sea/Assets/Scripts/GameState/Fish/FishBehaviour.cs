    
using UnityEngine;
using System.Collections;

public class FishBehaviour : MonoBehaviour {


    //For calculating different chances for the phases
    public int ChanceForBreedPhase = 10;
    public int ChanceForIdlePhase = 20;
    public int ChanceForMovePhase = 30;
    public int MinBabyChance;
    public int MaxBabyChance;

    public int MyHusbandusBabychance;

    //How fast we can swim
    public float SwimSpeed;

    //Public variables to control time of different phases... This should be calculated later
    public float MaxMovePhase;
    public float MinMovePhase;
    public float MaxMateLookingPhase;
    public float MinMateLookingPhase;

    private float IdleTimer;

    public float OffsetX;
    public float OffsetY;

    //Floats regarding movementrestriction
    public float maxX, minX, maxY, minY;

    //Timer for phases
    private float phaseClock;

    //Floats regarding movement
    private float tChange = 0; //force new direction in the first update
    private float randomX = 0.1f;
    private float randomY;
    private float preRandX = 0.1f;

    public bool areWeInAqarium = false;
    public bool areWeHungry = true;
    public bool weAreMating = false;

    //Bools to keep track of phases
    private bool weAreEating;
    private bool ShouldWeMove;
    private bool ShouldWeLookForMate;
    private bool ShouldWeIdle;
    private bool didWeReset = false;
    private bool areWeTurning = false;

    private GameObject ourAquarium;
    public GameObject MyHusbandu;
    public GameObject MyWaifu;
    private Transform thisTransform;
    public GameObject AnimHandler;
    public GameObject OurParticle;


    void Awake(){
        thisTransform = transform;     
    }

    void Start() {
        phaseClock = Time.time + 2;
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time < phaseClock && areWeInAqarium){
            if(ShouldWeMove == true){
                Movement();
            }
           
        }
        else if (Time.time >= phaseClock && !didWeReset)
        {
            if(areWeInAqarium && !weAreEating && !weAreMating){
                didWeReset = true;
                ResetPhases();
                PickAPhase();
            }
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

        if(weAreMating && areWeInAqarium){
            if(MyHusbandu != null){
                if(MyHusbandusBabychance != null){
                    ourAquarium.GetComponent<AquariumScript>().RemoveFishFromDating(gameObject);

                    int OurBabyChance = (MyHusbandusBabychance + (Random.Range(MinBabyChance, MaxBabyChance))) / 2;
                    if (OurBabyChance >= Random.Range(MinBabyChance, MaxBabyChance)) {
                        Debug.Log("I got pregnant, yay");
                    }

                    weAreMating = false;

                    MyHusbandu = null;
                    ResetPhases();
                    PickAPhase();
                    
                }
            }
        
            if (MyWaifu != null){
                ourAquarium.GetComponent<AquariumScript>().RemoveFishFromDating(gameObject);

                MyWaifu.GetComponent<FishBehaviour>().MyHusbandusBabychance = Random.Range(MinBabyChance, MaxBabyChance);

                weAreMating = false;
                MyWaifu = null;
                ResetPhases();
                PickAPhase();
                
            }
        }
	}

    void StartIdlePhase() { 
        int randomIdle = Random.Range(1, 4);

        switch (randomIdle) { 
            case(1):
                IdleTimer = AnimHandler.GetComponent<FishAnimation>().RuntimeIdle1;
                break;

            case(2):
                IdleTimer = AnimHandler.GetComponent<FishAnimation>().RuntimeIdle2;
                break;

            case(3):
                IdleTimer = AnimHandler.GetComponent<FishAnimation>().RuntimeIdle3;
                break;
        }

        AnimHandler.GetComponent<FishAnimation>().Idle(randomIdle);
        phaseClock = Time.time + IdleTimer - 0.4f;
        ShouldWeIdle = true;
    }

    public void StartBreedPhase() {
        ShouldWeLookForMate = true;
        phaseClock = Time.time + RandomizePhasetime(MinMateLookingPhase,MaxMateLookingPhase);
        //AnimHandler.GetComponent<FishAnimation>().Idle(1);

        
        MyHusbandu = ourAquarium.GetComponent<AquariumScript>().FindBreedingMate(gameObject);
        if (MyHusbandu == null)
        {
            ourAquarium.GetComponent<AquariumScript>().AddFishToDating(gameObject);
        }
        else
        {
            weAreMating = true;
        }
        

    }

    void StartMovePhase() { 
        ShouldWeMove = true;
        AnimHandler.GetComponent<FishAnimation>().Swimming(true);
        phaseClock = Time.time + RandomizePhasetime(MinMovePhase,MaxMovePhase);
    }

    void Movement(){
        if (Time.time >= tChange && !areWeTurning)
        {
            preRandX = randomX;         
            randomX = Random.Range(-2.0f,2.0f); // with float parameters, a random float
            randomY = Random.Range(-2.0f,2.0f); //  between -2.0 and 2.0 is returned

            if (preRandX > 0f && randomX < 0f)
            {
                StartCoroutine(TurnFish(true,false));
            }
            else if (preRandX < 0f && randomX > 0f)
            {
                StartCoroutine(TurnFish(false,false));
            }

            // set a random interval between 0.5 and 1.5
            tChange = Time.time + Random.Range(4f,6f);
        }

        transform.Translate(new Vector3(randomX, randomY, 0f) * SwimSpeed * Time.deltaTime);

        // if object reached any border, revert the appropriate direction
        if (transform.position.x >= maxX || transform.position.x <= minX)
        {
            if (randomX > 0f)
            {
                StartCoroutine(TurnFish(true,true));
            }
            if (randomX < 0f)
            {
                StartCoroutine(TurnFish(false,true));
            }

            preRandX = -randomX;
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

    public void PlayPickupAnimation() {
        AnimHandler.GetComponent<FishAnimation>().ResetAllVariables(false);
        AnimHandler.GetComponent<FishAnimation>().PickedUp(true);
    }

    private IEnumerator TurnFish(bool left,bool isItTurn)
    {
        areWeTurning = true;
        float runtime = AnimHandler.GetComponent<FishAnimation>().RuntimeTurnL;
        float preSpeed = SwimSpeed;

        if (left)
            AnimHandler.GetComponent<FishAnimation>().Turn(true);
        else
            AnimHandler.GetComponent<FishAnimation>().Turn(false);
        phaseClock = phaseClock + runtime;

        if (!isItTurn)
        {
            SwimSpeed = 0;
            yield return new WaitForSeconds(runtime - 0.2f);
            SwimSpeed = preSpeed;
            areWeTurning = false;
        }
        else {
            yield return new WaitForSeconds(runtime - 0.2f);
            areWeTurning = false;
        }

        
    }

    private float RandomizePhasetime(float minValue, float maxValue){
        float returnValue = Random.Range(minValue, maxValue);
        return returnValue;
    }

    void ResetPhases(){

        AnimHandler.GetComponent<FishAnimation>().ResetAllVariables(true);
        if(ShouldWeLookForMate)
            ourAquarium.GetComponent<AquariumScript>().RemoveFishFromDating(gameObject);
        ShouldWeLookForMate = false;
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
        didWeReset = false;
    }

    public IEnumerator waitForSeconds(float secondsTowait) {
        yield return new WaitForSeconds(secondsTowait);
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

            AnimHandler.GetComponent<FishAnimation>().PickedUp(false);
            
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