using Customize;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelector : MonoBehaviour
{
    private Manager Manager { get => Manager.Instance; }
    private SelectorHandler SelectorHandler { get => Manager.selectorHandler; }

    private GameObject unitSelector;

    void Start() 
    {
        unitSelector = GameObject.Find("UnitSelector");
    }

    public void CreateUnitSelector()
    {
        var unitCount = Manager.unitList.Count;
        for (int i = 0; i < unitCount; i++)
        {
            CreateUnitSelectorItem(i);
        }
    }

    private void CreateUnitSelectorItem(int num)
    {
        var item = new GameObject($"Unit{num + 1}");
        item.transform.parent = unitSelector.transform;

        item.AddComponent<RectTransform>();
        item.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

        item.AddComponent<Image>();

        var temp = num;

        item.AddComponent<Button>();
        item.GetComponent<Button>().onClick.AddListener(() => SelectorHandler.ChangeUnit(temp)); 
    }
}
