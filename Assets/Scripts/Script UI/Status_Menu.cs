using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Status_Menu : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI damegeText;

    public void UpdateBar(int currentValue, int maxValue){
        float fillValue = (float)currentValue / maxValue; // gia trị hiện tại / giá trị tối đa = lượng máu đang có
        fillBar.fillAmount = fillValue;
        valueText.text = maxValue.ToString();
    }

    public void UpdateDamege(int damege){
        damegeText.text = damege.ToString();
    }
}
