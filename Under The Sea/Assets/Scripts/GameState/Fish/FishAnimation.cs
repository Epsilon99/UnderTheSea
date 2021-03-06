﻿using UnityEngine;
using System.Collections;

public class FishAnimation : MonoBehaviour {
    
    private Animator anim;

    public float currentAnimtionSpeed;

    public float CurrentRuntimeEating;
    public float CurrentRuntimeIdle1;
    public float CurrentRuntimeIdle2;
    public float CurrentRuntimeIdle3;
    public float CurrentRuntimeParring;
    public float CurrentRuntimePickup;
    public float CurrentRuntimeSwimming;
    public float CurrentRuntimeTurnL;
    public float CurrentRuntimeTurnR;

    public float RuntimeEating;
    public float RuntimeIdle1;
    public float RuntimeIdle2;
    public float RuntimeIdle3;
    public float RuntimeParring;
    public float RuntimePickup;
    public float RuntimeSwimming;
    public float RuntimeTurnL;
    public float RuntimeTurnR;



    public GameObject MyParrent;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        currentAnimtionSpeed = anim.speed;
        SetAllAnimationSpeeds();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetAllAnimationSpeeds() {
        CurrentRuntimeEating = RuntimeEating * currentAnimtionSpeed;
        CurrentRuntimeIdle1 = RuntimeIdle1 * currentAnimtionSpeed;
        CurrentRuntimeIdle2 = RuntimeIdle2 * currentAnimtionSpeed;
        CurrentRuntimeIdle3 = RuntimeIdle3 * currentAnimtionSpeed;
        CurrentRuntimeParring = RuntimeParring * currentAnimtionSpeed;
        CurrentRuntimePickup = RuntimePickup * currentAnimtionSpeed;
        CurrentRuntimeSwimming = RuntimeSwimming * currentAnimtionSpeed;
        CurrentRuntimeTurnL = RuntimeTurnL * currentAnimtionSpeed;
        CurrentRuntimeTurnR = RuntimeTurnR * currentAnimtionSpeed;
    }

    public void Swimming(bool start) {
        if (start)
        {
            anim.SetBool("Swimming", true);
        }
        else
            anim.SetBool("Swimming", false);
    }

    public void Eating(bool start){
        if (start)
            anim.SetBool("Eating", true);
        
        else
            anim.SetBool("Eating", false);
    }

    public void Mating(bool start)
    {
        if (start)
        {
            anim.SetBool("Parring", true);
        }
        else
            anim.SetBool("Parring", false);
    }

    public void PickedUp(bool start)
    {
        if (start)
        {
            anim.SetBool("PickedUp", true);
        }
        else
            anim.SetBool("PickedUp", false);
    }

    public void Turn(bool left)
    {
        
        if (left) {
            anim.SetBool("TurningLeft",true);
            StartCoroutine(RotateMe(Vector3.up * (transform.localEulerAngles.y - (-90)) * -1, CurrentRuntimeTurnL));
        }
        if (!left) {
            anim.SetBool("TurningRight", true);
            StartCoroutine(RotateMe(Vector3.up * (transform.localEulerAngles.y - 90) * -1, CurrentRuntimeTurnR));
        }
    }

    public void Idle(int whichIdle) {
        switch (whichIdle)
        {
            case 0:
                anim.SetInteger("Idle", 0);
                break;

            case 1:
                anim.SetInteger("Idle", 1);
                break;

            case 2:
                anim.SetInteger("Idle", 2);
                break;

            case 3:
                anim.SetInteger("Idle", 3);
                break;
        }
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        //MyParrent.GetComponent<BoxCollider>().enabled = false;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
        ResetAllVariables(true);
        yield return new WaitForSeconds(CurrentRuntimeTurnL / 3);
        //MyParrent.GetComponent<BoxCollider>().enabled = true;
    }

    public void ResetAllVariables(bool exceptSwimmin) {

        if(exceptSwimmin){
            Swimming(true);
            Eating(false);
            Mating(false);
            PickedUp(false);
            anim.SetBool("TurningRight", false);
            anim.SetBool("TurningLeft", false);
            Idle(0);
        }else{
            Swimming(false);
            Eating(false);
            Mating(false);
            PickedUp(false);
            anim.SetBool("TurningRight", false);
            anim.SetBool("TurningLeft", false);
            Idle(0);
        }
        
    }
}
