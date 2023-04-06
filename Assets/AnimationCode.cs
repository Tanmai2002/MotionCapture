using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class AnimationCode : MonoBehaviour
{

    public GameObject[] Body;

    string tempStr = "";
    int numToSendToPython = 0;
   
    UdpSocket udpSocket;

    public void QuitApp()
    {
        print("Quitting");
        Application.Quit();
    }

    public void UpdatePythonRcvdText(string str)
    {
        tempStr = str;
    }

    public void SendToPython()
    {
        udpSocket.SendData("Sent From Unity: " + numToSendToPython.ToString());
        numToSendToPython++;
        // sendToPythonText.text = "Send Number: " + numToSendToPython.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        // lines = System.IO.File.ReadLines("Assets/AnimationFile.txt").ToList();
        udpSocket = FindObjectOfType<UdpSocket>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Id is");
        // Transform t=animator.GetBoneTransform(HumanBodyBones.Head);
        Debug.Log(tempStr);
        
        string[] points = new string[120];
        if(tempStr.Length > 0){
            points = tempStr.Split(',');
            // Debug.Log(points.Length);
        }
        

        for(int i = 0; i < 33; i++)
        {
            float x = float.Parse(points[i*3])/100;
            float y = float.Parse(points[1 + i*3])/100;
            float z = 10;

            Body[i].transform.position = new Vector3(x, y, z);
           
            
        }


        
        Thread.Sleep(2);
    }
}
