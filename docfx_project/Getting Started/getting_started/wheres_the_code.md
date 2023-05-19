# Wheres The Code

## Want to See the Code?

Example scripts are located **Assets/EasyCodeForVivox/Examples/Demo Scene Examples/** and are the highest abstraction away from the **Vivox Unity SDK** and the easiest scripts to get started with. They are meant to be example scripts to show you how to inherit from `EasyManager.cs`.

> [!NOTE]
> `EasyManager.cs` is a mid-level abstraction containing variable instances for the core Vivox Functionality scripts located in Assets/EasyCodeForVivox/Scripts/EasyBackend

Feel free to modify the code, redistribute, or sell. Check out the License here. This asset is meant for the community and small teams and there will always be a free version that implements most if not all **Vivox functionality**.

## Want To Customize This Asset

Assets/EasyCodeForVivox/EasyScripts/EasyBackend folder contains all the scripts you need to implement Vivox Voice and Text Chat functionality. If you want to start learning Vivox from scratch or implement your own code look at the scripts in the Vivox Backend folder. The code they contain are very similar to the Vivox Documentation and it will be easier to implement Vivox from scratch after having working code examples to compare to the Vivox Documentation.

> [!NOTE]
> EasyCode uses dependency injection under the hood. It's basically injecting existing Singleton instances of classes instead of using the new keyword (to create new classes/objects in every class where I need a specific class) or having a static instance of the class.

It is recommended to inherit from EasyManager.cs or create your own version using the scripts in EasyBackend/ because all the boilerplate code has been written for you, you can just create custom wrapper methods around the default methods.

> [!WARNING]
> ** Don’t change the name of EasyManager.cs or you risk breaking this asset and any future updates. It's better to create a copy and rename it. Also change the namespace to a namespace that fits your project and just import EasyCode with a using statement
using EasyCodeForVivox;
