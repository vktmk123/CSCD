using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;

    public void UpdateBar(float currentValue, float maxValue){
        float fillValue = currentValue / maxValue; // gia trị hiện tại / giá trị tối đa = lượng máu đang có
        fillBar.fillAmount = fillValue;
        valueText.text = currentValue.ToString() + "/" + maxValue.ToString();
    }

}
