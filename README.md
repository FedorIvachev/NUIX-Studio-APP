# IoTStudio-client

**V0.6-aplha released*
...EDITING...
![](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/v0.6/Architecture.png)
Since the architecture of version 0.5 does not allow user-friendly control, we decided to change it.
Currenly, it can be represented by four layers:
1. **IoT real world devices.**
2. **openHAB server.** Provides:
	* Synchronization with IoT real world devices;
	* Storage of IoT data.
3. **PC - Unity Server.** It is connected by REST API to the openHAB server and is used for computations (virtual IoT devices interaction; virtual screens, lights, radio waves etc).
4. **VR Headset - Unity Client.** Even though we call it Client, it is actually connected to layer 2 - openHAB server. Provides API for interaction with virtual world.

The idea of running two instances of Unity at the same time is based on dividing the responsibilities between several project instances. In result, not powerful VR Headsets are responsible only for interaction with virtual IoT devices, while PC is responsible only for difficult computations (such as encoding/decoding, physics and other). In addition, *openHAB server - PC Unity Server* latency is so low compared to the latency of *openHAB server - VR Headset Unity Client* and *PC Unity Server - VR Headset Unity Client* that we don't need to care about having an additional layer.

The creation of items is performed on the openHAB server in a user-friendly way. The items are automatically synchronized with Unity Client and Server. I will further describe it in the next commit.

[You can also download the whole Project (Unity App + openHAB server).](https://github.com/VRSimulator/IoTStudio-WholeProject). Text creators to gain access.

...EDITING...

Please see the **[Setup](#setup) procedure**, cloning the repo is not necessary because the repository is only bare code. All stuff is inside [Releases](https://github.com/VRSimulator/IoThingsLab/releases) ~~~~

Text Fedor 费杰 on WeChat to know how to adapt the platform for your projects and also explain how it works.

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Required software and hardware](#required-software-and-hardware)
* [IoT VR Platform package](#iot-vr-platform-package)
* [Setup](#setup)
* [Known issues](#known-issues)
* [Contributing to the platform](#contributing-to-the-platform)

## General info

<img align="left" width="200" src="https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/20201030_173803.jpg">
<img align="left" width="200" src="https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/20201030_175023.jpg">

By using IoThingsLab platform researchers can connect real and virtual IoT devices, test new IoT devices inside VR environment (and don't even need to buy them!)

	
## Technologies
IoThingsLab uses the features of [Microsoft's Mixed Reality Toolkit](https://github.com/microsoft/MixedRealityToolkit-Unity#feature-areas), such as hand tracking and interaction techniques. 

Connection to real-world IoT devices is performed by REST API calls to [openHAB](https://www.openhab.org/download/) server, which runs either locally on user machine, or remotely on [myopenhab server](http://myopenhab.org/).
## Required software and hardware:
1. Unity Version 2019.4.13+
### Additional software & hardware:
1. Oculus Quest 1 or 2
2. OpenHAB (running on local or remote server)
3. IoT devices

## IoT VR Platform package
Available items (the list is frequently updated):

| [![Lamp](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/Lamp.png)]() [Light Item](Documentation/Things/Lamp.md) | [![Door](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/Door.png)]() [Door](Documentation/Things/Door.md) | [![WeightScaler](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/WeightScaler.png)]() [Weight Scaler](Documentation/Things/WeightScaler.md) | 
|:--- | :--- | :--- |
| A lamp thing with Location and Light items attached | A door with a door close/open sensor item attached | Weight Scaler item triggers according to the weight scaled on it |
| [![Camera](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/Camera.png)]() [Camera](Documentation/Things/Camera.md) | [![TV](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/TV.png)]() [TV](Documentation/Things/TV.md) | [![Vacuum Cleaner](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/VacuumCleaner.png)]() [Vacuum Cleaner](Documentation/Things/VacuumCleaner.md) |
| A camera with a motion sensor connected | A TV translating an image from the camera | A vacuum cleaner thing, which can be docked/undocked and move around the scene |

### Gesture recognizer

GestureRecognizerItem.cs item class enables usage of user-defined gestures. In the example, the Lamp toggles when Thumbs Up gesture is shown.

![](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/ThumbsUp.gif)


### Thing Designer

In the Client scene you can find a GameObject called ThingDesigner. Add item prefabs to ThingDesignerItemCollection. You can instantiate new items, move them around the scene and even edit them (in the future update). Save the newly created thing as a prefab using Unity Editor (in Play Mode - input simulation mode).  

![](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/ThingDesignerEditor.gif)

![](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/ThingDesignerRuntime.gif)

### Package Structure 
The main part of the package is Server, where classes for the items and things are defined.

*Things are Gameobjects added into Unity project which represent either a digital twin of a real IoT device or a purely virtual IoT device. Things potentially provide many functionalities in one.*

*Items are the parts things consist of: for example, smart light in the room can consist of several lamps and a receiver – each of them as an item.*
	
Other parts of IoT VR Platform are: Resources, Thirdparty and Scenes.	
	
	
## Setup

### VR (Client) part
1. **[VIDEO TUTORIAL](https://www.bilibili.com/video/BV1vr4y1F7Jg)** Intall MRTK [here](https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/Installation.html) and configure it for Oculus [here](https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/CrossPlatform/OculusQuestMRTK.html)
**OR** just use [this project](https://github.com/provencher/MRTK-Quest-Sample) to save time *(but it is not guaranteed to work properly).*
2. **[VIDEO TUTORIAL](https://www.bilibili.com/video/BV17z4y1y7Bb)** Import IoThingsLab.unitypackage from the [latest release](https://github.com/VRSimulator/IoThingsLab/releases).

### If you want to use other VR Headsets than Oculus or use Oculus connected to PC/Mac/Linux

[**Tutorial For Steam VR**](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/SteamVRSetupTutorial/SteamVRSetup.md)

### Input simulation
[Input simulation service Documentation](https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/InputSimulation/InputSimulationService.html)

### Tutorial on the IoT part and how to run the server on the local machine
1. Install [openHAB](https://www.openhab.org/download/) and run the server.;
2. Add [REST API](https://www.openhab.org/docs/configuration/restdocs.html) binding;
3. Follow the instructions for binding your IoT device;
Example: control the lamp brightness by gesture (angle of ThumbsUp)

![](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/openHABGestureControl.gif)


## Known Issues
1. If the camera is sticked to your head and is not moving **OR** if the following error occurs:
![](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/Bug.png)

On the top of Unity Editor, select Mixed Reality Toolkit -> Utilities -> Oculus -> Integrate Oculus Integration Unity Modules;

2. If the following error occurs when you try to run the project in input simulation:

![](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/ErrorLayout.png)

Then you need to select the default layout:

![](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/LayoutFix.png)


## Contributing to the platform
请分享新想法, 谢谢！
