using UnityEngine;

public class ScoreBoard : MonoBehaviour
{	

    [SerializeField]
    private int score;

    [SerializeField]
    private int completion;

    [SerializeField]
    private int waiting;
    
    [SerializeField]
    private int rage;

    [SerializeField]
    private int rageMax = 20;

    [SerializeField]
    private ScoreBoardView view = new ScoreBoardView();

    public void AddScore(int value)
    {
        var temp = score;
        score += value;
        view.UpdateScore(temp, score);
    }

    public void AddPassengerCompletion()
    {
        completion++;
        view.UpdatePassengerCompletion(completion);
    }

    public void AddRage(int value)
    {
        rage += value;
        rage = Mathf.Clamp(rage, 0, rageMax);


        view.UpdateRage((float)rage/rageMax);
    }

    public void AdjustRageMax(int value)
    {
        rageMax = value;
        view.UpdateRageMax(value);
    }

    public void UpdateDepot(string value)
    {
        view.UpdateDepot(value);
    }

    public void AddWaitingPassenger()
    {
        waiting++;
        view.UpdateWaitingPassenger(waiting);
    }

    public void RemoveWaitingPassenger()
    {
        waiting--;
        view.UpdateWaitingPassenger(waiting);
    }

}