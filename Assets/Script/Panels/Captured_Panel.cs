using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Captured_Panel : MonoBehaviour, IPanel
{
    public RawImage takenPhoto;
    public InputField photoNote;
    public Text CaseNumberUp;
    public GameObject Overview_Panel;
    private string imgPath;


    public void OnEnable()
    {
        CaseNumberUp.text = "CASE NUMBER : " + UIManager.Instance.activeCase.caseID;
    }
  public  void  TakepictureDown()
    {
        TakePicture(512);
    }

    public void ProcessInfo()
    {

        byte[] imageData = null ;

        if (string.IsNullOrEmpty(imgPath)==false)
        {
            //Create 2d texture + Apply The Camera Image Texture from image path
            Texture2D img = NativeCamera.LoadImageAtPath(imgPath, 512, false);

            //Encode to PNG
            imageData = img.EncodeToPNG();
        }


        UIManager.Instance.activeCase.photoTaken = imageData;


        UIManager.Instance.activeCase.photoNote = photoNote.text;
        Overview_Panel.SetActive(true);
    }

    private void TakePicture(int maxSize)
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create a Texture2D from the captured image
                Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize,false);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                takenPhoto.texture = texture;
                takenPhoto.gameObject.SetActive(true);
                imgPath = path;

               
            }
        }, maxSize);

        Debug.Log("Permission result: " + permission);
    }
}
