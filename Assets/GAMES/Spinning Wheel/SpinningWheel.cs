using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningWheel : MonoBehaviour
{
    bool spin;

    [SerializeField] Transform Wheel;
    [SerializeField] Transform Needle;
    float speed = 7;

    List<Collider2D> results = new List<Collider2D>();

    public void OnSpinClick()
    {
        spin = true;
        speed = Random.Range(3, 7);

    }

    private void Update()
    {
        if(spin)
        {
            Wheel.Rotate(0, 0, speed);
            speed = speed - Time.deltaTime;
        }

        if(speed<=0 && spin)
        {
            speed = 0;
            spin = false;

            ContactFilter2D contactFilter = new ContactFilter2D();
            Needle.GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, results);

            foreach(Collider2D r in results)
            {
                Debug.Log(r.name);
            }
        }
    }
}
