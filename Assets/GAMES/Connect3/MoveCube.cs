using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCube : MonoBehaviour
{
    bool Move = false;

    CubeScript CubeToMove;
    CubeScript CubeToMove2;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("hit");
                if (hit.transform.CompareTag("Player"))
                {
                    CubeToMove = hit.transform.GetComponent<CubeScript>();
                    Move = true;
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (CubeToMove2 == null)
                return;

            CubeToMove.transform.DOMove(CubeToMove2.transform.position, 0.2f).OnComplete
            (() =>{
                CubeToMove.setPos();
                CubeToMove = null;
            });

            CubeToMove2.transform.DOMove(new Vector3(CubeToMove.x, CubeToMove.y, 0), 0.2f).OnComplete
            (() => {
                CubeToMove2.setPos();
                CubeToMove2 = null;
            });
        }

        if(Move)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("hit");
                if (hit.transform.CompareTag("Player") && hit.transform != CubeToMove.transform)
                {
                    CubeToMove2 = hit.transform.GetComponent<CubeScript>();
                    Move = false;
                }
            }
        }
    }
}
