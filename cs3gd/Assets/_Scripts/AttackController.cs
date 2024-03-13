using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public AudioSource hitSound;
    public float attackLength;
    private BoxCollider attackBox;
    private double timeLastAttacked;

    // Start is called before the first frame update
    void Start()
    {
        attackBox = GetComponent<BoxCollider>();
        attackBox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeAsDouble - timeLastAttacked >= attackLength){
            attackBox.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider col)   
    {
        if (col.tag == "hitBox")
        {
            hitSound.Play();
        }
    }

    private void attacking(){
        attackBox.enabled = true;
        timeLastAttacked = Time.timeAsDouble;
    }
}
