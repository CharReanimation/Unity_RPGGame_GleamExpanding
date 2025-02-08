using UnityEngine;

public class PlayerAnimatorScriptComponentHelper
{
    public static void GetPlayerAnimatorScriptComponents(GameObject obj, /*out Animator animator, */out PlayerAnimatorManager animatorManager, out PlayerProperties properties, out PlayerMove move)
    {
        // animator = obj.GetComponent<Animator>();
        animatorManager = obj.GetComponent<PlayerAnimatorManager>();
        properties = obj.GetComponent<PlayerProperties>();
        move = obj.GetComponent<PlayerMove>();


        // Debug Log
        /*if(animator == null)
        {
            Debug.LogError($"{obj.name}: Animator is not assigned!");
        }*/

        if (animatorManager == null)
        {
            Debug.LogError($"{obj.name}: PlayerAnimatorManager is not assigned!");
        }

        if (properties == null)
        {
            Debug.LogError($"{obj.name}: Player Properties is not assigned!");
        }

        if (move == null)
        {
            Debug.LogError($"{obj.name}: PlayerMove is missing!");
        }
    }
}
