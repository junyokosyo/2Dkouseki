using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIme : MonoBehaviour
{
    private bool _timestop=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")&&_timestop==false)
        {
            _timestop = true;
            Time.timeScale = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Q)&&_timestop==true)
        {
            _timestop = false;
            Time.timeScale = 1f;
        }
    }
}
