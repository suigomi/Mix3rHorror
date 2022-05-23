using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ChangeTime : MonoBehaviour
{
    private GameManager gameManager;
    private TimeManager timeManager;
    private SpawnWarpGate spawnWarpGate;

    [Tooltip("どのくらいSan値を下げるか")]
    public int substructValue;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        timeManager = GameObject.Find("Time Manager").GetComponent<TimeManager>();
        spawnWarpGate = GetComponent<SpawnWarpGate>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin")
        {
            gameManager.SubSanValue(substructValue);
            timeManager.TimeChange(true);
            spawnWarpGate.MoveGate();
        }
    }

}
