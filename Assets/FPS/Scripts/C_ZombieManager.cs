using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ZombieManager : MonoBehaviour
{
    public GameObject g_ZombiePrefab;
    public GameObject[] g_SpawnPoints;
    public GameObject g_ZombieTarget;
    int g_RandomIndex = 0;
    GameObject g_CloneObj;
    C_Zombie[] g_ZombieScriptArray;
    int g_TotalZombieCount = 0;
    float g_SpawnInterval,g_StartTime,g_Elapsedtime = 0;
    // Start is called before the first frame update
    void Start()
    {
        g_SpawnInterval = 3;
        g_StartTime =Time.time;
        m_InitZombies();
    }

    // Update is called once per frame
    void Update()
    {
        m_SpawnZombies();
        
    }

    void m_InitZombies()
    {
        g_TotalZombieCount = 20;
        g_ZombieScriptArray = new C_Zombie[g_TotalZombieCount];
        for(int i=0;i<g_TotalZombieCount;i++)
        {
            g_CloneObj = Instantiate(g_ZombiePrefab);
            g_CloneObj.SetActive(false);
            g_CloneObj.transform.parent =this.transform;
            g_ZombieScriptArray[i] = g_CloneObj.GetComponent<C_Zombie>();
        }

    }

    void m_SpawnZombies()
    {
        g_Elapsedtime = Time.time - g_StartTime;
        if(g_Elapsedtime >= g_SpawnInterval)
        {
            g_StartTime = Time.time;
            g_Elapsedtime = 0;
            for(int i=0;i<g_TotalZombieCount;i++)
            {
                if(!g_ZombieScriptArray[i].gameObject.activeInHierarchy)
                {
                    g_RandomIndex = Random.Range(0,g_SpawnPoints.Length);
                    g_ZombieScriptArray[i].transform.position = g_SpawnPoints[g_RandomIndex].transform.position;
                    g_ZombieScriptArray[i].gameObject.SetActive(true);
                    g_ZombieScriptArray[i].g_Target = g_ZombieTarget.transform;
                    return;
                }
                
            }
        }


    }
}
