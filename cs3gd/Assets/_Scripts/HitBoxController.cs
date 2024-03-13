using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    private BoxCollider hitBox;
    public AudioSource hurtSound;
    public float iFrames = 60;
    private float lastHit = 0;

    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float timeSinceLastHit = Time.time - lastHit;

        if (timeSinceLastHit > iFrames)
        {
            hitBox.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "attackBox")
        {
            lastHit = Time.time;
            hurtSound.Play();

            if (hitBox.enabled)
            {
                hitBox.enabled = false;
            }
        }
    }

    private void roll(){
        lastHit = Time.time;

        if (hitBox.enabled)
            {
                hitBox.enabled = false;
            }
    }
}
