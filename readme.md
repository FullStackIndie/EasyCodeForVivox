# EasyCodeForVivox

This is an Extension Asset for **Vivox Voice Chat SDK** with the goal of helping indie devs and small teams and even new programmers get setup with Vivox as fast as possible. Vivox can be hard to implement and understand and this asset provides ease of use without learning how to use Vivox from scratch.

You can download the latest updates here in the [Releases](https://github.com/FullStackIndie/EasyCodeForVivox/releases) section of this repo. This repo will always be more updated than the Unity Asset Store version but...   **EasyCodeForVivox** is availible on the [Unity Asset Store](https://assetstore.unity.com/packages/add-ons/easycodeforvivox-202190)  and will always be the most reliable version.

# Where it works

At the moment this asset works and has been tested on Windows and Android. It should work iOS and Mac but I have not tested it yet.
Vivox does not support Linux at the moment as far as I know.

## Disclaimer

I also may accidentaly commit and push bugs and breaking changes to this repo when I am just trying to save some work.

EasyCode will start supporting **Unity Authentication and Vivox services** from the package manager at some point the future and will be fully UGS compatible

**Download from this Github Repo directly at your own risk!!! Latest commits may contain bugs**.

## Design Decisions

**There have been many breaking changes since I first started this project, but starting from EasyCode 2.0 I will try and provide backwards compatibilty for future releases and If I deprecate something I will leave in the asset until there is enough documentation on how to upgrade/migrate your code to the newest versions**

I have decided to support only Unity 2021.3 (LTS) and newer. **EasyCode will no longer support 2020 or lower**. My decision is based on Unity releasing their Gaming Services and after seeing how some of the services they offer work well with Vivox. Most of the Gaming Services are only available natively in **2021+** (although you can import some if them in 2019 & 2020 versions). **Another big plus is that Unity supports C# 8 which provides great improvements when coding in C#**

## Before you Import into your Project

This Asset comes with a **3D demo scene** that requires Unity's Networking stack **NetCodeForGameObjects**. If you want to use the 3d demo scene you need **Unity 2021.3.11f** or similar **2021.3 version**. (May work on 2022+ but have not tested)

If you don't need the 3D demo scenes then you wont need to import NetCodeForGameObjects. **You can still use the Chat scene demo** since it doesnt require **NetCodeForGameObjects**.

## Dependencies

**Vivox Voice Chat** is a seperate asset in the **Unity Asset Store** that must be downloaded seperately. You can also download from **Unity Gaming Services Dashboard** or from the **Vivox Developer Portal**.

EasyCode uses Zenject Plugin for dependency injection and is included automatically in this project. It does add an extra 50mb of files but Dependency Injection is great for scaling for your game when you have a lot of systems. **Zenject** is a **free** Unity Asset that can be downloaded from the **Unity Asset Store** or from the **Zenject Github Repo**. **EasyCodeForVivox** comes with a **Zenject Installer** that will install all the dependencies for you. **You can also use your own Zenject Installer** if you want to customize the installation process.

## Features

- Easy to use API and Demo Scenes to see what Vivox is capable of
- 3D Demo Scene with NetCodeForGameObjects to test out 3D Positional Audio
- Login Multiple Users/Player Sessions (requires custom logic)
- Join 1-10 Non-Positional Channels (Conference Channels)
- Join 1 3D Positional Channel
- Maximum Joined Channels is 10 (9 Non-Positional, 1 Positional)
- Send Channel Messages
- Send Direct Messages (as long you know the User's Name) [**Recommended to use dedicated game server or 3rd party service to get user's name**]
- Toggle Voice/Audio on/off in channel
- Toggle Text in on/off channel
- Adjust Local Players Volume
- Adjust Remote Player's Volume if you know their name
- Mute Self
- Mute other players in channel if you know their name
- Text-To-Speech(TTS) - All of Vivox TTS options available
- Push-To-Talk functionality
- **Demo Scene Feature** - Raise Hand feature to imitate Zoom/Discord video sessions where Admin/Teacher/Host can mute/unmute anyone in the current channel. Anyone who is not Admin/Host/Teacher can raise their hand and then the Admin/Host/Teacher gets to decide if they want to unmute them and when to mute them. In the LoginToVivox method in EasyChatExample.cs or Easy3DExample.cs make sure the joinMuted parameter is set to true for all users and whoever you decide gets to be the Admin leave false. It is false by default.

## Documentation

**For complete documentation check out the GitBook Docs**
(<https://fullstackindie.gitbook.io/easy-code-for-vivox/intro/introduction>)

**EasyCodeForVivox** also comes with an **offline lite version** of the online docs

## Contribute

If you would like to contribute to this project reach out to me at on Twitter, LinkedIn, (links in Bio/profile) or create a pull request and I will see if your contribution is worthy of being added :grin: :stuck_out_tongue_winking_eye:

## Fork Me

**You can also fork this project and create your own version**. Check out the **license** for more info <https://github.com/FullStackIndie/EasyCodeForVivox/blob/main/LICENSE.md>
