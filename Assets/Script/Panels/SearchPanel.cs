using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchPanel : MonoBehaviour,IPanel
{
    public InputField caseNumberInput;
    public SelectPanel selecpanel;


   
    public void ProcessInfo()
    {
        //download list of all object inside s3 storage 

        AWSManager.Instance.Getlist(caseNumberInput.text, () =>
        {
            selecpanel.gameObject.SetActive(true);
        });
       
        //comapre case number input
        //download that object
    }
}
