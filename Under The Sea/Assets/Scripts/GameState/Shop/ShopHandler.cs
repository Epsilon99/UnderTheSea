using UnityEngine;
using System.Collections;

public class ShopHandler : MonoBehaviour {
    public AudioClip OpenSound;
    public AudioClip CloseSound;

    public GameObject ShopScreen;
    public GameObject ClosePos;
    public GameObject OpenPos;
    public GameObject ArrowLeft;
    public GameObject ArrowRight;
    public GameObject SelectPlate;

    public float speed;

    public bool areWeInShop = false;

    private bool areWeInPosition = true;
    public bool disabledLeftArrow;
    public bool disabledRightArrow;
    public bool disabledSelectPlate;

    private GameObject destinationGO;
    private GameObject GUISFX;
    private GameObject GUIHandlerGO;


    // Use this for initialization
    void Start()
    {
        GUISFX = GameObject.FindGameObjectWithTag("GUISFX");
        GUIHandlerGO = GameObject.FindGameObjectWithTag("GUIHandler");
    }

    // Update is called once per frame
    void Update()
    {

        if (!areWeInPosition && !areWeInShop)
        {
            Vector3 destination = destinationGO.transform.position;
            Vector3 dir = destination - ShopScreen.transform.position;
            dir.z = 0;
            dir.Normalize();

            if (ShopScreen.transform.position.y > destination.y)
            {
                ShopScreen.transform.Translate(dir * speed * Time.deltaTime);
            }
            else if (ShopScreen.transform.position.y <= destination.y)
            {
                areWeInShop = !areWeInShop;
                areWeInPosition = true;
            }
        }

        if (!areWeInPosition && areWeInShop)
        {
            Vector3 destination = destinationGO.transform.position;
            Vector3 dir = destination - ShopScreen.transform.position;
            dir.z = 0;
            dir.Normalize();

            if (ShopScreen.transform.position.y < destination.y)
            {
                ShopScreen.transform.Translate(dir * speed * Time.deltaTime);
            }
            else if (ShopScreen.transform.position.y >= destination.y)
            {
                areWeInShop = !areWeInShop;
                areWeInPosition = true;
            }
        }
    }

    public void MoveTheShop() {
        if (!areWeInShop)
        {
            DisableObjects();
            GUISFX.audio.clip = OpenSound;
            GUIHandlerGO.GetComponent<QuickShop>().AreQuickShopAvavible = false;
            GUISFX.audio.Play();
            destinationGO = OpenPos;
            areWeInPosition = false;
        }
        else if (areWeInShop)
        {
            EnableObjects();
            GUISFX.audio.clip = CloseSound;
            GUIHandlerGO.GetComponent<QuickShop>().AreQuickShopAvavible = true;
            GUISFX.audio.Play();
            destinationGO = ClosePos;
            areWeInPosition = false;
        }
    }

    private void DisableObjects()
    {
        if (ArrowLeft.active == true)
        {
            ArrowLeft.active = false;
            disabledLeftArrow = true;
        }
        else
        {
            disabledLeftArrow = false;
        }

        if (ArrowRight.active == true)
        {
            ArrowRight.active = false;
            disabledRightArrow = true;
        }
        else
            disabledRightArrow = false;

        if (SelectPlate.active == true)
        {
            SelectPlate.active = false;
            disabledSelectPlate = true;
        }
        else
        {
            disabledLeftArrow = false;
        }
    }

    private void EnableObjects()
    {
        if (disabledLeftArrow)
        {
            ArrowLeft.active = true;
            disabledLeftArrow = false;
        }

        if (disabledRightArrow)
        {
            ArrowRight.active = true;
            disabledRightArrow = false;
        }

        if (disabledSelectPlate) {
            SelectPlate.active = true;
            disabledSelectPlate = false;
        }

    }

}
