using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Slider kickPowerSlider;

    [SerializeField] private FieldPlayer player;
    
    private void Start()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }

    private void Update()
    {
        while (player.sliderDown)
        {
            kickPowerSlider.value = Mathf.Lerp(kickPowerSlider.value, 30, 4f * Time.deltaTime);
            break;
        }
    }
    

    public void SetSlider(float count)
    {
        kickPowerSlider.value = count;
    }
}
