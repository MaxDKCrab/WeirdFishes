using System;
using UnityEngine;

public class BoxSelectionUI : MonoBehaviour
{
    private RTSInput input;
    
    [SerializeField] private GameObject boxSelectUI;

    private RectTransform boxSelectUITransform;

    private void Start()
    {
        input = GetComponent<RTSInput>();
        boxSelectUITransform = boxSelectUI.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!input.boxSelectInfo.relevant)
        {
            boxSelectUI.SetActive(false);
            return;
        }
        
        boxSelectUI.SetActive(true);

        //float pointDistance = Vector2.Distance(input.boxSelectInfo.startPos,input.boxSelectInfo.endPos);
        
        float width = input.boxSelectInfo.endPos.x - input.boxSelectInfo.startPos.x;
        float height = input.boxSelectInfo.endPos.y - input.boxSelectInfo.startPos.y;
        
        boxSelectUITransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Abs(width));
        boxSelectUITransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Abs(height));
        
        boxSelectUITransform.anchoredPosition = input.boxSelectInfo.startPos + new  Vector2(width, height) * 0.5f;

    }
}
