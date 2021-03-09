# NUIXStudio-client

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Required software and hardware](#required-software-and-hardware)
* [Setup](#setup)
* [Known issues](#known-issues)
* [Contributing to the platform](#contributing-to-the-platform)

## General info

<img align="left" width="200" src="https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/20201030_173803.jpg">
<img align="left" width="200" src="https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/20201030_175023.jpg">

By using NUIX-Studio platform researchers can connect real and virtual IoT devices, test new IoT devices inside VR environment (and don't even need to buy them!)

![](https://github.com/FedorIvachev/IoThingsLab-ReadmeFiles/blob/master/Readme/Files/v0.6/Architecture.png)
Currenly, the architecture can be represented by four layers:
1. **IoT real world devices.**
2. **openHAB server.** Provides:
	* Synchronization with IoT real world devices;
	* Storage of IoT data.
3. **PC - Unity Server.** It is connected by REST API to the openHAB server and is used for computations (virtual IoT devices interaction; virtual screens, lights, radio waves etc).
4. **VR Headset - Unity Client.** Even though we call it Client, it is actually connected to layer 2 - openHAB server. Provides API for interaction with virtual world.

The idea of running two instances of Unity at the same time is based on dividing the responsibilities between several project instances. In result, not powerful VR Headsets are responsible only for interaction with virtual IoT devices, while PC is responsible only for difficult computations (such as encoding/decoding, physics and other). In addition, *openHAB server - PC Unity Server* latency is so low compared to the latency of *openHAB server - VR Headset Unity Client* and *PC Unity Server - VR Headset Unity Client* that we don't need to care about having an additional layer.

The creation of items is performed on the openHAB server in a user-friendly way. The items are automatically synchronized with Unity Client and Server. I will further describe it in the next commit.

[You can also download the whole Project (Unity App + openHAB server).](https://github.com/VRSimulator/IoTStudio-WholeProject). Text creators to gain access.


## Technologies
NUIX-Studio uses the features of [Microsoft's Mixed Reality Toolkit](https://github.com/microsoft/MixedRealityToolkit-Unity#feature-areas), such as hand tracking and interaction techniques. 

Connection to real-world IoT devices is performed by REST API calls to [openHAB](https://www.openhab.org/download/) server.

## Required software and hardware:
1. Unity 2020
2. openHAB v3.0+

### Additional hardware:
1. Oculus Quest 1 or 2
2. IoT devices

## Setup

### openHAB installation

1. Install [openHAB](https://openhab.org/docs/installation/)
2. Go to Settings - Bindings and add Network Binding
3. Go to Things and add a new thing through Network binding Scan
4. Go to Model - Create Equipment from Things and select the recently created thing
5. Edit the added equipment thing. Write "Network device" in the category

### Unity installation

1. [Download Unity Hub](https://unity3d.com/get-unity/download)
2. Install Unity 2020 through the Installs Tab
3. In the Projects Tab, select Add. Open the dowloaded project (either you cloned it or dowloaded as [zip archive](https://github.com/VRSimulator/NUIX-Studio-Client/archive/master.zip))
4. Download [3DLivingRoom](https://github.com/VRSimulator/NUIX-Studio-Client/releases/download/v0.6-alpha1/3DLivingRoom.unitypackage) package, select Import package - Custom package and then import it.
5. If the openHAB server is running on your PC, the server will be accessible by address <Your IP Address>:8080
6. Run the Scene from \Assets\NUIX-Studio-Client\openHABIntegration\Scenes


### Additional: Run on Oculus
7. Follow the steps on [adding Oculus to MRTK](https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/CrossPlatform/OculusQuestMRTK.html)
8. In the builds settings, select platform - Android and Device - Oculus Quest 1 or 2. Press Build and Run
9. Now Oculus is used to control the IoT devices stored on your openHAB server

### Input simulation
[Input simulation service Documentation](https://microsoft.github.io/MixedRealityToolkit-Unity/Documentation/InputSimulation/InputSimulationService.html)

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
