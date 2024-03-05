using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HudController : MonoBehaviour
{
    public TMP_Text text;
    public Rigidbody rb;
    private double speed;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = Math.Round(Vector3.Magnitude(rb.velocity));

        text.text = speed.ToString();
    }
}
