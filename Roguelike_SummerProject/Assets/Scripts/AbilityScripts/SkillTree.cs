using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skilltree;
    private void Awake() {
        skilltree = this;
    }

    public int[] skillLevels;
    public int[] skillCaps;
    public string[] skillNames;
    public string[] skillDescs;

    public List<Skill> skillList;
    public GameObject skillHolder;

    public List<GameObject> connectorList;
    public GameObject connectorHolder;

    public int skillPoints;

    private void Start() {
        skillPoints = 20;
        skillLevels = new int[6]; //6 skills and their default levels set to zero
        skillCaps = new int[] {1,5,5,2,10,10}; //skill caps per level in order
        skillNames = new[] {"skill ONE", "skill TWO", "skill THREE", "skill FOUR", "skill FIVE", "skill SIX"};
        skillDescs = new[]
        {
            "desc ONE",
            "desc TWO",
            "desc THREE",
            "desc FOUR",
            "desc FIVE",
            "desc SIX",
        };

        //adds skills/connectors to the list of skills/connectors (make sure the skills are children of something)
        foreach (var skill in skillHolder.GetComponentsInChildren<Skill>()){
            skillList.Add(skill);
        }
        foreach (var connector in connectorHolder.GetComponentsInChildren<RectTransform>()){
            connectorList.Add(connector.gameObject);
        }
        
        //sets IDs for skills
        for (int i = 0; i < skillList.Count; i++)
        {
           skillList[i].id = i;
        }

        //connecting the skills (need to have skill 0 to get skills 1-3)
        skillList[0].connectedSkills = new[] {1,2,3};
        skillList[2].connectedSkills = new[] {4,5};

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI(){
        foreach (var skill in skillList)
        {
            skill.UpdateUI();
        }
    }
}
