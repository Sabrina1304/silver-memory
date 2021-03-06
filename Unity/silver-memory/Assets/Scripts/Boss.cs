﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    private bool activated = false;
    public int life = 20;
    private Animator animator;

    public float attackTimer;

    public GameObject rock;
    private bool coroutineStarted = false;
    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Surement bouger cette condition dans gameManager, sinon le boss sera tout le temps affiché
        if(Vector3.Distance(this.transform.position,gameManager.player.gameObject.transform.position) < 50f && ! gameManager.player.carryingEle && !activated)
        {
            Debug.Log("Vous êtes arrivé au boss, il s'active");
            activated = true;
        }
        if(activated && life > 0 && !coroutineStarted)
        {
            coroutineStarted = true;
            int random = Random.Range(0, 3);
            if (0 == random)
            {
                //Debug.Log("attack 0");
                coroutineStarted = false;
            }
            else if(random == 1)
            {
                StartCoroutine(AttackMove1());
            }
            else if (random == 2)
            {
                StartCoroutine(AttackMove2());
            }
        }
        else if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private IEnumerator AttackMove1()
    {
        animator.SetTrigger("Attack1");
        //Debug.Log("1");
        GameObject falling = Instantiate(rock, new Vector3(gameManager.player.transform.position.x, this.transform.position.y + 3*this.transform.localScale.y, gameManager.player.transform.position.z),Quaternion.identity);
        Destroy(falling, 5f);
        yield return new WaitForSeconds(attackTimer);
        coroutineStarted = false;
    }
    private IEnumerator AttackMove2()
    {
        animator.SetTrigger("Attack2");
        //Debug.Log("2");
        GameObject rising = Instantiate(rock, new Vector3(gameManager.player.transform.position.x, this.transform.position.y  - 3* this.transform.localScale.y, gameManager.player.transform.position.z), Quaternion.identity);
        Rigidbody rb = rising.GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddForce(9.81f * Vector3.up, ForceMode.Impulse);
        Destroy(rising, 5f);
        yield return new WaitForSeconds(attackTimer);
        coroutineStarted = false;
    }
}
