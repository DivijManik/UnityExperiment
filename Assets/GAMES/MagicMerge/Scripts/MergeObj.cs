using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class MergeObj : MonoBehaviour
{
    Vector3 LastPos;

    [SerializeField]public ObjType objType;

    bool checked_;
    void Start()
    {
        LastPos = transform.position;
    }

    public void UseGridPos()
    {
        if (GridData.Instance.CheckGridPosEmpty(new Vector3(Mathf.RoundToInt(transform.position.x), 0.5f, Mathf.RoundToInt(transform.position.z))))
        {
            Vector3 newPos = new Vector3(Mathf.RoundToInt(transform.position.x), 0.5f, Mathf.RoundToInt(transform.position.z));

            CheckNearbyObjectsCount(newPos);
            transform.DOMoveX(Mathf.RoundToInt(transform.position.x), 1f);
            transform.DOMoveZ(Mathf.RoundToInt(transform.position.z), 1f);
            LastPos = newPos;
        }
        else
        {
            transform.DOMove(LastPos, 1f);
        }
    }

    public void EndGridMove()
    {
        transform.DOComplete();
    }

    void CheckNearbyObjectsCount(Vector3 pos)
    {
        List<MergeObj> mergeableObjs = GridData.Instance.CheckGridPosNearby(pos, objType);

        if (mergeableObjs == null)
            return;
        if (mergeableObjs.Count > 1)
        {
            foreach (var item in mergeableObjs)
            {
                if (item != this)
                    Destroy(item.gameObject);
            }
            UpgradeItem();
        } 
    }

    public void CheckNearbyObjectsCountOnce(Vector3 pos)
    {
        List<MergeObj> mergeableObjs = GridData.Instance.CheckGridPosNearbyOnce(pos, objType);

        if (mergeableObjs == null)
            return;
        if (mergeableObjs.Count > 1)
        {
            foreach (var item in mergeableObjs)
            {
                if (item != this)
                    Destroy(item.gameObject);
            }
            UpgradeItem();
        }
    }

    public void UpgradeItem()
    {
        Debug.Log("Item Upgraded");
    }
}

public enum ObjType
{
    Cube,
    Sphere,
    Flower
}
