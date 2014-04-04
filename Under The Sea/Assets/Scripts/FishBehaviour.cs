using UnityEngine;
using System.Collections;

public class FishBehaviour : MonoBehaviour {


    //For calculating different chances for the phases
    public int ChanceForBreedPhase = 10;
    public int ChanceForIdlePhase = 20;
    public int ChanceForMovePhase = 30;


    //How fast we can swim
    public float SwimSpeed;

    //Public variables to control time of different phases
    public float MaxIdlePhase;
    public float MinIdlePhase;
    public float MaxMovePhase;
    public float MinMovePhase;
    public float MaxBreedPhase;
    public float MinBreedPhase;

    //Floats regarding movementrestriction
    private float maxX = -2.748552f;
    private float minX = -13.95572f;
    private float maxY = 10.226f;
    private float minY = 4.658046f;

    //Timer for phases
    private float phaseClock;

    //Floats regarding movement
    private float tChange = 0; //force new direction in the first update
    private float randomX;
    private float randomY;

    //Bools to keep track of phases
    private bool ShouldWeMove;
    private bool ShouldWeBreed;
    private bool ShouldWeIdle;


    void OnAwake(){
        ResetPhases();
        PickAPhase();
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time < phaseClock){
            if(ShouldWeMove == true){
                Movement();
            }
        }
        else{
            ResetPhases();
            PickAPhase();
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
        if(transform.position.x >= maxX || transform.position.x <= minX)
            randomX = -randomX;

        if(transform.position.y >= maxY || transform.position.y <= minY)
            randomY = -randomY;

        // make sure the position is inside the borders
        //transform.position.x = Mathf.Clamp(transform.position.x, minX, maxX);
        //transform.position.y = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
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
        
        
    }


