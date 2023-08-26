using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropObjs : MonoBehaviour
{
    MergeObj CurrentDraggableObj;

    [SerializeField]
    LayerMask mask;

    bool OnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.TryGetComponent<MergeObj>(out MergeObj m))
                {
                    CurrentDraggableObj = m;
                    m.EndGridMove();
                }
            }
        }
        else if (Input.touchCount > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);

            if (hit.collider != null)
            {
                //OnRegionClick((Region)Enum.Parse(typeof(Region), hit.transform.name));
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (!OnGround)
            {
                CurrentDraggableObj.UseLastPos();
                CurrentDraggableObj = null;
                //return;
            }
            else
            {
                CurrentDraggableObj.UseGridPos();
                CurrentDraggableObj = null;
            }
        }

        if(CurrentDraggableObj != null && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000f,mask))
            {
                if(hit.transform.CompareTag("Background"))
                {
                    OnGround = false;
                }
                else
                {
                    OnGround = true;
                }
                CurrentDraggableObj.transform.position = hit.point + new Vector3(0,0.5f,0);             
            }
        }
    }
}
