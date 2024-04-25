using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public AudioSource hitSound;
    public float attackBoxDelay = 0.1F;
    public float attackBoxCutOff= 1;
    private BoxCollider attackBox;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        attackBox = GetComponent<BoxCollider>();
        attackBox.enabled = false;
        animator = transform.root.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(1).IsName("Attack") && 
            (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < attackBoxDelay ||
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime > attackBoxCutOff))
        {
            attackBox.enabled = false;
        } else {
            attackBox.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider col)   
    {
        if (col.tag == "hitBox")
        {
            hitSound.Play();
        }
    }
}
