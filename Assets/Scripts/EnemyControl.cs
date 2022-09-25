using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControl : MonoBehaviour
{

    bool m_FallDown = false;
    void FallDownFN()
    {
        Debug.Log("¶³¾îÁ®¶ó");
        m_FallDown = true;
    }

    public void DieEventFN( string p_msg )
    {
        Debug.Log( $"¶³¾îÁ®¶ó2 : {p_msg}" );
        m_FallDown = true;
    }

    public void SetDamage(int p_dmg)
    {
        if (HP <= 0)
            return;

        HP -= p_dmg;

        if (HP <= 0
            && !isdie)
        {
            isdie = true;
            LinkAnimator.SetTrigger("Die");

            Agent.isStopped = true;
            Agent.enabled = false;

            //this.Invoke( "FallDownFN", 6f );
        }
    }

    private void Awake()
    {
        LinkAnimator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.speed = MoveSpeed;
    }


    public bool ISMove = false;
    void Start()
    {
        
    }

    public int HP = 10;
    public Animator LinkAnimator = null;

    public NavMeshAgent Agent = null;


    public Transform Target = null;
    public float MoveSpeed = 1f;

    void UpdateMove()
    {
        Vector3 direction = Target.position - transform.position;
        //direction = direction.normalized;

        float dist = direction.magnitude;


        direction.Normalize();

        //direction = direction * (MoveSpeed * Time.deltaTime);
        direction = MoveSpeed * Time.deltaTime * direction;

        if (direction.magnitude <= dist)
        {
            transform.position += direction;
        }
        else
        {
            transform.position = Target.position;
        }


    }

    void UpdateAgent()
    {
        if (Agent.enabled == false)
            return;


        if( Agent.velocity.sqrMagnitude <= (0.1f * 0.1f) )
        {
            ISMove = false;
        }
        else
            ISMove = true;

        Agent.SetDestination(Target.position);

        

    }

    public float FallDownSpeed = 1f;
    bool isdie = false;
    void Update()
    {
        if (m_FallDown)
        {
            //Vector3 temppos = transform.position;
            //temppos.y -= Time.deltaTime * FallDownSpeed;
            //transform.position = temppos;

            transform.Translate(0f, -Time.deltaTime * FallDownSpeed, 0f);

            if( transform.position.y <= -4f )
            {
                GameObject.Destroy(this.gameObject);
            }
        }


        if (isdie)
            return;


        UpdateAgent();
        LinkAnimator.SetBool("Move", ISMove);




    }
}
