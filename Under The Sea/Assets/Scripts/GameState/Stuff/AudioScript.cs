using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {

    public AudioClip BGTrack;
    public float fadeInTo;
    public float fadeInIncrement;

    private float curVolume;

	// Use this for initialization
	void Start () {
        audio.clip = BGTrack;
        audio.Play();
        curVolume = audio.volume;
	}
	
	// Update is called once per frame
	void Update () {
	    if(curVolume < fadeInTo){
            StartCoroutine(fadeIn(0.5f));
        }
	}

    private IEnumerator fadeIn(float secondsTowait)
    {
        
        yield return new WaitForSeconds(secondsTowait);
        curVolume += fadeInIncrement;
        audio.volume = curVolume;
    }
}
