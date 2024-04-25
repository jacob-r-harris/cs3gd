using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.Properties;
using System.Runtime.CompilerServices;

public class HudController : MonoBehaviour
{
    public TMP_Text text;
    public GameObject player;
    private float health;
    private float newHealth;
    Image[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();

        health = player.GetComponentInChildren<HitBoxController>().health;

        hearts = gameObject.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            newHealth = player.GetComponentInChildren<HitBoxController>().health;

            if (newHealth != health)
            {
                health = newHealth;

                if (health < 0)
                {
                    health = 0;
                } else if (health > 6) {
                    health = 6;
                }

                Debug.Log("Health:" + health.ToString());

                hearts[(int)health+1].enabled = false;
            }
        } else {
            foreach (Image image in hearts)
            {
                image.enabled = false;
            }
        }
    }
}
