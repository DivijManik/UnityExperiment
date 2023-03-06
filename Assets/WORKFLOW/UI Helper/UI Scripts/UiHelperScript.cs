using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class UiHelperScript : MonoBehaviour
{
    [SerializeField]
    RectTransform parent;

    [SerializeField] UiPresets preset;
    [SerializeField] Presets Obj;

    [SerializeField]
    TextAnchor anchor;
    [SerializeField] bool TakePresetsAnchor;

    [Range(0,10)]
    [SerializeField] int margin;

    [SerializeField] Vector2 position;

    [SerializeField] bool SetAsParent;
    List<GameObject> actions = new List<GameObject>();

    public void InstantiateUI()
    {
        RectTransform obj_ = Instantiate(preset.GetObj(Obj) , parent.transform);
        obj_.name = preset.GetObj(Obj).name;

        Vector2 pos = RectTransformPresetApplyUtils.ToViewportCoords(anchor);

        //  Custom Anchor
        if (!TakePresetsAnchor)
        {
            obj_.anchorMin = pos;
            obj_.anchorMax = pos;
            obj_.pivot = pos;
        }

        obj_.anchoredPosition = new Vector2(pos.x==0?margin : pos.x == 1 ? -margin : 0, pos.y == 0 ? margin : pos.y == 1? -margin:0);
        obj_.anchoredPosition += position;

        actions.Add(obj_.gameObject);

        if (SetAsParent)
            parent = obj_;
    }

    public void DestroyLast()
    {
        try
        {
            GameObject g = actions.Last();
            actions.Remove(g);

            DestroyImmediate(g);
        }
        catch
        {
            Debug.Log("Undo Not Allowed");
        }
    }
}

public enum Anchor
{
    top,
    topLeft,
    topRight
}