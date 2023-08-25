using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using static UnityEditor.Progress;

public class GridData : MonoBehaviour
{
    [SerializeField]
    List<MergeObj> MergeObjs = new List<MergeObj>();

    public static GridData Instance;

    [SerializeField] List<Vector3> PosiblePositions;
    private void Start()
    {
        foreach (var item in FindObjectsOfType<MergeObj>())
        {
            MergeObjs.Add(item);
        }

        Instance = this;

        PosiblePositions.Add(new Vector3(1, 0, 0));
        PosiblePositions.Add(new Vector3(0, 0, 1));
        PosiblePositions.Add(new Vector3(-1, 0, 0));
        PosiblePositions.Add(new Vector3(0, 0, -1));
        PosiblePositions.Add(new Vector3(1, 0, 1));
        PosiblePositions.Add(new Vector3(-1, 0, -1));
        PosiblePositions.Add(new Vector3(-1, 0, 1));
        PosiblePositions.Add(new Vector3(1, 0, -1));
    }

    public bool CheckGridPosEmpty(Vector3 pos)
    {
        foreach (var item in MergeObjs)
        {
            if(item.transform.position.x == pos.x && item.transform.position.z == pos.z)
            {
                return false;
            }
        }

        return true;
    }

    public List<MergeObj> CheckGridPosNearby(Vector3 pos, ObjType type)
    {
        List<MergeObj> ObjsThatCanBeMerged = new List<MergeObj>();
        foreach (Vector3 pos_ in PosiblePositions)
        {
            //Debug.Log("X  " +(pos + pos_).x);
            //Debug.Log("Z  " +(pos + pos_).z);
            MergeObj m = MergeObjs.FirstOrDefault(item => item.objType == type && item.transform.position.x == (pos+pos_).x && item.transform.position.z == (pos + pos_).z);

            if(m != null)
            {
                ObjsThatCanBeMerged.Add(m);
            }
        }

        if(ObjsThatCanBeMerged.Count >1)
        {
            foreach (var item in ObjsThatCanBeMerged)
            {
                MergeObjs.Remove(item);
            }
        }
        else if(ObjsThatCanBeMerged.Count == 1)
        {
            ObjsThatCanBeMerged[0].CheckNearbyObjectsCountOnce(ObjsThatCanBeMerged[0].transform.position);
            //CheckMergePosForEach(type);
        }
        return ObjsThatCanBeMerged;
    }

    void CheckMergePosForEach(ObjType type)
    {
        foreach (var item in MergeObjs)
        {
            if(item.objType == type)
                item.CheckNearbyObjectsCountOnce(item.transform.position);
        }
    }

    public List<MergeObj> CheckGridPosNearbyOnce(Vector3 pos, ObjType type)
    {
        List<MergeObj> ObjsThatCanBeMerged = new List<MergeObj>();
        foreach (Vector3 pos_ in PosiblePositions)
        {
            //Debug.Log("X  " +(pos + pos_).x);
            //Debug.Log("Z  " +(pos + pos_).z);
            MergeObj m = MergeObjs.FirstOrDefault(item => item.objType == type && Mathf.RoundToInt(item.transform.position.x) == (pos + pos_).x && Mathf.RoundToInt(item.transform.position.z) == (pos + pos_).z);

            if (m != null)
            {
                ObjsThatCanBeMerged.Add(m);
            }
        }

        if (ObjsThatCanBeMerged.Count > 1)
        {
            foreach (var item in ObjsThatCanBeMerged)
            {
                MergeObjs.Remove(item);
            }
        }

        return ObjsThatCanBeMerged;
    }
}
