using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    // Public Fields
    public float Health { get; private set; } = 100;
    public float Stamina { get; private set; } = 100;



    public bool isAttacking { get; set; } // isAttacking
    public bool isRunning { get; set; } // isRunning



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
