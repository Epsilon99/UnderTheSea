using UnityEngine;
using System.Collections;

public class MuteMusicButton : MonoBehaviour {

    public GameObject MusicHandler;

    public Material Muted;
    public Material UnMuted;

    private bool Hover;
    private bool AreWePlaying = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && Hover) {
            if (AreWePlaying)
            {
                renderer.material = Muted;
                MusicHandler.audio.Stop();
                AreWePlaying = false;
            }
            else {
                renderer.material = UnMuted;
                MusicHandler.audio.Play();
                AreWePlaying = true;
            }
        }


	}

    void OnMouseEnter() {
        Hover = true;
    }

    void OnMouseExit()
    {
        Hover = false;
    }
}
