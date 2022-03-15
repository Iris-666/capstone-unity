using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GPSLocation : MonoBehaviour
{
    public Text GPSStatus;
    public Text latitudeValue;
    public Text longtitudeValue;
    public Text altitudeValue;
    public Text horizontalAccurancyValue;
    public Text timestampValue;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLoc());
    }

    IEnumerator GPSLoc()
    {
        //check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        //start service before querying location
        Input.location.Start();

        //wait until service initialize
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //service didn't init in 20 sec
        if (maxWait < 1)
        {
            GPSStatus.text = "Time Out";
            yield break;
        }

        //connection failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Unable to determine device location";

            yield break;
        }
        else
        {
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }//end of GPSLoc

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            GPSStatus.text = "Running";
            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longtitudeValue.text = Input.location.lastData.longitude.ToString();
            altitudeValue.text = Input.location.lastData.altitude.ToString();
            horizontalAccurancyValue.text = Input.location.lastData.horizontalAccuracy.ToString();
            timestampValue.text = Input.location.lastData.timestamp.ToString();
            Debug.Log("time stamp: " + timestampValue.text);
            //Access granted to GPS value and it has been init
        }
        else
        {
            GPSStatus.text = "Stop";

            //servie is stopped
        }

    }//end of UpdateGPSData

}
