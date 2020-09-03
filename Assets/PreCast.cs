using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreCast : MonoBehaviour
{
    public Text Latitude;
    public Text Longitude;

  

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Latitude.text = GPS.Instance.Latitude.ToString();
        Longitude.text = GPS.Instance.Longitude.ToString();
    }
}
