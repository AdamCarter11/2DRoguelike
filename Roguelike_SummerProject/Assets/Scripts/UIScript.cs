using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject skillTreePanel;
    public void OpenSkillTree(){
        skillTreePanel.SetActive(true);
    }
    public void CloseSkillTree(){
        skillTreePanel.SetActive(false);
    }
    
    public void LoadTestScene(){
        SceneManager.LoadScene("TestScene2");
    }
    public void LoadSampleScene(){
        SceneManager.LoadScene("SampleScene");
    }
}
