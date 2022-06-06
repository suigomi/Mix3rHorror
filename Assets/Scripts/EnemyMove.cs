using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    [Tooltip("追いかける対象")]
    private GameObject player;
    
    private NavMeshAgent navMeshAgent;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // NavMeshAgentを保持しておく
        navMeshAgent = GetComponent<NavMeshAgent>();


        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid) 
        {
            // プレイヤーを目指して進む
            navMeshAgent.destination = player.transform.position;
        }

        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (distance <= 2.5f)
        {
            gameManager.GameOver();
        }
    }

    
}