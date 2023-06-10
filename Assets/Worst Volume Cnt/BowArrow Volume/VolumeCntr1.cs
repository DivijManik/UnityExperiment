using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeCntr1 : MonoBehaviour
{
    [SerializeField] Slider slider;

    [SerializeField] Transform steering;
    [SerializeField] Transform steeringPoint;

    [SerializeField] Transform bow;

    [SerializeField] Transform ArrowPrefab;

    Quaternion lastRot;

    Transform Arrow;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //lastMousePos = Input.mousePosition;
            lastRot = steering.rotation;
        }
        else if (Input.GetMouseButton(0))
        {
            //float f = (lastMousePos.normalized.x - Input.mousePosition.normalized.x) * 1.5f;
            //steering.LookAt(Input.mousePosition)
            steeringPoint.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            var relativeUp = steeringPoint.TransformDirection(Vector3.forward);
            var relativePos = steeringPoint.position - steering.position;
            Quaternion steer = Quaternion.LookRotation(-relativePos, -relativeUp);

            steering.rotation = steer;
            
            lastRot = steering.rotation;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Arrow.GetComponent<ArrowScript>().AllowMove();
        }
        else
        {
            if(bow.childCount == 0 && Arrow == null)
            {
                Arrow = Instantiate(ArrowPrefab, bow);
            }
            steering.eulerAngles = new Vector3(-90,90,-90);
        }


    }

}


//if (steering.rotation == lastRot)
////    return;
//if (steer.x < 0)
//{
//    slider.value -= 0.0009f;
//}
//else
//{
//    slider.value += 0.0008f;
//}
