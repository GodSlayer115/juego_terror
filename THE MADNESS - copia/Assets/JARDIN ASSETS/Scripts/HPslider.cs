using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPslider : MonoBehaviour
{
    public int maxHP;
    public int currHP;
    public int playerHP;
    public Image fill; 

    public Health playerHealth;
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHP = playerHealth.health;
        currHP = playerHP;
        getCurrentFill();
    }

    void getCurrentFill()
    {
        float fillAmount = (float) currHP / (float) maxHP;
        fill.fillAmount = fillAmount;
    }
}
