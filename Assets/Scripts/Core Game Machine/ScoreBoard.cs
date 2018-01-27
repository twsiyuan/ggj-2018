using UnityEngine;

public class ScoreBoard : MonoBehaviour
{	

    [SerializeField]
    private int score;
	
    [SerializeField]
    private int rage;

    [SerializeField]
    private ScoreBoardView view = new ScoreBoardView();

    public void UpdateScore()
    {
        score++;
        view.UpdateScore(score);
    }

    public void UpdateDepot(string value)
    {
        view.UpdateDepot(value);
    }

}