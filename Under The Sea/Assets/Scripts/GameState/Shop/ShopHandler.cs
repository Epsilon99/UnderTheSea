using UnityEngine;
using System.Collections;

public class ShopHandler : MonoBehaviour {
    public GameObject ShopScreen;
    public GameObject ClosePos;
    public GameObject OpenPos;
    public GameObject ArrowLeft;
    public GameObject ArrowRight;

    public float speed;

    public bool areWeInShop = false;

    private bool areWeInPosition = true;
    public bool disabledLeftArrow;
    public bool disabledRightArrow;

    private GameObject destinationGO;



    // Use this for initialization
    void Start()
    {

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
            DisableArrows();
            destinationGO = OpenPos;
            areWeInPosition = false;
        }
        else if (areWeInShop)
        {
            EnableArrows();
            destinationGO = ClosePos;
            areWeInPosition = false;
        }
    }

    private void DisableArrows()
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
    }

    private void EnableArrows()
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

    }
}
