using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPanel : MonoBehaviour, IPanel
{
    public Text information_ID;
    public Text information_matter;

    public void OnEnable()
    {
        information_ID.text = UIManager.Instance.activeCase.caseID;
        information_matter.text = UIManager.Instance.activeCase.matter;


    }

    public void ProcessInfo()
    {
       
    }
}
