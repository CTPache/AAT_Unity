# Phoenix Wright Ace Attorney Trilogy - Modding/Translating project

## Running the project
### From the Unity Editor
* Using UnityHub import this as a project (clone the repository and add project from disk).
* Open it, wait until the project loads.
* Copy the StreamingAssets directory from the game (``PWAAT_Data/StreamingAssets``) in the Assets directory.
* Execute the Asset Decryptor on that directory (``assetDecryptor.exe ./Assets/StreamingAssets/``).
* If you want to run a custom language, add it to ``/Assets/LangPacks``. There are 3 examples: Spanish, Portuguese and Vietnamese.
### From the retail game
You can do it two ways.
The first way is just building the project from the editor and taking the ``Managed`` directory to the ``PWAAT_Data`` directory, replacing everything.

The other way is to build it with Visual Studio.
* Open ``AAT.sln``. Compile the solution (Debug or Release). The compiled libraries will be on ``obj/``.
* You won't probably need to replace ``Assembly-CSharp-firstpass``, so take just ``Assembly-CSharp.dll`` and copy it to ``PWAAT_Data/Managed``.
* Right now, the only library that has to be added is ``Newtonsoft.Json.dll``. It is on the ``/Assets/UnityPackages`` directory.

You must run the Asset Decryptor on your game as well (``assetDecryptor.exe ./PWAAT_Data/StreamingAssets/``).

For the retail game, the ``LangPacks`` directory should be located on the ``PWAAT_Data`` directory.

## Language Pack format
A Language Pack will mirror the structure of ``StreamingAssets``. Add there any asset you wish to replace.

You must specify on a ``manifest.json`` file the following fields:
* "lang": Should be the exact same name as the LangPack directory
* "fallback": The language your patch is based. usually USA.
* "sufix": The sufix of the files you are going to load (if your pack just replaces the English version, it'll be ``_u``).
* "menuStrings": The name of your language in every other language. If it's missing from this list it defaults to the value of "lang". Recomended to add, at least, the 7 vanilla languages (JAPAN, "USA", "GERMAN", "FRANCE", "KOREA", "CHINA_T", "CHINA_S").
Optional fields:
* "font": You can add a font asset on your project, it should be a unity.3d file containing the font. See the Vietnam example.
* "fontSize": The size the font will be displayed on the message box.
* "fontTanteiSize": The size the font will be displayed on the court record.
* "pcview": Optional struct that manages case 3-4's intro. See [PcViewCtrl](/Assets/Scripts/Assembly-CSharp/PcViewCtrl.cs). The example languages show how it works.
> "fontCountArray"
> 
> "fontFillAmount"
> 
> "OBJ_OP4_008_DiffPosition"
> 
> "facePhotoPosition"
> 
> "fontSpritePositionX"
> 
> "fontSpritePositionY"
> 
> "cursorPosition"
* "international_files_common", "international_files_gs1", "international_files_gs2","international_files_gs3": If your patch just replaces a language don't even worry about these.
They are dictionaries of assets that don't just use the sufix but have different names. See [LanguageFileName](Assets/Scripts/Assembly-CSharp/LanguageFileName.cs) and [ReplaceLanguage](Assets/Scripts/Assembly-CSharp/ReplaceLanguage.cs).

## Future plans

First of all, the Vietnamese patch has some peculiarities that have to be worked on. They had to add more options for some situations, and that is not been supported right now.

Also, Rise From The Ashes' 3D minigames and all the other things that are only used there need to be worked on.

I would like to add support for general mods as well, like my [Old UI Mod](https://github.com/CTPache/aat-old-gui-mod), the [Autoplay](https://www.nexusmods.com/phoenixwrightaceattorneytrilogy/mods/5) and [Discord RPC](https://github.com/WorstAquaPlayer/AATrilogy-2019-RPC).

When all that is done, I'm planing on adding other languages. I've tested [Dant's Russian patch](https://gamecom.neocities.org/Ace_Attorney/Translations/Sudebnyy_povorot_Trilogiya_Steam/) but it has some font related issues and some encoding diferences for what it seems.

The [Arabic](https://www.nexusmods.com/phoenixwrightaceattorneytrilogy/mods/1) patch should work just fine.

The [Thai](https://github.com/MaFIaTH/AAT-Thai-Translation-Localization-Mod) patch seems to work, but I can't test it myself.
