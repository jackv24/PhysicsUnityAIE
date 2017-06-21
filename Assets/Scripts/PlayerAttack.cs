using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount = 10;

    public LayerMask raycastLayer;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1000f, raycastLayer))
            {
                CharacterStats stats = hit.collider.GetComponent<CharacterStats>();

                if(stats)
                {
                    stats.RemoveHealth(damageAmount);
                }
            }
        }
    }
}
