using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skilltree;
    private void Awake() {
        skilltree = this;
    }

    public List<AbilityBaseClass> abilities;

}
