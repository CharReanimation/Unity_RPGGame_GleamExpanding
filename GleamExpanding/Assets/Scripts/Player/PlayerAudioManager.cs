using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{    
    // Private Fields
    private PlayerSwordAnimator playerSwordAnimator;
    private AudioSource playerAudioSource;




    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        GetComponents();
    }


    // Update is called once per frame
    void Update()
    {

    }




    // Get Components
    private void GetComponents()
    {
        // Get Components
        playerSwordAnimator = GetComponent<PlayerSwordAnimator>();
        playerAudioSource = GetComponent<AudioSource>();
    }


}
