using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System;
using Amazon.S3.Util;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class AWSManager : MonoBehaviour
{
    private static AWSManager _instance;
    public static AWSManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("AWS MANAGER IS NULL");
            }
            return _instance;
        }
    }



    public string S3Region = RegionEndpoint.USEast1.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }

    private AmazonS3Client _s3Client;
    public AmazonS3Client S3Client
    {
        get
        {
            if(_s3Client == null)
            {
                _s3Client = new AmazonS3Client(new CognitoAWSCredentials(
                "us-east-1:261aa659-23a7-4f3c-ab06-529861734e63", // Identity Pool ID
                 RegionEndpoint.USEast1 // Region
                 ), _S3Region);
            }
            return _s3Client;
        }
        
    }

    private void Awake()

    {
        _instance = this;

        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        // ResultText is a label used for displaying status information
        S3Client.ListBucketsAsync(new ListBucketsRequest(), (responseObject) =>
        {
           
            if (responseObject.Exception == null)
            {
                
                responseObject.Response.Buckets.ForEach((s3b) =>
                {
                    Debug.Log("Bucket Name : " + s3b.BucketName);
                  
                });
            }
            else
            {
                Debug.Log("AWS ERROR" + responseObject.Exception);
            }
        });
    }
    public void UploadtoS3(string path, string caseID)
    {

        FileStream stream = new FileStream(path,FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
        PostObjectRequest request = new PostObjectRequest()
        {
            Bucket = "aerocasefiles",
            Key = "case#" + caseID,
            InputStream = stream,
            CannedACL = S3CannedACL.Private


        };
        S3Client.PostObjectAsync(request, (fardinObj) =>
         {
             if (fardinObj.Exception == null)
             {
                 Debug.Log("Sucessfully Posted");
             }
             else
             {
                 Debug.Log("Upload Failed : " + fardinObj.Exception);
             }
         });
    }
    public void Getlist(string casenumber,Action onComplete = null)
    {
        string target = "case#" + casenumber;

        Debug.Log("AWSManager::GetList()");
        var request = new ListObjectsRequest()
        {
            BucketName = "aerocasefiles"
        };
        S3Client.ListObjectsAsync(request, (responceObject) =>
         {
         if (responceObject.Exception == null)
         {
              bool casefound = responceObject.Response.S3Objects.Any(fardinahosan => fardinahosan.Key == target);

                 if (casefound==true)
                 {
                     Debug.Log("Case Found");

                     //If we Successfully get the file it responseobj can hold the data
                     S3Client.GetObjectAsync("aerocasefiles", target, (responseobj) =>
                      {
                          if(responseobj.Response.ResponseStream != null)
                          {
                              byte[] data = null;
                              using (StreamReader reader = new StreamReader(responseobj.Response.ResponseStream))
                              {
                                  using (MemoryStream memory = new MemoryStream())
                                  {
                                      var buffer = new byte[512];
                                      var bytesRead = default(int);

                                      while((bytesRead = reader.BaseStream.Read(buffer,0,buffer.Length)) > 0)
                                      {
                                          memory.Write(buffer, 0, bytesRead);
                                      }
                                      data = memory.ToArray();
                                  }
                              }
                              //Convert Bytes to case object
                              using (MemoryStream memory = new MemoryStream(data))
                              {
                                  BinaryFormatter bf = new BinaryFormatter();
                                  Case downloadedcase = (Case)bf.Deserialize(memory);
                                  Debug.Log("Downloaded Case Name : " + downloadedcase.matter);
                                  UIManager.Instance.activeCase = downloadedcase;
                                  if(onComplete != null)
                                  {
                                      onComplete();
                                  }
                                  
                              }
                          }


                      });
                 }
                 else
                 {
                     Debug.Log("Case Not Found : " +casefound);
                 }
             }
         else
             {
                 Debug.Log("Error Getting File : " + responceObject.Exception);
             }
                 
         });
    }
}
