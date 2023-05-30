using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private Image _circleImage;

    public void SetSelected(bool isSelected)
    {
        _circleImage.enabled = isSelected;
    }
}
