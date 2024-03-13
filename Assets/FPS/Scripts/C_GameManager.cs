using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool g_GameStartFlag = false;
    public static int g_TotalKills = 0;
    public static float g_PlayerLife = 1;
    public Scrollbar g_LifeBar;
    public Text g_ScoreTxt;
    void Start()
    {
        g_GameStartFlag = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        g_LifeBar.size = g_PlayerLife;
        g_ScoreTxt.text = ""+g_TotalKills;

        if(g_PlayerLife <= 0)
        {
            g_GameStartFlag = false;
        }
    }

    
}
