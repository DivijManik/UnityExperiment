using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using TMPro;
using static UnityEngine.Networking.UnityWebRequest;
using static UnityEditor.PlayerSettings;

public class WordScape : MonoBehaviour
{
    List<RaycastResult> results = new List<RaycastResult>();

    PointerEventData PointerEventData;

    [SerializeField]
    LineRenderer LR;

    int LinePos;

    List<GameObject> WordSeq = new List<GameObject>();

    [SerializeField]
    Transform panel;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            PointerEventData = new PointerEventData(EventSystem.current);
            PointerEventData.position = Input.mousePosition;

            EventSystem.current.RaycastAll(PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.transform.CompareTag("Player") && !WordSeq.Contains(result.gameObject))
                {
                    LR.positionCount = LinePos + 2;
                    LR.SetPosition(LinePos, result.gameObject.transform.position);
                    WordSeq.Add(result.gameObject);
                    LinePos++;

                    if(!LR.gameObject.activeInHierarchy)
                        LR.gameObject.SetActive(true);
                }
            }
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 90;
            LR.SetPosition(LinePos, pos);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            string word = "";

            foreach (GameObject seq in WordSeq)
            {
                word += seq.GetComponent<TextMeshProUGUI>().text;
            }
            Debug.Log(word);

            // CLEAR
            for (int i = 0; i < LR.positionCount; i++)
            {
                LR.SetPosition(i, Vector3.zero);
            }
            LinePos = 0; WordSeq.Clear();
            LR.gameObject.SetActive(false);
        }
    }
}
