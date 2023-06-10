using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeCntr : MonoBehaviour
{
    [SerializeField] Slider slider;

    [SerializeField] Transform steering;
    [SerializeField] Transform steeringPoint;
    Quaternion lastRot;
    
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
            Quaternion steer = Quaternion.LookRotation(relativePos, relativeUp);

            bool b = GetRotateDirection(lastRot, steer);
            steering.rotation = steer;
            //if (steering.rotation == lastRot)
            //    return;
            if (steer.x < 0)
            {
                slider.value -= 0.0009f;
            }
            else
            {
                slider.value += 0.0008f;
            }

            lastRot = steering.rotation;
        }
        else
        {
            steering.eulerAngles = new Vector3(-90,90,90);
        }


    }

    bool GetRotateDirection(Quaternion from, Quaternion to)
    {
        Debug.Log(to.x);
        float fromY = from.eulerAngles.x;
        float toY = to.eulerAngles.x;
        float clockWise = 0f;
        float counterClockWise = 0f;

        if (fromY <= toY)
        {
            clockWise = toY - fromY;
            counterClockWise = fromY + (360 - toY);
        }
        else
        {
            clockWise = (360 - fromY) + toY;
            counterClockWise = fromY - toY;
        }
        return (clockWise <= counterClockWise);
    }
}
