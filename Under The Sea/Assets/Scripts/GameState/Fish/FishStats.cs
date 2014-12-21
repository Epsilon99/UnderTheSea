using UnityEngine;
using System.Collections;

public class FishStats : MonoBehaviour {

    //Basic stats, that nameplate needs access too
    public string Name;
    public string type;
    public int currentAge;

    //Things to set for each Race
    public GameObject BabyFish;
    public float RaceSwimSpeed;
    public int RaceAge;
    public int RaceBreedCoolDown;
    public int RaceBreedChance;
    public int RaceIdleChance;
    public int RaceChanceForMove;
    public int RaceBabyChance;
    public int RaceFoodDecay;
    public float RaceStomachSize;
    public float RaceHappinessDecayAmount;
    public float RaceHungerLimit;
    public int RaceSellPrice;

    //Happiness and stomach, are determening values, that changes from the players interaction
    public float CurHappiness;
    public float CurStomach;
    public int HappinessLevel;

    //These are standard modifiers for all fish
    private float FishYearInSeconds = 120;
    private float FoodDecayInSeconds = 60;
    private float HappinessDecayInSeconds = 30;

    //Things that are calculated
    private int DeathYear;
    private float currentSwimSpeed;
    private float currentColor;
    private float currentAnimationSpeed;

    //Other stuff
    public GameObject GraphicHandler;
    public Material ThisFishMaterial;
    private GameObject GameHandlerGO;
    private float maxHappiness = 100;
    private int TotalHappinessSteps = 10;
    private float sizeOfDecrement;
    private float nextIncrement;
    private float nextDecrement;
    private float standardMinModifer = 0.8f;
    private float standardMaxModifer = 1.2f;
    private float CurrentTextureMix;
    private bool areWeHungering;
    private bool didWeStartFoodTimer = false;
    


    void OnEnable() {
        SetStartStats();
        SetAllBasicStats();
        GameHandlerGO = GameObject.FindGameObjectWithTag("GameHandler");
        setNameForFish(GameHandlerGO.GetComponent<FishNamerScript>().NameMe());
    }


	// Use this for initialization
	void Start () {
        StartCoroutine(AgeFunction());
        StartCoroutine(HappinessDecay());
	}
	
	// Update is called once per frame
	void Update () {
        if (CurHappiness < nextDecrement)
        {
            HappinessDecrement();
            nextDecrement -= sizeOfDecrement;
            nextIncrement -= sizeOfDecrement;
        }

        if (CurHappiness > nextIncrement)
        {
            HappinessIncrement();
            nextDecrement += sizeOfDecrement;
            nextIncrement += sizeOfDecrement;
        }

        if (CurStomach < RaceHungerLimit) {
            areWeHungering = true;
        }

        if(areWeHungering && CurStomach>RaceHungerLimit){
            areWeHungering = false;
        }

        if (areWeHungering && !didWeStartFoodTimer) {
            StartCoroutine(FoodDecay());
        }

        float testModifer = ReturnCurrentModifer();

        if (testModifer == 1.2f)
            HappinessLevel = 5;
        else if (testModifer == 1.1f)
            HappinessLevel = 4;
        else if (testModifer == 1f)
            HappinessLevel = 3;
        else if (testModifer == 0.9f)
            HappinessLevel = 2;
        else if (testModifer == 0.8f)
            HappinessLevel = 1;

	}

    void setFishMaterial(float amount) {
        CurrentTextureMix += amount;
        GraphicHandler.renderer.material = ThisFishMaterial;
        GraphicHandler.renderer.material.SetFloat("_MixValue", CurrentTextureMix);
    }

    void HappinessDecrement() {
        setFishMaterial(1f / (TotalHappinessSteps*2));
        SetAllBasicStats();
    }

    void HappinessIncrement() {
        setFishMaterial(-(1f / (TotalHappinessSteps * 2)));
        SetAllBasicStats();
    }

    void SetAllBasicStats() {
        gameObject.GetComponent<FishBehaviour>().breedCooldown = RaceBreedCoolDown * ReturnCurrentModifer();
        gameObject.GetComponent<FishBehaviour>().ChanceForBreedPhase = (int)(RaceBreedChance * ReturnCurrentModifer());
        gameObject.GetComponent<FishBehaviour>().MinBabyChance = (int)((RaceBabyChance * standardMinModifer) * ReturnCurrentModifer());
        gameObject.GetComponent<FishBehaviour>().MaxBabyChance = (int)((RaceBabyChance * standardMaxModifer) * ReturnCurrentModifer());
        gameObject.GetComponent<FishBehaviour>().SwimSpeed = RaceSwimSpeed * ReturnCurrentModifer();
    }

    void SetStartStats() {
        CurHappiness = maxHappiness;
        CurStomach = RaceStomachSize;
        sizeOfDecrement = maxHappiness / TotalHappinessSteps;
        nextDecrement = CurHappiness - sizeOfDecrement;
        nextIncrement = CurHappiness + sizeOfDecrement;
        setFishMaterial(0f);
        CurrentTextureMix = 0f;
        gameObject.GetComponent<FishBehaviour>().Babyfish = BabyFish;
        gameObject.GetComponent<FishBehaviour>().ChanceForIdlePhase = RaceIdleChance;
        gameObject.GetComponent<FishBehaviour>().ChanceForMovePhase = RaceChanceForMove;
        gameObject.GetComponent<FishBehaviour>().ChanceForBreedPhase = (int)(RaceBreedChance * ReturnCurrentModifer());
        setFishDeathAge();
    }

    float ReturnCurrentModifer() {
        if (CurHappiness == 100)
        {
            return (1.2f);
        }
        else if (CurHappiness < 100 && CurHappiness >= 90)
        {
            return (1.1f);
        }
        else if (CurHappiness < 90 && CurHappiness >= 60)
        {
            return (1f);
        }
        else if (CurHappiness < 60 && CurHappiness >= 40)
        {
            return (1f);
        }
        else if (CurHappiness < 40 && CurHappiness >= 20)
        {
            return (0.9f);
        }
        else if (CurHappiness < 20 && CurHappiness >= 0)
        {
            return (0.8f);
        }
        else
            return(0f);
        
    }

    void setNameForFish(string NameOfTheFish) {
        Name = NameOfTheFish;
    }

    void setFishDeathAge() {
        DeathYear = Random.Range((RaceAge - 5), (RaceAge + 6));
    }

    void deathByAge() {
        Destroy(gameObject);
    }

    public int GetCurrentSellPrice() { 
        int curSellPrice = (int)(RaceSellPrice * ReturnCurrentModifer());
        return curSellPrice;
    }

    private IEnumerator AgeFunction()
    {
        if (currentAge + 1 < DeathYear)
        {
            yield return new WaitForSeconds(FishYearInSeconds);
            currentAge += 1;
            StartCoroutine(AgeFunction());
        }

        else if (currentAge + 1 >= DeathYear)
        {
            deathByAge();
        }
    }

    private IEnumerator FoodDecay() {
        didWeStartFoodTimer = true;
        yield return new WaitForSeconds(FoodDecayInSeconds);
        if (CurStomach < RaceStomachSize) { 
            if(areWeHungering)
                CurStomach -= RaceFoodDecay;
        }
        didWeStartFoodTimer = false;
    }

    private IEnumerator HappinessDecay() {
        yield return new WaitForSeconds(HappinessDecayInSeconds);
        ChangeCurrentHappiness(-RaceHappinessDecayAmount);
        StartCoroutine(HappinessDecay());
    }

    public void ChangeCurrentHappiness(float amount) {

        CurHappiness += amount;

        if(CurHappiness > maxHappiness)
            CurHappiness = maxHappiness;

        if (CurHappiness < 0)
            CurHappiness = 0;
    }

    public void ChangeCurrentStomach(float amount) {
        CurStomach += amount;

        if (CurStomach > RaceStomachSize)
            CurStomach = RaceStomachSize;

        if (CurStomach < 0)
            CurStomach = 0;
    }
}
