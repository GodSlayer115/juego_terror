using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapFill : MonoBehaviour
{
    public int maxTrapTime;
    public float currTrapTime;
    public float decayTrapTime;
    public float pressEcharge = 2.5f;
    public Image fill;
    [SerializeField] private GameObject trapFill;

    private void Start()
    {
        trapFill.SetActive(false);
    }
    void Update()
    {
        if(currTrapTime < 0) 
        { currTrapTime = 0; }
        if (Input.GetKeyUp(KeyCode.E)) 
        { currTrapTime += pressEcharge ; }
        currTrapTime -= decayTrapTime;
        getCurrentFill();

    }

    void getCurrentFill()
    {
        float fillAmount = (float)currTrapTime / (float)maxTrapTime;
        fill.fillAmount = fillAmount;
    }
}
