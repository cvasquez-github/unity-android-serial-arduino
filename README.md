# unity-android-serial-arduino
Connect an Arduino board to your Android device and send commands through the serial port from your Unity application/game.


# Arduino
On the Arduino side, you need to create a small application that reads the serial port incoming commands and reacts accordingly.
Take a look at this example for instance, that receives commands from the serial port and writes to a LCD 16x2 display: 
https://github.com/cvasquez-github/arduino-serial-to-i2c-lcd

# Java Android USB Serial Library
I used the Android USB Serial Library (usb-serial-for-android-3.4.4.jar) to be able to use the Android USB serial port in my application:
https://github.com/mik3y/usb-serial-for-android

# Java "Middleware"
You need to write some Java code to be able to communicate your Unity project with the Arduino through Android.
In this example I created a custom Unity Activity to expose the necesary lower level methods to Unity, take a look at the MyUnityPlayerActivity.java  file for reference.
https://github.com/cvasquez-github/unity-android-serial-arduino/blob/main/MyUnityPlayerActivity.java

Make sure you update the activity name in your Android Manifest XML file.

# Unity
Then you need to be able to call the Java class methods from Unity.
Take a look at the UnityArduinoLCD.cs file, that is the one that talks to the Java code.
- You first need to get the Unity Player Java Class: AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
- Then get the current activity Java Object out of it: AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
- Finally call the Java method on your current Activity: activity.Call("WriteLCD", new object[] { "line1", "line2" });  

I used this on my Android-based 3D virtual pinball to write the score of the game to a 16x2 LCD display, through an Arduino UNO board, using an nVidia Shield Google TV device on the Android side.
