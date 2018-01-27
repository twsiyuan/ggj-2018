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
    private Text depot;

    public void UpdateScore(int value)
    {
        score.text = "累積乘客數: " + value.ToString();
    }
    

    public void UpdateDepot(string value)
    {
        depot.text = "車站數量: " + value;
    }

}