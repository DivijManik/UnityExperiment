using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObjectsScript : MonoBehaviour
{
    [SerializeField]
    Transform ObjToSnap;

    [SerializeField]
    LayerMask mask;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                ObjToSnap.position = hit.point + new Vector3(0, (ObjToSnap.GetComponent<MeshFilter>().mesh.bounds.size.y / 2) - ObjToSnap.GetComponent<MeshFilter>().mesh.bounds.center.y, 0);
            }
        }
    }
}
