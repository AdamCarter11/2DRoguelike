using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public AbilityBaseClass ability;
    private float coolDownTime;
    private float activeTime;

    enum AbilityState{
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    public KeyCode key; //used so we can set key in inspector
    // Update is called once per frame
    void Update()
    {
        switch(state){
           case AbilityState.ready:
            if(Input.GetKeyDown(key)){
                //activate ability
                ability.Activate(gameObject);
                state = AbilityState.active;
                activeTime = ability.activeTime;
            }
            break;
           case AbilityState.active:
            if(activeTime > 0){
                activeTime-= Time.deltaTime;
            }
            else{
                ability.BeginCoolDown(gameObject);
                state = AbilityState.cooldown;
                coolDownTime = ability.coolDownTime;
            }
           break;
           case AbilityState.cooldown:
            if(coolDownTime > 0){
                coolDownTime-= Time.deltaTime;
            }
            else{
                state = AbilityState.ready;
            }
           break; 
        }

        
    }
}
