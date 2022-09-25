using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TLGBTN : MonoBehaviour
{



    public void _On_ChangeSlider(float p_val )
    {
        Debug.Log($"슬라이더 : ");
    }


    public void _On_SetChange(bool p_is)
    {
        Debug.Log($"토글 클릭 L {p_is}");


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
