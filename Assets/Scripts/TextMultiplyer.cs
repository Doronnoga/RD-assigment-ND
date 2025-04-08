using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextValueChanger : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI textElement;
    public Animator animator;

    [Header("Value Settings")]
    public float startValue = 1.0f;
    public float endValue = 2.0f;
    public float changeDuration = 2f;

    [Header("Color Settings")]
    public List<Color> colors = new List<Color> { Color.white, Color.yellow, Color.red, Color.magenta };

    private bool isChanging = false;
    private float timer = 0f;

    public void OnStart()
    {
        isChanging = true;
        timer = 0f;
    }

    public void OnEnd()
    {
        isChanging = false;
    }

    void Update()
    {
        if (!isChanging) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / changeDuration);

        float currentValue = Mathf.Lerp(startValue, endValue, t);
        animator.SetFloat("Value", currentValue);
        textElement.text = $"{currentValue:F1}X";

        if (colors.Count >= 2)
        {
            float segmentLength = 1f / (colors.Count - 1);
            int index = Mathf.FloorToInt(t / segmentLength);

            if (index < colors.Count - 1)
            {
                float segmentT = (t - (segmentLength * index)) / segmentLength;
                textElement.color = Color.Lerp(colors[index], colors[index + 1], segmentT);
            }
            else
            {
                textElement.color = colors[^1];
            }
        }

        if (t >= 1f)
        {
            isChanging = false;
        }
    }
}