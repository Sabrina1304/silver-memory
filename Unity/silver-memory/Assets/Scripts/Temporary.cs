﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Temporary : MonoBehaviour
{
    private bool coroutineStarted = false;
    private Collider collisioner;

    private void Start()
    {
        collisioner = this.GetComponents<Collider>()[0];
    }
    // Update is called once per frame
    void Update()
    {
        if (collisioner.isTrigger && !coroutineStarted)
        {
            Debug.Log("Unfading");
            StartCoroutine(Fade(false,5f));
        }
    }
    private IEnumerator Fade(bool state,float timer)
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(timer);
        collisioner.isTrigger = state;
        coroutineStarted = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.StartsWith("Player") && !coroutineStarted && !collisioner.isTrigger)
        {
            StartCoroutine(Fade(true,2f));
        }
    }
}
