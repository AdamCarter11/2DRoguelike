using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : AbilityBaseClass
{
    public float dashVelocity;

    public override void Activate(GameObject parent)
    {
        //activate ability
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();    //can use this to access public vars on playerMovement script
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();

        rb.velocity = rb.velocity * dashVelocity;
        Debug.Log("activatedDash");
    }
    public override void BeginCoolDown(GameObject parent)
    {
        //reset back to whatever you want (in this case, reset speed)
        Debug.Log("dash cooldown began");
    }
}
