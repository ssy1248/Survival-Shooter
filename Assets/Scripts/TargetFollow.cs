using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    public Transform TargetObj = null;
    public Vector3 OffsetPos;
    public float TargetWeightVal = 0.5f;

    [SerializeField]
    Vector3 m_TargetOffset;
    Quaternion m_TargetRot;

    
    void Start()
    {
        if(TargetObj != null)
        {
            m_TargetOffset = transform.position - TargetObj.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( TargetObj == null)
            return;

        transform.position = Vector3.Lerp(transform.position
            , m_TargetOffset + TargetObj.position + OffsetPos
            , TargetWeightVal );

    }
}
