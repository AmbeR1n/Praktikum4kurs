using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public List<Sprite> emotions;
    public string characterName;

    public void ChangeEmotion(int currentExpression) =>
        GetComponent<Image>().sprite = emotions[currentExpression];

    public void SetTransparent(bool isTransparent)
    {
        var image = GetComponent<Image>();
        if (image != null)
        {
            Color color = image.color;
            color.a = isTransparent ? 0f : 1f;
            image.color = color;
        }
    }
}
