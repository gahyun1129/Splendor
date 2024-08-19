using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exScrollBar : MonoBehaviour
{
    private ScrollRect scrollRect;      
    public float space = 50f;           // prefab 오브젝트 간의 간격
    public GameObject uiPrefab;         // prefab 오브젝트
    public List<RectTransform> uiObjects = new List<RectTransform>();

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    public void AddNewUiObject()
    {
        // 객체 추가
        var newUi = Instantiate(uiPrefab, scrollRect.content).GetComponent<RectTransform>();
        uiObjects.Add(newUi);

        // 객체의 y값 설정
        float y = 0f;
        for ( int i = 0; i < uiObjects.Count; i++)
        {
            uiObjects[i].anchoredPosition = new Vector2(0f, -y);
            y += uiObjects[i].sizeDelta.y + space;
        }

        // content 영역 설정
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, y);
    }
}
