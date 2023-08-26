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

            transform.DOMoveX(Mathf.RoundToInt(transform.position.x), 1f);
            transform.DOMoveZ(Mathf.RoundToInt(transform.position.z), 1f);
            LastPos = newPos;

            CheckNearbyObjectsCount(newPos);

        }
        else
        {
            transform.DOMove(LastPos, 1f);
        }
    }
    public void UseLastPos()
    {
        transform.DOMove(LastPos, 1f);
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
                {
                    item.EndGridMove();
                    item.transform.DOMove(transform.position, 1f).OnComplete(() => { Destroy(item.gameObject); });
                }
            }
            StartCoroutine(UpgradeItem());
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
                {
                    item.EndGridMove();
                    item.transform.DOMove(transform.position, 1f).OnComplete(() => { Destroy(item.gameObject); });                    
                }
            }
            StartCoroutine(UpgradeItem());
        }
    }

    IEnumerator UpgradeItem()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Item Upgraded");
        GridData.Instance.AddToMergeObjs(this);

        // ^ this is the upgraded item
        // we can also destroy this here and before that instantiate a level 2 prefab
    }
}

public enum ObjType
{
    Cube,
    Sphere,
    Flower
}
