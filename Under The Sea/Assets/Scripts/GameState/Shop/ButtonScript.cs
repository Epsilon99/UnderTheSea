using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public Material ButtonNone;
    public Material ButtonHover;
    public Material ButtonBuy;

    public AudioClip PressSound;

    private float lightUpTime = 0.4f;
    private GameObject GUISFX;

    private bool hover;

	// Use this for initialization
	void Start () {
        GUISFX = GameObject.FindGameObjectWithTag("GUISFX");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && hover)
        {
            StartCoroutine(BuyThis());
        }
	}

    void OnMouseEnter() {
        renderer.material = ButtonHover;
        hover = true;
    }

    void OnMouseExit(){
        renderer.material = ButtonNone;
        hover = false;
    }


    private IEnumerator BuyThis() {
        GUISFX.audio.clip = PressSound;
        GUISFX.audio.Play();
        renderer.material = ButtonBuy;
        yield return new WaitForSeconds(lightUpTime);
        if(hover)
            renderer.material = ButtonHover;
        else
            renderer.material = ButtonNone;
    }
}
