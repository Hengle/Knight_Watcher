using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("The UIManager is NULL"); ;
            }
            return _instance;
        }
    }
    public Case activeCase;
    public Maps_Panel Mapspanel;
    public GameObject borderpanel;
    private void Awake()
    {
        _instance = this;
    }
    public void CreateNewCase()
    {
        activeCase = new Case();
        int RandomCaseId = Random.Range(0, 10000);
         activeCase.caseID = "" + RandomCaseId;
        Mapspanel.gameObject.SetActive(true);
        borderpanel.SetActive(true);
    }

    public void OnEnable()
    {
        CreateNewCase();
    }

    public void SubmitButton()
    {
        //create a case to save
        //populate the case data
        //open a data stream to turn that objevt (case ) into file
        //begin aws

        Case awscase = new Case();
        awscase.caseID = activeCase.caseID;
        awscase.matter = activeCase.matter;
        awscase.date = activeCase.date;
        awscase.loactionNote = activeCase.loactionNote;
        awscase.staticmaplocation = activeCase.staticmaplocation;
        awscase.photoTaken = activeCase.photoTaken;
        awscase.photoNote = activeCase.photoNote;
        awscase.Lat = activeCase.Lat;
        awscase.Lon = activeCase.Lon;

        string filepath = Application.persistentDataPath + "/case#" + awscase.caseID + ".dat";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filepath);
        bf.Serialize(file, awscase);
        file.Close();
        Debug.Log("File Path : " + Application.persistentDataPath);

        //Send to AWS
        AWSManager.Instance.UploadtoS3(filepath,awscase.caseID);
    }
}
