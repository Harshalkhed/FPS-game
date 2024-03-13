using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Player : MonoBehaviour
{
    public float g_CharacterMoveSpeed,g_CharacterRotateSpeed=0;
    public ParticleSystem g_MuzzleFlash;
    public GameObject g_HitVFX;
    Vector3 g_MoveDirection,g_RotateDirection,g_CameraEulerAngle = Vector3.zero;
    CharacterController g_CharacterController;
    Camera g_PlayerCamera;

    bool g_IsMuzzleEffectActive = false;
    Ray g_Ray;
    RaycastHit g_RaycastHitInfo;
    Vector3 g_RayOrigin;
    GameObject g_TempObj;
    // Start is called before the first frame update
    void Start()
    {
        
        Application.targetFrameRate = 30;
        g_CharacterController = this.GetComponent<CharacterController>();
        g_PlayerCamera = this.transform.GetChild(0).GetComponent<Camera>();
        g_RayOrigin.x = 0.5f;
        g_RayOrigin.y = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(C_GameManager.g_GameStartFlag)
        {
            m_ManagePlayerMovement();
            m_ManagePlayerRotation();
            m_ManageWeapon();
            m_ManageWeaponVFX();
        }
        
    }

    void m_ManagePlayerMovement()
    {
        g_MoveDirection = this.transform.forward * Input.GetAxis("Vertical");
        g_MoveDirection += this.transform.right * Input.GetAxis("Horizontal");
        g_CharacterController.SimpleMove(g_MoveDirection * g_CharacterMoveSpeed);
    }
    void m_ManagePlayerRotation()
    {
        g_RotateDirection.x = -Input.GetAxis("Mouse Y") * g_CharacterRotateSpeed * Time.deltaTime;
        g_PlayerCamera.transform.Rotate(g_RotateDirection,Space.Self);
        
        g_CameraEulerAngle = Vector3.zero;
        g_CameraEulerAngle.x = g_PlayerCamera.transform.localEulerAngles.x;

        if(g_CameraEulerAngle.x > 200 && g_CameraEulerAngle.x < 300)
        {
            g_CameraEulerAngle.x = 300;
        }
        else if(g_CameraEulerAngle.x < 100 && g_CameraEulerAngle.x > 60)
        {
            g_CameraEulerAngle.x = 60;
        }

        g_PlayerCamera.transform.localEulerAngles = g_CameraEulerAngle;

        g_RotateDirection = Vector3.zero;
        g_RotateDirection.y = Input.GetAxis("Mouse X") * g_CharacterRotateSpeed * Time.deltaTime;
        this.transform.Rotate(g_RotateDirection,Space.World);
    }


    void m_ManageWeapon()
    {

        if(Input.GetMouseButtonDown(0))
        {
            g_Ray = g_PlayerCamera.ViewportPointToRay(g_RayOrigin);
            if(Physics.Raycast(g_Ray,out g_RaycastHitInfo ))
            {
               Instantiate(g_HitVFX,g_RaycastHitInfo.point,Quaternion.LookRotation(g_RaycastHitInfo.normal));
               if(g_RaycastHitInfo.transform.tag == "zombie")
               {
                   if(g_RaycastHitInfo.transform.GetComponent<C_Zombie>().g_isActive)
                   {
                       g_RaycastHitInfo.transform.GetComponent<C_Zombie>().m_KillZombie();
                         C_GameManager.g_TotalKills++;
                   }
                   
               }
            }

        }




        
    }

    void m_ManageWeaponVFX()
    {
        if(Input.GetMouseButtonDown(0) && !g_IsMuzzleEffectActive)
        {
            g_MuzzleFlash.Play();
            g_IsMuzzleEffectActive = true;
        }

        if(Input.GetMouseButtonUp(0) && g_IsMuzzleEffectActive)
        {
            g_MuzzleFlash.Stop();
            g_IsMuzzleEffectActive = false;
        }
    }

    
}
