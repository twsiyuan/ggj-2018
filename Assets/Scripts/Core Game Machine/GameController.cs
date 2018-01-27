using UnityEngine;

public class GameController : MonoBehaviour
{
    private IGameLoop _gameLoop;
    private ISensorListener _sensorListener;

    [SerializeField]
    private AnimateManager _animateMgr;
    [SerializeField]
    private GameMachine_Prototype _gameMachine;

    void Start() {
        _gameLoop = new GameLoop();
        _sensorListener = new SensorListener(_gameMachine);

        _gameLoop.Initial(_sensorListener, _animateMgr);

        StartCoroutine(_gameLoop.MainLoop());
    }
}