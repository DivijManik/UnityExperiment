using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotScript : MonoBehaviour
{
    Vector3[] ElasticPos =
        new Vector3[]{ new Vector3(1,0,-4.5f), new Vector3(0, 0, -5), new Vector3(-1, 0, -4.5f) };

    int[] LinePos = new int[]{ 1, 2, 3 };

    [SerializeField]
    LineRenderer LR;

    bool move;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            move = true;

            transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, 100))
            //{
            //    if (hit.transform == this.transform)
            //    {
            //    }
            //}
        }
        else if(Input.GetMouseButtonUp(0))
        {
            move = false;
            
            transform.GetComponent<Rigidbody>().AddForce(-(LR.GetPosition(2) * (LR.GetPosition(2).z*-10)));
            //transform.GetComponent<Rigidbody>().useGravity = true;
        }

        if(move)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,0, mousePos.y));
            worldPos.y = 0;
            worldPos.z *= 0.02f;
            worldPos.x *= 0.04f;
            worldPos.z -= 5; // offset

            if (worldPos.z > 0.6)
                worldPos.z = 0.6f;

            LR.SetPosition(1, ElasticPos[0] + worldPos);
            LR.SetPosition(2, ElasticPos[1] + worldPos);
            LR.SetPosition(3, ElasticPos[2] + worldPos);

            transform.position = LR.GetPosition(2) + new Vector3(0, 0, 0.5f); //offset for ball
        }
        
    }
}
