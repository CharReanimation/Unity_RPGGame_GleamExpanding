using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Private Fields
    [Header("Animator Settings")]

    private PlayerMove playerMove;
    private bool isAttacking;



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
        // Get Components in current GameOBJ

        playerMove = GetComponent<PlayerMove>();

        if (playerMove == null)
        {
            Debug.LogError("PlayerMove or component is missing!");
        }
    }




    // Attack Testing

}
