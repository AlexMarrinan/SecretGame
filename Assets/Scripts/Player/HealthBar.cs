using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = (float) PlayerStats.instance.currentHP / (float) PlayerStats.instance.maxHP;
        text.text = 
            PlayerStats.instance.currentHP.ToString() +
            " / " +
            PlayerStats.instance.maxHP.ToString();
    }
}
