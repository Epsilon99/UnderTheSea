using UnityEngine;
using System.Collections;

public class SellButtonScript : MonoBehaviour {

    public Material ButtonNone;
    public Material ButtonHover;
    public Material ButtonBuy;
    public AudioClip PressSound;

    private GameObject GUISFX;
    private GameObject Player;
    public GameObject SelectPlateGO;
    public GameObject GUIHandler;
    private float lightUpTime = 0.4f;
    private bool hover;

    // Use this for initialization
    void Start()
    {
        
        GUISFX = GameObject.FindGameObjectWithTag("GUISFX");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hover)
        {
            StartCoroutine(SellThis());
        }

    }

    void OnMouseEnter()
    {
        renderer.material = ButtonHover;
        hover = true;
    }

    void OnMouseExit()
    {
        renderer.material = ButtonNone;
        hover = false;
    }


    private IEnumerator SellThis()
    {
        GUISFX.audio.clip = PressSound;
        GUISFX.audio.Play();
        renderer.material = ButtonBuy;

        GameObject FishToSell = SelectPlateGO.GetComponent<SelectPlateScript>().FishWeHaveSelected;
        GUIHandler.GetComponent<GUIScript>().UnselectFish();
        Player.GetComponent<PlayerStats>().Currency += FishToSell.GetComponent<FishStats>().GetCurrentSellPrice();
        hover = false;
        DestroyObject(FishToSell);

        yield return new WaitForSeconds(lightUpTime);
        if (hover)
            renderer.material = ButtonHover;
        else
            renderer.material = ButtonNone;
    }
}
