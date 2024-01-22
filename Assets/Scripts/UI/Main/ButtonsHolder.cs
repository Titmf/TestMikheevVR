using System;

using TMPro;

using UI;

using UnityEngine;
using UnityEngine.UI;

public class ButtonsHolder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _buttonsText;
    [SerializeField] private Image[] _buttonImages;
    [SerializeField] private MainUI _mainUI;
    
    private int? _currentIndex = null;
    
    private Color32 BasicColor = new Color32(255,210,0,255);
    private Color32 PickedColor = new Color32(180,150,0,255);

    private void Awake()
    {
        _mainUI.OnComponentButtonPressed += SetButtonImage;
    }

    private void Start()
    {
        for (var index = 0; index < _buttonsText.Length; index++)
        {
            int Number = index + 1;
            _buttonsText[index].text = "Компонент #" + Number;
        }
    }

    private void SetButtonImage(int index)
    {
        if (_currentIndex == index)
        {
            _buttonImages[index].color = BasicColor;
        }
        else
        {
            if (_currentIndex != null)
            {
                _buttonImages[(int) _currentIndex].color = BasicColor;
            }

            _currentIndex = index;
                
            _buttonImages[(int) _currentIndex].color = PickedColor;
        }
    }

    private void OnDestroy()
    {
        _mainUI.OnComponentButtonPressed -= SetButtonImage;
    }
}