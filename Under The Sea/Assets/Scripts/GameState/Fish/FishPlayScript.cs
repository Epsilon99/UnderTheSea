using UnityEngine;
using System.Collections;

public class FishPlayScript : MonoBehaviour {

    public GameObject CursorHandler;
    public Texture2D PlayCursor1;
    public Texture2D PlayCursor2;
    public Texture2D PlayCursor3;
    public Texture2D PlayCursor4;
    private Texture2D previousCursor;

    public float secondsBetweenFrames;

    private GameObject PlayerStatGO;

    public float timeBeforePlay;

    public float hoverTime;
    public int moneys;
    private float timer;
    private int MaxMoneyCount = 5;
    private int MoneyTimesCounter;
    private float MoneyRefreshTimer = 60;

    private bool wasCounterSet = false;
    private bool areWePlayingAnimation = false;
    private bool shouldWePlayAnimation = false;
    private bool getMoney = false;

    void OnEnable() {
        areWePlayingAnimation = false;
        shouldWePlayAnimation = false;
        PlayerStatGO = GameObject.FindGameObjectWithTag("Player");
        CursorHandler = GameObject.FindGameObjectWithTag("GUIHandler");
    }

	// Use this for initialization
	void Start () {
        MoneyTimesCounter = MaxMoneyCount;
	}
	
	// Update is called once per frame
	void Update () {
        if (!shouldWePlayAnimation && timer <= Time.time && wasCounterSet == true)
        {
            getMoney = true;
            timer = Time.time + hoverTime;
            shouldWePlayAnimation = true;
        }

        if (getMoney && timer <= Time.time && gameObject.GetComponent<FishBehaviour>().areWeInAqarium != false)
        {
            if (MoneyTimesCounter > 0)
            {
                PlayerStatGO.GetComponent<PlayerStats>().Currency += moneys;
                MoneyTimesCounter -= 1;
            }
            gameObject.GetComponent<FishStats>().ChangeCurrentHappiness(Random.RandomRange(0,0.3f));
            timer = Time.time + hoverTime;
        }

        if (shouldWePlayAnimation && !areWePlayingAnimation && gameObject.GetComponent<FishBehaviour>().areWeInAqarium != false)
        {  
            StartCoroutine(PlayAnimation());
        }

	}

    void OnMouseEnter() {
        if (!wasCounterSet)
        {
            previousCursor = CursorHandler.GetComponent<CursorScript>().currentCursor;
            timer = Time.time + timeBeforePlay;
            wasCounterSet = true;
        }
        
    }

    void OnMouseExit() {
        getMoney = false;
        CursorHandler.GetComponent<CursorScript>().ChangeTheCurrentCursor(previousCursor);
        shouldWePlayAnimation = false;
        areWePlayingAnimation = false;
        wasCounterSet = false;

        if (MoneyTimesCounter < MaxMoneyCount)
            StartCoroutine(RefreshOurMoney());
    }

    private IEnumerator RefreshOurMoney() {
        yield return new WaitForSeconds(MoneyRefreshTimer);
        MoneyTimesCounter += 1;

        if(MoneyTimesCounter < MaxMoneyCount)
            StartCoroutine(RefreshOurMoney());
    }

    private IEnumerator PlayAnimation() {
        areWePlayingAnimation = true;
        if (shouldWePlayAnimation) { 
        CursorHandler.GetComponent<CursorScript>().ChangeTheCurrentCursor(PlayCursor1);
        yield return new WaitForSeconds(secondsBetweenFrames);
        }

        if (shouldWePlayAnimation)
        {
            CursorHandler.GetComponent<CursorScript>().ChangeTheCurrentCursor(PlayCursor2);
            yield return new WaitForSeconds(secondsBetweenFrames);
        }

        if (shouldWePlayAnimation)
        {
            CursorHandler.GetComponent<CursorScript>().ChangeTheCurrentCursor(PlayCursor3);
            yield return new WaitForSeconds(secondsBetweenFrames);
        }

        if (shouldWePlayAnimation)
        {
            CursorHandler.GetComponent<CursorScript>().ChangeTheCurrentCursor(PlayCursor4);
            yield return new WaitForSeconds(secondsBetweenFrames);
        }

        areWePlayingAnimation = false;
    }

}
