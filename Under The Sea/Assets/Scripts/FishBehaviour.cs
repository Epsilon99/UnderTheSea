using UnityEngine;
using System.Collections;

public class FishBehaviour : MonoBehaviour {

    public float MaxIdlePhase;
    public float MinIdlePhase;
    public float MaxMovePhase;
    public float MinMovePhase;
    public float MaxBreedPhase;
    public float MinBreedPhase;

    private float eventClock;

    void OnAwake() {
        
    
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void StartIdlePhase() { }

    void StartBreedPhase() { }

    void StartMovePhase() { }

    private float RandomizePhasetime(float minValue, float maxValue){
        float returnValue = Random.Range(minValue, maxValue);
        return returnValue;
    }

    void PickAPhase() {
        int phaseValue = Random.Range(1, 3);

        switch (phaseValue)
        {
            case 1:
                StartIdlePhase();
                break;
            case 2:
                StartBreedPhase();
                break;
            case 3:
                StartMovePhase();
                break;
        }
    }

}
