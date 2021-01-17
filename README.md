# Robot Simulator
A driving simulator for the VEX Robotics competition.

## Sidequest Simple Installation
1. Install Sidequest (free) using a guide like [this one](https://www.androidcentral.com/how-sideload-apps-oculus-quest).
2. Use the Sidequest desktop app to install the APK from the [Releases tab](https://github.com/yummypasta/VEXSimulator/releases).

## Simple Installation
1. Make sure you have Android Studio downloaded (including the `adb` CLI tool).
2. Make sure your Quest is in Developer Mode.
3. Download the latest release from the [Releases tab](https://github.com/yummypasta/VEXSimulator/releases).
4. Plug in your Quest
5. In your Terminal/Command Prompt/PowerShell, type `adb devices`. Once you accept the prompt on your Quest, type `adb install path/to/your/downloaded/apk/your-download-file-name.apk`
6. Your app is installed under the **Unknown Sources** section of your Quest app launch screen.

## Get Started with Development
1. Install the right version of Unity and get it set up with Android Studio.
2. As of 2021, the latest Blender and FBX robot CAD files are not available in the repository since they are too large. Please contact the developers for access to these files. **IMPORTANT!** _Before_ opening Unity, place the FBX file in the directory as `Vex Simulator VR/Assets/Models/Turbo/Turbo_export.fbx`.
2. To build it out to your device, open the **Build Settings** window, make sure the target platform is Android, and click **Build & Run**.

## License
All rights reserved. You may not use, sell, distribute, or modify this program without permission from the author.

---
Developed by Eric Yoon, 2020 | 
[yoonicode.com](yoonicode.com)

3D Models by Kensuke Shimojo. Thank you!
