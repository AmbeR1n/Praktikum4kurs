using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private List<Sprite> backgroundSprites;

    public void ChangeBG(int bgIndex) =>
        GetComponent<Image>().sprite = backgroundSprites[bgIndex];
}