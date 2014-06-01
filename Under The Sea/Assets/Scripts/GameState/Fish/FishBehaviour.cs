    
using UnityEngine;
using System.Collections;

public class FishBehaviour : MonoBehaviour {

    //For calculating different chances for the phases
    public int ChanceForBreedPhase;
    public int ChanceForIdlePhase;
    public int ChanceForMovePhase;
    public int MinBabyChance;
    public int MaxBabyChance;
    public GameObject Babyfish;

    public int MyHusbandusBabychance;

    //How fast we can swim
    public float SwimSpeed;

    //Public variables to control time of different phases... This should be calculated later
    public float MaxMovePhase;
    public float MinMovePhase;
    public float MaxMateLookingPhase;
    public float MinMateLookingPhase;
    public float breedCooldown;

    private float IdleTimer;

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
    public bool weAreMating = false;
    public bool facingRight = true;

    //Bools to keep track of phases
    private bool weAreEating;
    private bool ShouldWeMove;
    private bool ShouldWeLookForMate;
    private bool ShouldWeIdle;
    private bool didWeReset = false;
    public bool areWePlayingAnimation = false;
    private bool areWeHavingSex;
    private bool canWeBreed = true;
    private bool areWeABaby = false;


    private GameObject ourAquarium;
    public GameObject MyHusbandu;
    public GameObject MyWaifu;
    private Transform thisTransform;
    public GameObject AnimHandler;
    public GameObject OurSelectParticle;
    public GameObject OurPickupParticle;
    public GameObject GameHandler;
    public GameObject SFXHandler;

    public AudioClip SoundEating;
    public AudioClip SoundSplashing;
    public AudioClip SoundPlace;

    void OnEnable() { 
        SFXHandler = GameObject.FindGameObjectWithTag("FishSFX");
        GameHandler = GameObject.FindGameObjectWithTag("GameHandler");
        gameObject.GetComponent<FishStats>().Name = GameHandler.GetComponent<FishNamerScript>().NameMe();
    }

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
                RandomMovement();
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
                if (!areWePlayingAnimation)
                    MoveTowardsObject(NearestFood);

            }
        }

        if(weAreMating && areWeInAqarium){
            if (MyHusbandu != null && canWeBreed)
            {
                if(MyHusbandusBabychance != null){
                    ourAquarium.GetComponent<AquariumScript>().RemoveFishFromDating(gameObject);

                    if (areWeHavingSex)
                    {
                        //play animation when we start breeding
                        AnimHandler.GetComponent<FishAnimation>().ResetAllVariables(false);
                        AnimHandler.GetComponent<FishAnimation>().Mating(true);
                        float animationTime = AnimHandler.GetComponent<FishAnimation>().RuntimeParring;

                        //Make sure we stay in animation, for its duration
                        StartCoroutine(WaitSecondsThenReset(animationTime*2));

                        //check if we get pregnatnt
                        int OurBabyChance = (MyHusbandusBabychance + (Random.Range(MinBabyChance, MaxBabyChance))) / 2;
                        //If so, spawn baby
                        if (OurBabyChance >= Random.Range(MinBabyChance, MaxBabyChance))
                        {
                            GameObject Fish = Instantiate(Babyfish, new Vector3(thisTransform.position.x - thisTransform.localScale.y / 2, thisTransform.position.y, thisTransform.position.z), Quaternion.identity) as GameObject;
                            Fish.GetComponent<FishBehaviour>().setBabyPhase(2);
                            Fish.transform.localScale = new Vector3(Fish.transform.localScale.x / 1.2f, Fish.transform.localScale.y / 1.2f, Fish.transform.localScale.z / 1.2f);
                        }

                        //Reset stuff
                        StartCoroutine(BreedCooldown(breedCooldown));
                        MyHusbandusBabychance = 0;
                        areWeHavingSex = false;
                        weAreMating = false;
                        MyHusbandu = null;
                    }
                    else {
                        float distanceToPartner = Vector2.Distance(thisTransform.position, MyHusbandu.transform.position);
                        if (distanceToPartner <= (thisTransform.localScale.x))
                            areWeHavingSex=true;
                        else
                            MoveTowardsObject(MyHusbandu);
                    }    
                }

            }

            if (MyWaifu != null && canWeBreed)
            {
                if(areWeHavingSex){
                    //We're done looking for partners, remove us from list
                    ourAquarium.GetComponent<AquariumScript>().RemoveFishFromDating(gameObject);

                    //play animation when we start breeding
                    AnimHandler.GetComponent<FishAnimation>().ResetAllVariables(false);
                    AnimHandler.GetComponent<FishAnimation>().Mating(true);
                    float animationTime = AnimHandler.GetComponent<FishAnimation>().RuntimeParring;

                    //Make sure we stay in animation, for its duration
                    StartCoroutine(WaitSecondsThenReset(animationTime*2));
                    
                    //Spread the sperm
                    MyWaifu.GetComponent<FishBehaviour>().MyHusbandusBabychance = Random.Range(MinBabyChance,MaxBabyChance);

                    //Reset stuff
                    StartCoroutine(BreedCooldown(breedCooldown));
                    areWeHavingSex = false;
                    weAreMating = false;
                    MyWaifu = null;


                } else { 
                    float distanceToPartner = Vector2.Distance(thisTransform.position,MyWaifu.transform.position);
                    if (distanceToPartner <= (thisTransform.localScale.x))
                            areWeHavingSex=true;
                        else
                            MoveTowardsObject(MyWaifu);
                    }
            }
        }
	}

    void StartIdlePhase() {
        AnimHandler.GetComponent<FishAnimation>().ResetAllVariables(false);
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
                areWePlayingAnimation = true;
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

    void RandomMovement(){
        if (Time.time >= tChange && !areWePlayingAnimation)
        {
            randomX = Random.Range(-2.0f,2.0f); // with float parameters, a random float
            randomY = Random.Range(-2.0f,2.0f); //  between -2.0 and 2.0 is returned

            if (facingRight && randomX < 0f)
            {
                StartCoroutine(TurnFish(true,false));
            }
            else if (!facingRight && randomX > 0f)
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
            if (facingRight)
            {
                randomX = -randomX;
                StartCoroutine(TurnFish(true,true));
            }

            if (!facingRight)
            {
                randomX = -randomX;
                StartCoroutine(TurnFish(false,true));
            }
        }
        

        if (transform.position.y >= maxY || transform.position.y <= minY){
           randomY = -randomY;
        }
        

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
    }

    void MoveTowardsObject(GameObject WhatToFollow) {
        float facingCheck = WhatToFollow.transform.position.x - thisTransform.position.x;

        if (!facingRight && facingCheck > 0f) {
            if (!areWePlayingAnimation)
                StartCoroutine(TurnFish(false, true));
     
        }
        if (facingRight && facingCheck < 0f)
        {
            if (!areWePlayingAnimation)
                StartCoroutine(TurnFish(true, true));
        }


        Vector3 dir = WhatToFollow.transform.position - thisTransform.position;
        dir.z = thisTransform.position.z;
        dir.Normalize();

        transform.Translate(dir * (SwimSpeed*2) * Time.deltaTime);

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
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
        OurPickupParticle.active = true;
    }

    public void setBabyPhase(float mintuesIShouldGrow) {
        StartCoroutine(GrowingUp(mintuesIShouldGrow));
    }

    private IEnumerator TurnFish(bool left,bool isItFastTurn)
    {
        areWePlayingAnimation = true;
        float runtime = AnimHandler.GetComponent<FishAnimation>().RuntimeTurnL;
        float preSpeed = SwimSpeed;

        if (left)
            AnimHandler.GetComponent<FishAnimation>().Turn(true);
        else
            AnimHandler.GetComponent<FishAnimation>().Turn(false);
        phaseClock = phaseClock + runtime;

        if (!isItFastTurn)
        {
            SwimSpeed = 0;
            yield return new WaitForSeconds(runtime);
            SwimSpeed = preSpeed;
            areWePlayingAnimation = false;
        }
        else {
            SwimSpeed = SwimSpeed / 2;
            yield return new WaitForSeconds(runtime/2);
            SwimSpeed = preSpeed;
            areWePlayingAnimation = false;
        }

        facingRight = !facingRight;

        
    }

    private IEnumerator WaitSecondsThenReset(float secondsToWaits) {
        yield return new WaitForSeconds(secondsToWaits);
        ResetPhases();
        PickAPhase();
    }

    private IEnumerator EatTheFood()
    {
        float animationTime = AnimHandler.GetComponent<FishAnimation>().RuntimeEating;
        float preSpeed = SwimSpeed;

        SwimSpeed = 0;
        ResetPhases();
        phaseClock = Time.time + animationTime;

        if (!areWePlayingAnimation)
        {
            AnimHandler.GetComponent<FishAnimation>().Eating(true);
            areWePlayingAnimation = true;
        }
        else
        {
            AnimHandler.GetComponent<FishAnimation>().ResetAllVariables(false);
        }
        playSound(SoundEating, false);

        yield return new WaitForSeconds(animationTime);

        SwimSpeed = preSpeed;
        areWePlayingAnimation = false;
    }

    private IEnumerator BreedCooldown(float secondsToWait) {
        canWeBreed = false;
        yield return new WaitForSeconds(secondsToWait);
        canWeBreed = true;
    }

    private IEnumerator GrowingUp(float minutesToGrow)
    {
        weAreMating = false;
        MyHusbandu = null;
        MyWaifu = null;
        areWeABaby = true;
        yield return new WaitForSeconds(minutesToGrow * 60);
        areWeABaby = false;
        thisTransform.localScale = new Vector3(thisTransform.localScale.x * 1.2f, thisTransform.localScale.z * 1.2f, thisTransform.localScale.y * 1.2f);
    }

    public void StartEatingNow(){
        StartCoroutine(EatTheFood());
    }

    private float RandomizePhasetime(float minValue, float maxValue){
        float returnValue = Random.Range(minValue, maxValue);
        return returnValue;
    }

    void ResetPhases(){
        if (areWePlayingAnimation) {
            areWePlayingAnimation = false;
        }

        AnimHandler.GetComponent<FishAnimation>().ResetAllVariables(true);
        if(ShouldWeLookForMate)
            ourAquarium.GetComponent<AquariumScript>().RemoveFishFromDating(gameObject);
        ShouldWeLookForMate = false;
        ShouldWeIdle = false;
        ShouldWeMove = false;

    }

    void PickAPhase() {
        if (canWeBreed && !areWeABaby)
        {
            int TotalPhaseAmount = ChanceForBreedPhase + ChanceForIdlePhase + ChanceForMovePhase;
            int phaseValue = Random.Range(0, TotalPhaseAmount);

            if (phaseValue > 0 && phaseValue < ChanceForBreedPhase)
                StartBreedPhase();
            else if (phaseValue > ChanceForBreedPhase && phaseValue < (ChanceForBreedPhase + ChanceForIdlePhase))
                StartIdlePhase();
            else if (phaseValue > (ChanceForBreedPhase + ChanceForIdlePhase))
                StartMovePhase();
        }
        else if(!canWeBreed || areWeABaby) {
            int TotalPhaseAmount = ChanceForIdlePhase + ChanceForMovePhase;
            int phaseValue = Random.Range(0, TotalPhaseAmount);

            if (phaseValue > 0 && phaseValue < ChanceForIdlePhase)
                StartIdlePhase();
            else if (phaseValue > ChanceForIdlePhase)
                StartMovePhase();
        }
        didWeReset = false;
    }

    private void playSound(AudioClip audioToPlay,bool loop){
        if (ourAquarium == GameHandler.GetComponent<GameHandler>().ActiveAquarium || ourAquarium == null) {
            SFXHandler.audio.clip = audioToPlay;
            if (loop)
                SFXHandler.audio.loop = true;
            SFXHandler.audio.Play();
        }
    }

    private void stopCurSound() {
        SFXHandler.audio.Stop();
        SFXHandler.audio.clip = null;
        SFXHandler.audio.loop = false;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Aquarium" && !areWeInAqarium)
        {
            stopCurSound();
            ourAquarium = other.gameObject;

            float curOffsetX = (OffsetX + (transform.localScale.x / 2f));
            float curOffsetY =(OffsetY + (transform.localScale.y / 2f));

            minX = (other.transform.position.x - (other.transform.localScale.x / 2) + curOffsetX);
            maxX = (other.transform.position.x + (other.transform.localScale.x / 2) - curOffsetX);
            minY = (other.transform.position.y - (other.transform.localScale.y / 2) + curOffsetY);
            maxY = (other.transform.position.y + (other.transform.localScale.y / 2) - curOffsetY);
            areWeInAqarium = true;

            other.gameObject.GetComponent<AquariumScript>().AddFishToList(gameObject);

            AnimHandler.GetComponent<FishAnimation>().ResetAllVariables(true);
            playSound(SoundPlace, false);
            OurPickupParticle.active = false;
        }

    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Aquarium" && !areWeInAqarium)
        {
            if (MyHusbandu != null) {
                MyHusbandu.GetComponent<FishBehaviour>().MyWaifu = null;
                MyHusbandu.GetComponent<FishBehaviour>().weAreMating = false;
                MyHusbandu.GetComponent<FishBehaviour>().areWeHavingSex = false;
                ourAquarium.GetComponent<AquariumScript>().RemoveFishFromDating(MyHusbandu);
                MyHusbandu = null;
                areWeHavingSex = false;
                weAreMating = false;
            }
            if (MyWaifu != null) {
                ourAquarium.GetComponent<AquariumScript>().RemoveFishFromDating(gameObject);
                MyWaifu.GetComponent<FishBehaviour>().MyHusbandu = null;
                MyWaifu.GetComponent<FishBehaviour>().weAreMating = false;
                MyWaifu.GetComponent<FishBehaviour>().areWeHavingSex = false;
                MyWaifu = null;
                areWeHavingSex = false;
                weAreMating = false;
            }

            ourAquarium = null;
            areWeInAqarium = false;
            other.gameObject.GetComponent<AquariumScript>().RemoveFishFromList(gameObject);
            playSound(SoundSplashing, true);
        }
    }
          
    }