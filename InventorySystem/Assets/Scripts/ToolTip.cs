using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    // 提示文本
    private Text toolTipText;
    // 内容文本
    private Text contentText;
    private CanvasGroup canvasGroup;
    // 目标Alpha值
    private float targetAlpha = 0;
    // 显示隐藏的速度
    public float smoothing = 1;

    void Start()
    {
        toolTipText = this.GetComponent<Text>();
        contentText = transform.Find("Content").GetComponent<Text>();
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (canvasGroup.alpha != targetAlpha)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, smoothing * Time.deltaTime);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) < 0.01f)
            {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }

    public void Show(string text)
    {
        contentText.text = text;
        toolTipText.text = text;
        targetAlpha = 1;
    }

    public void Hide()
    {
        targetAlpha = 0;
    }

    public void SetLocalPotion(Vector3 position)
    {
        transform.localPosition = position;
    }
}
