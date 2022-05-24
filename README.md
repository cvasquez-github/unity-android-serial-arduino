# unity-android-serial-arduino
Connect an Arduino board to your Android device and send commands through the serial port from your Unity application/game.


# Arduino
On the Arduino side, you need to create a small application that reads the serial port incoming commands and reacts accordingly.
Take a look at this example for instance, that receives commands from the serial port and writes an LCD: https://github.com/cvasquez-github/arduino-serial-to-i2c-lcd

# Java Android USB Serial Library
I used the following Android USB Serial Library to be able to use the Android USB Serial port in my application.
https://github.com/mik3y/usb-serial-for-android

# Java "Middleware"
You need to write some Java code to be able to community your Unity project with the Arduino.
In this example I create a custom Unity Activity to expose the necesary lower level methods to Unity.

# Unity
Then you need to be able to call the Java class methods from Unity.


