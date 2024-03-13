using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_HitMarker : MonoBehaviour
{
    public float g_lifeSpan = 0 ;
    float g_StartTime,ElapsedTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        g_StartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime = Time.time - g_StartTime;
        if(ElapsedTime > g_lifeSpan)
        {
            Destroy(this.gameObject);
        }
    }
}
