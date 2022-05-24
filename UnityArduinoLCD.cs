using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnityArduinoLCD : MonoBehaviour
{
    AndroidJavaClass unityPlayer=null;
    AndroidJavaObject activity=null;
    private Thread lcdThread;

    void Start()
    {
        try
        {
            unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            if (unityPlayer != null)
            {
                activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception writing to lcd from unity:" + e.Message);
        }
        DirectClearLCD();
        DirectWriteLCD("Line 1", "Line 2");
        lcdThread = new Thread(ThreadExecution) { IsBackground = true, Priority = System.Threading.ThreadPriority.Normal, Name = "LCDThread" };
        lcdThread.Start();
    }
	
	private bool running = true;
	void OnApplicationQuit()
    {
        running = false;
    }

	void Update()
    {
    }
	
    private string lastLine1 = null;
    private string lastLine2 = null;
    private void ThreadExecution()
    {
        AndroidJNI.AttachCurrentThread();
        while (running)
        {
            if (LCDLine1 != null && LCDLine2 != null && (!LCDLine1.Equals(lastLine1) || !LCDLine2.Equals(lastLine2)))
            {
                DirectWriteLCD(LCDLine1, LCDLine2);
            }
            Thread.Sleep(1000);
        }
    }

    private string LCDLine1=null, LCDLine2= null;
    public void WriteLCD(string line1, string line2)
    {
        LCDLine1 = line1;
        LCDLine2 = line2;
    }

    private int MAX_LINE_LENGTH = 16; //16x2 LCD
    private string PrepareLCDLine(string line)
    {
        if (line != null)
        {
            if (line.Length > MAX_LINE_LENGTH)
            {
                line = line.Substring(0, 16);
            }
            else if (line.Length < MAX_LINE_LENGTH)
            {
                line = line.PadRight(MAX_LINE_LENGTH - line.Length);
            }
        }
        else
        {
            line = "".PadRight(16);
        }
        return line;
    }
    public void DirectWriteLCD(string line1, string line2)
    {
        
        try
        {
            if (unityPlayer != null)
            {
                if (activity != null)
                {
                    activity.Call("WriteLCD", new object[] { PrepareLCDLine(line1), PrepareLCDLine(line2) });
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception writing to lcd from unity:" + e.Message);
        }
        
    }

    private void DirectClearLCD()
    {
        try
        {
            if (unityPlayer != null)
            {
                if (activity != null)
                {
                    activity.Call("ClearLCD", new object[] { });
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception writing to lcd from unity:" + e.Message);
        }
    }
}
