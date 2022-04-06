using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveBehaviour : MonoBehaviour
{
    float damage = 20f;
    float range = 4f;
    float timeToLive = 1f;

    float delay = 0.3f;
    float count = 0;

    private float startingValue; //starting value and running value
    private float value;

    void Start()
    {
        startingValue = 0.05f;
        value = (range / 50) / timeToLive;
    }

    void FixedUpdate()
    {
        timeToLive -= Time.fixedDeltaTime;
        count += Time.fixedDeltaTime;

            if (timeToLive < 0)
            {
                Destroy(gameObject);
            }

        transform.localScale += new Vector3(value, value, value);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //deal damage
        }
    }
}
