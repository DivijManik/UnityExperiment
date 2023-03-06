using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCubeOnEdges : MonoBehaviour
{
    [SerializeField] Rigidbody RB;

    private void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(1, 0, 0,Space.World); 
            RB.velocity = Vector3.forward *1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-1, 0, 0, Space.World);
            RB.velocity = -Vector3.forward * 1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -1, Space.World); 
            RB.velocity = Vector3.right * 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, 1, Space.World);
            RB.velocity = -Vector3.right * 1;
        }
    }
}
