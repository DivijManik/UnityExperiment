using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiHelperScript : MonoBehaviour
{
    [Tooltip("If Empty It will be set as the Canvas or Transform by default")]
    [Header("Set Canvas or a UI object as the parent")]
    [SerializeField] Transform parent;

    [Header("Css code goes here")]
    [SerializeField] List<string> CssCode;

    [Header("Will Ui have any componenet? (it will be set to each Ui in CssCode List)")]
    [SerializeField] Type UiObjType;

    void Start()
    {
        #region parent for Ui
        if (parent == null)
        {
            //Try Finding Canvas if it fails it will add or getComponent to the script holder
            try
            {
                parent = FindFirstObjectByType<Canvas>().transform;
            }
            catch
            {
                
                if (!gameObject.TryGetComponent<Canvas>(out Canvas c))
                {
                    Debug.LogWarning("Adding a canvas to the object holding UiHelperScript");
                    gameObject.AddComponent<Canvas>();
                }
                parent = transform;
            }
        }
        #endregion

        // Css to Unity UI 

        StartCoroutine(UiDelay());
    }

    IEnumerator UiDelay()
    {
        foreach (string s in CssCode)
        {
            Debug.Log(s);
            string v = "";
            List<string> key = new List<string>(), value = new List<string>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ':')
                {
                    key.Add(v);
                    v = "";
                }
                else if (s[i] == ';')
                {
                    value.Add(v);
                    v = "";
                }
                else if (s[i] != ' ')
                {
                    v += s[i];
                }
            }

            CssToUi(key, value);

            yield return new WaitUntil(WaitTillReady);
        }
    }
    bool ready = true;
    bool WaitTillReady()
    {
        if (ready)
            return true;

        return false;
    }

    /// <summary>
    ///
    ///             converts Css code to UI
    /// </summary>
    Dictionary<string, string> dict = new Dictionary<string, string>();
    RectTransform UiObj;
    void CssToUi(List<string> key, List<string> value)
    {
        ready = false;
        dict.Clear();
        for (int i = 0; i < key.Count; i++)
        {
            try
            {
                dict.Add(key[i], value[i]);
                Invoke(key[i], 0);
            }
            catch { }
        }

        ready = true;
    }

    void position()
    {
        switch (dict["position"].ToLower())
        {
            case "absolute":
                UiObj = new GameObject().AddComponent<RectTransform>();
                UiObj.parent = parent;
                //UiObj = Instantiate(new GameObject(), parent).AddComponent<RectTransform>();
                break;
            case "relative":
                UiObj = new GameObject().AddComponent<RectTransform>();
                UiObj.parent = parent;
                break;
            case null:
                UiObj = new GameObject().AddComponent<RectTransform>();
                UiObj.parent = parent;
                break;
        }

        switch (UiObjType)
        {
            case Type.Image:
                UiObj.gameObject.AddComponent<Image>();
                break;
            case Type.Button:
                UiObj.gameObject.AddComponent<Button>();
                break;
            case Type.Text:
                UiObj.gameObject.AddComponent<TextMeshProUGUI>();
                break;
        }
    }

    void width()
    {
        string value = dict["width"];

        value = Regex.Match(value, @"\d+").Value;
        UiObj.sizeDelta = new Vector2(int.Parse(value), UiObj.sizeDelta.y);
    }

    void height()
    {
        string value = dict["height"];
        value = Regex.Match(value, @"\d+").Value;
        UiObj.sizeDelta = new Vector2(UiObj.sizeDelta.x, int.Parse(value));
    }

    void left()
    {
        UiObj.anchorMin = new Vector2(0, UiObj.anchorMin.y);
        UiObj.anchorMax = new Vector2(0, UiObj.anchorMax.y);
        UiObj.pivot = new Vector2(0, UiObj.anchorMax.y);

        string value = dict["left"];
        value = Regex.Match(value, @"\d+").Value;
        UiObj.anchoredPosition = new Vector3(int.Parse(value), UiObj.anchoredPosition.y);
    }

    void right()
    {
        UiObj.anchorMin = new Vector2(1, UiObj.anchorMin.y);
        UiObj.anchorMax = new Vector2(1, UiObj.anchorMax.y);
        UiObj.pivot = new Vector2(1, UiObj.anchorMax.y);

        string value = dict["right"];
        value = Regex.Match(value, @"\d+").Value;
        UiObj.anchoredPosition = new Vector3(-Mathf.Abs(int.Parse(value)), UiObj.anchoredPosition.y);
    }

    void top()
    {
        UiObj.anchorMin = new Vector2(UiObj.anchorMin.x,1);
        UiObj.anchorMax = new Vector2(UiObj.anchorMax.x, 1);
        UiObj.pivot = new Vector2(UiObj.anchorMax.x, 1);

        string value = dict["top"];
        value = Regex.Match(value, @"\d+").Value;
        UiObj.anchoredPosition = new Vector3(UiObj.anchoredPosition.x, -Mathf.Abs(int.Parse(value)));
    }

    void bottom()
    {
        UiObj.anchorMin = new Vector2(UiObj.anchorMin.x, 0);
        UiObj.anchorMax = new Vector2(UiObj.anchorMax.x, 0);
        UiObj.pivot = new Vector2(UiObj.anchorMax.x, 0);

        string value = dict["bottom"];
        value = Regex.Match(value, @"\d+").Value;
        UiObj.anchoredPosition = new Vector3(UiObj.anchoredPosition.x, int.Parse(value));
    }
}

public enum Type
{
    NULL,
    Image,
    Button,
    Text
}