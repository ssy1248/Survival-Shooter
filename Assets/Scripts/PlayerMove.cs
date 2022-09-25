using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    void Start()
    {
        m_TargetOffset = TargetObj.position - transform.position;

        ViewCam = Camera.main;// GameObject.FindObjectOfType<Camera>();
        m_Body = GetComponent<Rigidbody>();


        SetHideLineRender();
    }

    
    public float MoveSpeed = 1f;
    public Transform TargetObj = null;

    public float TargetWeightVal = 0.5f;


    [SerializeField]
    Vector3 m_TargetOffset;


    protected Rigidbody m_Body = null;


    private void OnTriggerEnter(Collider other)
    {
        
    }


    [Header("[확인용]")]
    [SerializeField]
    protected Vector2 m_MousePos;
    [SerializeField]
    protected Vector2 m_MouseCenterPos;
    [SerializeField]
    protected float m_Radian;

    public Camera ViewCam;

    void UpdateRotation()
    {
        m_MousePos = Input.mousePosition;
        //Debug.Log($"포즈 : {mousepos}");

        Vector2 tempvec = m_MousePos - m_MouseCenterPos;
        m_Radian = Mathf.Atan2(tempvec.y, tempvec.x);

        transform.rotation = Quaternion.Euler(0f
            , (-m_Radian * Mathf.Rad2Deg) + 90f
            , 0f);
    }


    void UpdateWorld2Screen()
    {
        m_MouseCenterPos = ViewCam.WorldToScreenPoint(transform.position);

        m_MousePos = Input.mousePosition;
        Vector2 tempvec = m_MousePos - m_MouseCenterPos;
        m_Radian = Mathf.Atan2(tempvec.y, tempvec.x);

        transform.rotation = Quaternion.Euler(0f
            , (-m_Radian * Mathf.Rad2Deg) + 90f
            , 0f);
    }


    public LineRenderer LLineRender;
    public Transform GunBarralEnd;
    public float Length = 10f;
    public GameObject HitParticle = null;

    public float LineDelaySec = 0.1f;
    float m_LinkeNextSec = 0f;

    void Shooting()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            Vector3 stpos = GunBarralEnd.position;
            Vector3 endpos = stpos +  GunBarralEnd.forward * Length;

            m_LinkeNextSec = Time.time + LineDelaySec;

            GunBarralEnd.gameObject.SetActive(true);
            LLineRender.gameObject.SetActive(true);

            RaycastHit hitinfo;
            if( Physics.Raycast(stpos, GunBarralEnd.forward, out hitinfo, Length) )
            {
                endpos = hitinfo.point;

                GameObject hitobj = GameObject.Instantiate(HitParticle);
                hitobj.transform.position = endpos;
                hitobj.transform.rotation = Quaternion.LookRotation(hitinfo.normal);

                hitobj.GetComponent<ParticleSystem>()?.Play();
                //ParticleSystem system = hitobj.GetComponent<ParticleSystem>();
                //if( system != null )
                //{
                //    system.Play();
                //}

                GameObject.Destroy(hitobj, 3f);

                //EnemyControl enemy = hitinfo.transform.GetComponent<EnemyControl>();
                //if(enemy != null)
                //{
                //    enemy.SetDamage(1);
                //}

                hitinfo.transform.GetComponent<EnemyControl>()?.SetDamage(1);
            }


            LLineRender.SetPosition(0, stpos);
            LLineRender.SetPosition(1, endpos);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_Body.velocity = Vector3.zero;
    }


    void SetHideLineRender()
    {
        if(m_LinkeNextSec <= Time.time )
        {
            GunBarralEnd.gameObject.SetActive(false);
            LLineRender.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        SetHideLineRender();
        Shooting();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 temppos = transform.position;
        temppos.x += x * MoveSpeed * Time.deltaTime;
        temppos.z += z * MoveSpeed * Time.deltaTime;
        transform.position = temppos;


        m_Body.velocity = Vector3.zero;


        //UpdateRotation();
        UpdateWorld2Screen();

    }

    private void LateUpdate()
    {
        //Vector3 currepos = TargetObj.position;
        //Vector3 targetpos = transform.position + m_TargetOffset;
        //Vector3 direction = (targetpos - currepos) * TargetWeightVal * Time.deltaTime;
        //// 카메라위치값
        //TargetObj.position += direction;


        //TargetObj.position = Vector3.Lerp(TargetObj.position
        //    , transform.position + m_TargetOffset
        //    , TargetWeightVal * Time.deltaTime);

    }



}
