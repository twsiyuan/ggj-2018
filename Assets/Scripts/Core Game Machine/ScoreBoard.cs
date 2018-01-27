using UnityEngine;

public class ScoreBoard : MonoBehaviour
{	

    [SerializeField]
    private int score;

    [SerializeField]
    private int completion;
	
    [SerializeField]
    private int rage;

    [SerializeField]
    private int rageMax;

    [SerializeField]
    private ScoreBoardView view = new ScoreBoardView();

    public void AddPassengerCompletion()
    {
        completion++;
        view.UpdateScore(completion);
    }

    public void AddRage(int value)
    {
        rage += value;
        rage = Mathf.Clamp(rage, 0, rageMax);
    }

    public void AdjustRageMax(int value)
    {
        rageMax = value;
    }

    public void UpdateDepot(string value)
    {
        view.UpdateDepot(value);
    }

}