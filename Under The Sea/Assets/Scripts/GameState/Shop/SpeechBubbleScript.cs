using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleScript : MonoBehaviour {

    public AudioClip AudioNo1, AudioNo2, AudioNo3, AudioNo4, AudioNo5, AudioNo6, AudioYes1, AudioYes2, AudioYes3, AudioYes4, AudioYes5, AudioYes6;

    public GameObject ShopKeeper;
    public Material Blank;
    private GameObject GUISFX;

	// Use this for initialization
	void Start () {
        GUISFX = GameObject.FindGameObjectWithTag("GUISFX");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayPositiveSound() {
        int ChosenFile = Random.Range(1, 7);

        switch (ChosenFile) { 
            case(1):
                GUISFX.audio.clip = AudioYes1;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayLongArmsUp();
                break;

            case(2):
                GUISFX.audio.clip = AudioYes2;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayLongArmsUp();
                break;

            case(3):
                GUISFX.audio.clip = AudioYes3;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayLongArmsUp();
                break;

            case(4):
                GUISFX.audio.clip = AudioYes4;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayLongArmsUp();
                break;

            case(5):
                GUISFX.audio.clip = AudioYes5;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayLongArmsUp();
                break;

            case(6):
                GUISFX.audio.clip = AudioYes6;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayLongArmsUp();
                break;
        }
    }

    public void PlayNegativeSound()
    {
        int ChosenFile = Random.Range(1, 7);

        switch (ChosenFile)
        {
            case (1):
                GUISFX.audio.clip = AudioNo1;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayLongArmsDown();
                break;

            case (2):
                GUISFX.audio.clip = AudioNo2;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayLongArmsDown();
                break;

            case (3):
                GUISFX.audio.clip = AudioNo3;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayLongArmsDown();
                break;

            case (4):
                GUISFX.audio.clip = AudioNo4;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayShortArmsDown();
                break;

            case (5):
                GUISFX.audio.clip = AudioNo5;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayShortArmsDown();
                break;

            case (6):
                GUISFX.audio.clip = AudioNo6;
                GUISFX.audio.Play();
                ShopKeeper.GetComponent<ShopKeeperScript>().PlayShortArmsDown();
                break;
        }
    }

    public void SetBackToBlank(float afterTimer) {
        StartCoroutine(setBlankAfterSeconds(afterTimer));
    }

    private IEnumerator setBlankAfterSeconds(float amountOfSeconds) {
        yield return new WaitForSeconds(amountOfSeconds);
        renderer.material = Blank;
    }
}
