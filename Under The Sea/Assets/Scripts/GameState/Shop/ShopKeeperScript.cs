using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopKeeperScript : MonoBehaviour {

    public Material HandsUpOpenMouth;
    public Material HandsUpClosedMouth;
    public Material HandsDownOpenMouth;
    public Material HandsDownClosedMouth;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayLongArmsUp() {
        StartCoroutine(LongArmsUp());
    }

    public void PlayShortArmsUp() {
        StartCoroutine(ShortArmsUp());
    }

    public void PlayLongArmsDown()
    {
        StartCoroutine(LongArmsDown());
    }

    public void PlayShortArmsDown()
    {
        StartCoroutine(ShortArmsDown());
    }

    private IEnumerator LongArmsUp() {
        renderer.material = HandsUpClosedMouth;
        yield return new WaitForSeconds(0.2f);
        renderer.material = HandsUpOpenMouth;
        yield return new WaitForSeconds(0.4f);
        renderer.material = HandsUpClosedMouth;
        yield return new WaitForSeconds(0.2f);
        renderer.material = HandsUpOpenMouth;
        yield return new WaitForSeconds(0.4f);
        renderer.material = HandsDownClosedMouth;
    }

    private IEnumerator ShortArmsUp()
    {
        renderer.material = HandsUpClosedMouth;
        yield return new WaitForSeconds(0.2f);
        renderer.material = HandsUpOpenMouth;
        yield return new WaitForSeconds(0.4f);
        renderer.material = HandsUpClosedMouth;
        yield return new WaitForSeconds(0.2f);
        renderer.material = HandsDownClosedMouth;
    }

    private IEnumerator LongArmsDown()
    {
        renderer.material = HandsDownOpenMouth;
        yield return new WaitForSeconds(0.4f);
        renderer.material = HandsDownClosedMouth;
        yield return new WaitForSeconds(0.2f);
        renderer.material = HandsDownOpenMouth;
        yield return new WaitForSeconds(0.4f);
        renderer.material = HandsDownClosedMouth;
    }

    private IEnumerator ShortArmsDown()
    {
        renderer.material = HandsDownOpenMouth;
        yield return new WaitForSeconds(0.4f);
        renderer.material = HandsDownClosedMouth;
    }
}
