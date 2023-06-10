using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Slider slider;

    [SerializeField] Transform preafabParent;

    [SerializeField] Transform UpPrefab;
    [SerializeField] Transform DownPrefab;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Transform t = Instantiate(UpPrefab, preafabParent);
            RandPos(new Vector2(0, 0), new Vector2(800, 1600), t);

            Transform t1 = Instantiate(DownPrefab, preafabParent);
            RandPos(new Vector2(0, 0), new Vector2(800, 1600), t1);
        }
    }
    private void Update()
    {
        transform.Translate(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    void RandPos(Vector2 min, Vector2 max, Transform obj)
    {
        obj.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Gate"))
        {
            slider.value += 1;
        }

        if (collision.CompareTag("Slider"))
        {
            slider.value -= 1;
        }

        RandPos(new Vector2(0, 0), new Vector2(800, 1600), collision.transform);
    }
}
