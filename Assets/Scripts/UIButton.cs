using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum E_Type
{
    One,
    Two,
    There


}

public class UIButton : MonoBehaviour
{

    void _On_Click22()
    {
        Debug.Log("���� Ŭ��22");

    }

    // string, int, float, bool, GameObject, enum
    public void _On_Click(int p_msg )
    {
        Debug.Log( $"���� Ŭ�� : {p_msg}");
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
