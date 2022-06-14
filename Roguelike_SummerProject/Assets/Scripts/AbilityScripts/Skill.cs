using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SkillTree;

public class Skill : MonoBehaviour
{
    public int id;
    public Text titleText;
    public Text descText;

    public int[] connectedSkills;

    public void UpdateUI(){
        titleText.text = skilltree.skillLevels[id] + "/" + skilltree.skillCaps[id] + "\n" + skilltree.skillNames[id];
        descText.text = skilltree.skillDescs[id] + "\nCost: " + skilltree.skillPoints + "/1 SP";

        //yellow if we have maxed it, green if we can afford it, otherwise it stays its base color
        GetComponent<Image>().color = skilltree.skillLevels[id] >= skilltree.skillCaps[id] ? Color.yellow : skilltree.skillPoints > 0 ? Color.green : Color.white;

        //attachs skills together
        foreach (var connectedSkill in connectedSkills)
        {
            skilltree.skillList[connectedSkill].gameObject.SetActive(skilltree.skillLevels[id] > 0);
            skilltree.connectorList[connectedSkill].SetActive(skilltree.skillLevels[id] > 0);
        }
    }
    public void Buy(){
        if(skilltree.skillPoints < 1 || skilltree.skillLevels[id] >= skilltree.skillCaps[id]){
            return;
        }
        skilltree.skillPoints -= 1;
        skilltree.skillLevels[id]++;
        skilltree.UpdateAllSkillUI();
    }
}
