using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client_Info_Panel : MonoBehaviour, IPanel
{
    public Text caseNumber;
    
    public InputField matterField;
    public Maps_Panel mapsPanel;

    public void OnEnable()
    {
        caseNumber.text = "CASE NUMBER : " + UIManager.Instance.activeCase.caseID;
    }

    public void ProcessInfo()
    {
        if(string.IsNullOrEmpty(matterField.text))
        {
            Debug.Log("Matter Field Empty ,Cant Continue");
        }
        else
        {
            UIManager.Instance.activeCase.matter = matterField.text;
            mapsPanel.gameObject.SetActive(true);
        }
    }
}
