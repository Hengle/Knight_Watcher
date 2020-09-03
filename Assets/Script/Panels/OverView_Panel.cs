using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OverView_Panel : MonoBehaviour, IPanel
{
    public Text caseNumber;
    public Text matterText;
    public Text dateText;
    public Text LocationNoteText;
    public Text captureNoteText;
    public RawImage capturedPhotoRaw;
    public RawImage loactionstaticmap;
    public Text o_Latt;
    public Text o_Lonn;


    public void OnEnable()
    {
        caseNumber.text = "CASE NUMBER : " + UIManager.Instance.activeCase.caseID;
        matterText.text = UIManager.Instance.activeCase.matter;
        dateText.text = DateTime.Today.ToString();
        LocationNoteText.text = UIManager.Instance.activeCase.loactionNote;
        captureNoteText.text = "Photo Note : \n " + UIManager.Instance.activeCase.photoNote;
        //Rebuild photo and Display it
        //convert bytes to png
        //convert texture2d to texture
        Texture2D reconstructedimage = new Texture2D(1, 1);
        reconstructedimage.LoadImage(UIManager.Instance.activeCase.photoTaken);
        capturedPhotoRaw.texture = (Texture)reconstructedimage;

        //Rebuild photo and Display it
        //convert bytes to png
        //convert texture2d to texture

        Texture2D reconstructedmapimage = new Texture2D(1, 1);
        reconstructedmapimage.LoadImage(UIManager.Instance.activeCase.staticmaplocation);
        loactionstaticmap.texture = (Texture)reconstructedmapimage;

        o_Latt.text = UIManager.Instance.activeCase.Lat;
        o_Lonn.text = UIManager.Instance.activeCase.Lon;

    }
    public void ProcessInfo()
    {
       
    }
}
