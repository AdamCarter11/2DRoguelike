using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBaseClass : ScriptableObject
{
    public int skillCap; //amount of points you can put into the skill (passives will have multiple)
    public int skillLevel;  //actual level of skill

    public int id; //used as easy reference in code
    public string nameOfAbility;
    public float coolDownTime;
    public float activeTime;

    public int[] connectedSkills;

    public virtual void Activate(GameObject parent){}
    public virtual void BeginCoolDown(GameObject parent){}
}
