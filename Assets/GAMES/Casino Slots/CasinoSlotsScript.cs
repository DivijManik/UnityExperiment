using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasinoSlotsScript : MonoBehaviour
{
    bool Roll;

    [SerializeField] RectTransform Slot;

    float speed = 5;

    List<Collider2D> results = new List<Collider2D>();

    public void OnRollClick()
    {
        Roll = true;
        speed = 5;
    }

    private void Update()
    {
        if (Roll)
        {
            Slot.anchoredPosition += new Vector2(0, speed);
            speed = speed - Time.deltaTime;

            if(Slot.anchoredPosition.y >= (Slot.transform.childCount-1) * 200)
            {
                Slot.anchoredPosition = new Vector2(0, 0);
            }
        }

        if (speed <= 0 && Roll)
        {
            speed = 0;
            Roll = false;
        }
    }

}
