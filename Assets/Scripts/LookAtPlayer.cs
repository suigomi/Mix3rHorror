using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    public GameObject player;

    [Tooltip("回転させるSpeed")]
    [SerializeField] private float speed = 0.1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ターゲット方向のベクトル
        Vector3 relativePos = player.transform.position - this.transform.position;

        // 方向を、回転情報に変換
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        // 回転させる
        transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);
    }

}
