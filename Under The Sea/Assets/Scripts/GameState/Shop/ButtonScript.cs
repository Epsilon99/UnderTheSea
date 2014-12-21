using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public int Cost;
    public int ItemID;
    public Material ButtonNone;
    public Material ButtonHover;
    public Material ButtonBuy;
    public Material SpeechHover;
    public Material SpeeclNoMoney;
    public AudioClip PressSound;
    public AudioClip BuzzSound;

    public GameObject PriceTag;

    private GameObject GUISFX;
    private GameObject Player;
    private GameObject SpeechBubbleArea;
    private float lightUpTime = 0.4f;
    private bool hover;

	// Use this for initialization
	void Start () {
        PriceTag.GetComponent<TextMesh>().text = Cost + "$";
        GUISFX = GameObject.FindGameObjectWithTag("GUISFX");
        SpeechBubbleArea = GameObject.FindGameObjectWithTag("SpeechBubleArea");
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && hover && Player.GetComponent<PlayerStats>().Currency >= Cost && ItemID != 0)
        {
            StartCoroutine(BuyThis());
        }

        else if (Input.GetMouseButtonDown(0) && hover && Player.GetComponent<PlayerStats>().Currency < Cost && ItemID != 0)
        {
            GUISFX.audio.clip = BuzzSound;
            GUISFX.audio.Play();
            StartCoroutine(WaitSecondsThenPlayNegativeSound(0.4f));
            SpeechBubbleArea.renderer.material = SpeeclNoMoney;
            SpeechBubbleArea.GetComponent<SpeechBubbleScript>().SetBackToBlank(10f);
        }

        else if (Input.GetMouseButtonDown(0) && hover && ItemID == 0) {
            GUISFX.audio.clip = BuzzSound;
            GUISFX.audio.Play();
            StartCoroutine(WaitSecondsThenPlayNegativeSound(0.4f));
            SpeechBubbleArea.renderer.material = SpeechHover;
            SpeechBubbleArea.GetComponent<SpeechBubbleScript>().SetBackToBlank(10f);
        }
	}

    void OnMouseEnter() {
        if (ItemID != 0)
        {
            if (Cost != 0)
                PriceTag.active = true;
            renderer.material = ButtonHover;
            if (SpeechHover != null)
                SpeechBubbleArea.renderer.material = SpeechHover;
        }

        hover = true;
    }

    void OnMouseExit(){
        if(PriceTag.active == true)
            PriceTag.active = false;
        renderer.material = ButtonNone;
        hover = false;

        SpeechBubbleArea.GetComponent<SpeechBubbleScript>().SetBackToBlank(0f);
    }


    private IEnumerator BuyThis() {
        GUISFX.audio.clip = PressSound;
        GUISFX.audio.Play();
        renderer.material = ButtonBuy;

        Player.GetComponent<PlayerStats>().Currency -= Cost;
        Player.GetComponent<PlayerStats>().AddItemToStash(ItemID);
        yield return new WaitForSeconds(lightUpTime);
        SpeechBubbleArea.GetComponent<SpeechBubbleScript>().PlayPositiveSound();
        if(hover)
            renderer.material = ButtonHover;
        else
            renderer.material = ButtonNone;
    }

    private IEnumerator WaitSecondsThenPlayNegativeSound(float AmounOfSeconds) {
        yield return new WaitForSeconds(AmounOfSeconds);
        SpeechBubbleArea.GetComponent<SpeechBubbleScript>().PlayNegativeSound();
    }
}
