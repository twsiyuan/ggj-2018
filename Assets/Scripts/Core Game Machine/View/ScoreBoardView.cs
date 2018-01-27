using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ScoreBoardView
{	
	
    [SerializeField]
    private Text score;

    [SerializeField]
    private Text completion;
    
    [SerializeField]
    private Text waiting;

    [SerializeField]
    private Text depot;

    [SerializeField]
    private Image rage;
    
    public void UpdateScore(int oldScore, int newScore)
    {
        
    }
    
    public void UpdatePassengerCompletion(int value)
    {
        score.text = "累積乘載數: " + value.ToString();
    }

    public void UpdateWaitingPassenger(int value)
    {
        waiting.text = "等待乘客數:" + value.ToString();
    }

    public void UpdateDepot(string value)
    {
        depot.text = "派車數量: " + value;
    }

    public void UpdateRage(float value)
    {
        rage.fillAmount = value;
    }

    public void UpdateRageMax(int value)
    {

    }

}