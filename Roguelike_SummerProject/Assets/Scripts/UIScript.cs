using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject skillTreePanel;
    public void OpenSkillTree(){
        skillTreePanel.SetActive(true);
    }
    public void CloseSkillTree(){
        skillTreePanel.SetActive(false);
    }
}
