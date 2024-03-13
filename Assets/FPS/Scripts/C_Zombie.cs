using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class C_Zombie : MonoBehaviour
{
    public NavMeshAgent g_NavMeshAgent;
    public Animator g_Animator;
    public Transform g_Target;
    float g_DistanceFromTarget = 0;
    Vector3 g_ZombieRotation = Vector3.zero;
    // Start is called before the first frame update
    public bool g_isActive = false;
    void OnEnable()
    {
        g_isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        m_SetDestinationForAI();
        m_ManageAnimation();
    }

    void m_SetDestinationForAI()
    {
        g_NavMeshAgent.destination = g_Target.position;
    }

    void m_ManageAnimation()
    {
        g_DistanceFromTarget = Vector3.Distance(this.transform.position,g_Target.position);
        if(g_DistanceFromTarget < 1.5f)
        {
            g_Animator.SetTrigger("attack");
            this.transform.forward = -g_Target.forward;
            if(g_isActive)
            {
                C_GameManager.g_PlayerLife -= 0.001f;
            }
            
        }
        else
        {
            g_Animator.SetTrigger("run");
        }
    }

    public void m_KillZombie()
    {
        StartCoroutine(e_KillSequence());
    }

    IEnumerator e_KillSequence()
    {
        g_isActive = false;
        g_Animator.SetTrigger("die");
        g_NavMeshAgent.isStopped = true;
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
