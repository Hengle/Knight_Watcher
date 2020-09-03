using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Maps_Panel : MonoBehaviour, IPanel
{
    public RawImage mapImage;
    public InputField noteField;
    public string url;

    public string first;
    public string last;
    public string apiKey;
    public string x;
    public string y;
    private string imgsavepath;
   
    // public Text locTextProperty;
    public Maps_Panel mapsPanel;

    public Text caseNumber;
    public Client_Info_Panel ClientPanel;

 


    public void OnEnable()
    {
        x = UIManager.Instance.activeCase.Lat;
        Debug.Log("X is : " + x);
        y = UIManager.Instance.activeCase.Lon;
        Debug.Log("Y is : " + y);
        caseNumber.text = "CASE NUMBER : " + UIManager.Instance.activeCase.caseID;
        StartCoroutine(Map());
        
    }
    
   
    IEnumerator Map()
    {
        url = first + y + "," + x + last + apiKey;
        using (WWW www = new WWW(url))
        {
            yield return www;
            if (www.error != null)
            {
                Debug.LogError("Map Error : " + www.error);
            }
            Texture2D texture = www.texture;
            mapImage.texture = texture;
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "maptexture.png", bytes);

            if (File.Exists(Application.persistentDataPath + "maptexture.png"))
            {
                byte[] byteArray = File.ReadAllBytes(Application.persistentDataPath + "maptexture.png");
                UIManager.Instance.activeCase.staticmaplocation = byteArray;
            }
        }
       


    }

   

    public void ProcessInfo()
    {
        if (string.IsNullOrEmpty(noteField.text))
        {
            Debug.Log("Matter Field Empty ,Cant Continue");
        }
        else
        {
            UIManager.Instance.activeCase.loactionNote = noteField.text;
            ClientPanel.gameObject.SetActive(true);
        }
    }

    



}
