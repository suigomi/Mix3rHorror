using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnWarpGate : MonoBehaviour
{
    //湖を中心とする円の内部にSpawn
    private GameObject lake;
    private Vector3 center;　//円の中心
    public float radius = 130f;

    //下方向にRayを飛ばして、下が地面か湖かを判断
    public float rayOriginY = 30f;　//Rayを飛ばす地点
    public float rayMaxDistance = 40f;　//Rayの飛程

    // Start is called before the first frame update
    void Start()
    {
        lake = GameObject.Find("WaterProDaytime");
        center = lake.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        //テスト用----------
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            MoveGate();
        }
        //--------------------
    }

    public void MoveGate()
    {
        float spawnX = center.x + radius * Random.insideUnitCircle.x;
        float spawnZ = center.z + radius * Random.insideUnitCircle.y;
        if(540f <= spawnX && spawnX <= 595f && 615f <= spawnZ && spawnZ <= 655f)
        {
            MoveGate();
            
        }
        else
        {
            Ray ray = new Ray(new Vector3(spawnX, rayOriginY, spawnZ), Vector3.down); //Rayを作る
            RaycastHit hit; //Rayが当たったオブジェクト
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Terrain"))
                {
                    float spawnY = hit.point.y + GetComponent<BoxCollider>().size.y / 2;
                    transform.position = new Vector3(spawnX, spawnY, spawnZ);
                    transform.eulerAngles = new Vector3(transform.rotation.x, Random.Range(0, 360), transform.rotation.z);
                }
                else //地面以外に当たったら再帰
                {
                    MoveGate();
                }
            }
            else //当たらなかったら再帰
            {
                MoveGate();
            }

        }
    }
}