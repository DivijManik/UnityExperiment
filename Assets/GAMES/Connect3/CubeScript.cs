using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CubeScript : MonoBehaviour
{
    [SerializeField]
    public int x, y;

    [SerializeField]
    TextMeshProUGUI Text;

    void Start()
    {
        setPos();
    }

    public void setPos()
    {
        x = (int)transform.position.x;
        y = (int)transform.position.y;
    }

    public void setText(string s)
    {
        Text.text = s;
    }
}
