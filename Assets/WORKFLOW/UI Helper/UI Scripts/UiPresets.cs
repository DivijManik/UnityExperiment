using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/UiPresets", order = 1)]
public class UiPresets : ScriptableObject
{
    [SerializeField] public RectTransform Button;
    [SerializeField] public RectTransform Panel;
    [SerializeField] public RectTransform InputField;

    public RectTransform GetObj(Presets p)
    {
        switch(p)
        {
            case Presets.Button:
                return Button;
            case Presets.InputField:
                return InputField;
            case Presets.Panel:
                return Panel; 
        }

        return null;
    }
}

public enum Presets
{
    Button,
    Panel,
    InputField
}
