using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chick_Animations : MonoBehaviour
{
    public GameObject chick;

    public Animator animate;

    public GameObject spawnButton;

    private bool eatBool = false;
    private bool runBool = false;
    private bool walkBool = false;

    private IEnumerator coroutine;

    //on button click, spawn the chick.



    IEnumerator SpawnChick()
    {
        coroutine = WaitSomeTime(2.0f);
        yield return StartCoroutine(coroutine);

        StartCoroutine(MoveChick(2.0f));
        yield break;
    }

    private IEnumerator WaitSomeTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    public IEnumerator MoveChick(float timeduration)
    {
        //move chick out of the lake
        float distance = Vector3.Distance(transform.localPosition, new Vector3(-200, 375, -150));
        float speed = distance / timeduration;
        while (chick.transform.localPosition.z != -150)
        {
            chick.transform.localPosition = Vector3.MoveTowards(chick.transform.localPosition, new Vector3(-200, 375, -150), speed * Time.deltaTime);
            timeduration -= Time.deltaTime;
            yield return null;
        }

        coroutine = WaitSomeTime(0.5f);
        yield return StartCoroutine(coroutine);

        //rotate chick to face the land
        timeduration = 1.0f;
        chick.transform.Rotate(timeduration * 0, -90, 0);

        coroutine = WaitSomeTime(0.5f);
        yield return StartCoroutine(coroutine);

        
        //make the chick walk to the land
        timeduration = 4.0f;
        distance = Vector3.Distance(transform.localPosition, new Vector3(0, 375, -100));
        speed = distance / timeduration;
        animate.SetBool("Walk", true);
        while (chick.transform.localPosition.z != -100)
        {
            chick.transform.localPosition = Vector3.MoveTowards(chick.transform.localPosition, new Vector3(0, 375, -100), speed * Time.deltaTime);
            timeduration -= Time.deltaTime;
            yield return null;
        }
        animate.SetBool("Walk", false);

        coroutine = WaitSomeTime(0.5f);
        yield return StartCoroutine(coroutine);

        //rotate chick to face player at an angle
        timeduration = 1.0f;
        chick.transform.Rotate(timeduration * 0, 60, 0);


        yield break;
    }

    public void Begin()
    {
        StartCoroutine(SpawnChick());
        spawnButton.SetActive(false);
    }


    //Action Button Functions ----------------------------------------------------------------
    public void EatClick()
    {
        animate.SetBool("Walk", false);
        animate.SetBool("Run", false);
        animate.SetBool("Jump", false);

        runBool = false;
        walkBool = false;

        //button toggle to start and stop eating animation.
        if (eatBool == false)
        {
            animate.SetBool("Eat", true);
            eatBool = true;
        }
        else if (eatBool == true)
        {
            animate.SetBool("Eat", false);
            eatBool = false;
        }
    }

    public void JumpClick()
    {
        animate.SetBool("Walk", false);
        animate.SetBool("Run", false);
        animate.SetBool("Eat", false);
        animate.SetBool("Jump", true);

        eatBool = false;
        runBool = false;
        walkBool = false;
    }

    public void WalkClick()
    {
        animate.SetBool("Eat", false);
        animate.SetBool("Run", false);
        animate.SetBool("Jump", false);

        eatBool = false;
        runBool = false;

        if (walkBool == false)
        {
            animate.SetBool("Walk", true);
            walkBool = true;
        }
        else if (walkBool == true)
        {
            animate.SetBool("Walk", false);
            walkBool = false;
        }

    }

    public void RunClick()
    {
        animate.SetBool("Walk", false);
        animate.SetBool("Eat", false);
        animate.SetBool("Jump", false);

        eatBool = false;
        walkBool = false;

        if (runBool == false)
        {
            animate.SetBool("Run", true);
            runBool = true;
        }
        else if (runBool == true)
        {
            animate.SetBool("Run", false);
            runBool = false;
        }

    }
}
