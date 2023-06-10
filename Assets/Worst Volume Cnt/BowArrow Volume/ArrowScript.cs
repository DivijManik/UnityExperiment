using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScript : MonoBehaviour
{
    bool Move;

    void Update()
    {
        if(Move)
        {
            transform.Translate(2, 0, 0);
        }
    }

    public void AllowMove()
    {
        Move = true;
        transform.parent = FindObjectOfType<Canvas>().transform;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Slider"))
        {
            Debug.Log(collision.GetContact(0).point);
            collision.transform.GetComponent<Slider>().value = collision.GetContact(0).point.x;

            Destroy(transform.gameObject);
        }

        if(collision.transform.CompareTag("Gate"))
        {
            Destroy(transform.gameObject);
        }
    }
}
