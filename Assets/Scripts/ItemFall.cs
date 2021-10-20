using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemFall : MonoBehaviour
{
    public float fallRate;
    public int itemScore;
    public bool caught;

    private void Start()
    {
        //since fall rate changes throughout the game it needs to be reset at the start of each
        fallRate = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //if item hasn't been caught move it downwards at fallRate
        if (!caught) transform.Translate(-transform.up * fallRate * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if item hits floor destroy item
        if (other.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}