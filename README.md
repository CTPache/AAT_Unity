# Phoenix Wright Ace Attorney Trilogy - Modding/Translating Project

## Running the Project

### From the Unity Editor

* Using Unity Hub, import this as a project (clone the repository and add the project from disk).
* Open it and wait until the project loads.
* Copy the `StreamingAssets` directory from the game (`PWAAT_Data/StreamingAssets`) into the `Assets` directory.
* Execute the Asset Decryptor on that directory (`assetDecryptor.exe ./Assets/StreamingAssets/`).
* If you want to run a custom language, add it to `/Assets/LangPacks`. There are 3 examples: Spanish, Portuguese, and Vietnamese.

### From the Retail Game

You can do this in two ways:

1. The first way is by building the project from the editor and copying the `Managed` directory to the `PWAAT_Data` directory, replacing everything.

2. The other way is to build it with Visual Studio:
   * Open `AAT.sln` and compile the solution (Debug or Release). The compiled libraries will be in `obj/`.
   * You probably won’t need to replace `Assembly-CSharp-firstpass`, so just take `Assembly-CSharp.dll` and copy it to `PWAAT_Data/Managed`.
   * Currently, the only additional library required is `Newtonsoft.Json.dll`. It is located in the `/Assets/UnityPackages` directory.

You must also run the Asset Decryptor on your game (`assetDecryptor.exe ./PWAAT_Data/StreamingAssets/`).

For the retail game, the `LangPacks` and `Mods` directories should be located in the `PWAAT_Data` directory.

## Language Pack Format

A language pack will mirror the structure of `StreamingAssets`. Add there any asset you wish to replace.

You must specify the following fields in a `manifest.json` file:

* `"lang"`: Should be the exact same name as the LangPack directory.
* `"fallback"`: The language your patch is based on, usually "USA".
* `"suffix"`: The suffix of the files you are going to load (if your pack just replaces the English version, it'll be `_u`).
* `"menuStrings"`: The name of your language in every other language. If it's missing from this list, it defaults to the value of `"lang"`. Recommended to add at least the 7 vanilla languages (`JAPAN`, `"USA"`, `"GERMAN"`, `"FRANCE"`, `"KOREA"`, `"CHINA_T"`, `"CHINA_S"`).

Optional fields:

* `"font"`: You can add a font asset to your project. It must be a Windows font and must be added to the `Resources` directory of the game.
* `"fontSize"`: The size of the font will be displayed in the message box.
* `"fontTanteiSize"`: The size of the font will be displayed in the court record.
* `"pcview"`: Optional struct that manages case 3-4's intro. See [PcViewCtrl](/Assets/Scripts/Assembly-CSharp/PcViewCtrl.cs). The example languages show how it works.

```json
"fontCountArray"
"fontFillAmount"
"OBJ_OP4_008_DiffPosition"
"facePhotoPosition"
"fontSpritePositionX"
"fontSpritePositionY"
"cursorPosition"
```

* `"international_files_common"`, `"international_files_gs1"`, `"international_files_gs2"`, `"international_files_gs3"`: If your patch just replaces a language, don’t worry about these. They are dictionaries of assets that don’t just use the suffix but have different names. See [LanguageFileName](Assets/Scripts/Assembly-CSharp/LanguageFileName.cs) and [ReplaceLanguage](Assets/Scripts/Assembly-CSharp/ReplaceLanguage.cs).

## Mod Format

A mod will work similarly to a language pack, except it doesn't require a manifest, as all the language-specific things won’t be used.  
The structure of a mod will match that of `StreamingAssets`, and mods will load in alphabetical order (so the asset selected will be from the mod with the highest priority). You can adjust the mod loading priority by naming them with a numerical prefix.

A mod can also run custom code — see the [Mod Example](ModExample/).

> As a good practice, I recommend ALWAYS shipping your mods with the source code.  
> And if you're a user, DO NOT RUN ANYTHING YOU DON'T TRUST.

## Future Plans

First of all, the Vietnamese patch has some peculiarities that need to be addressed. It had to add more options for certain situations, and that is not supported right now.

Also, *Rise From The Ashes’* 3D minigames and other exclusive features used only there still need to be handled properly.

Once those are done, I plan to add other languages. I've tested [Dant’s Russian patch](https://gamecom.neocities.org/Ace_Attorney/Translations/Sudebnyy_povorot_Trilogiya_Steam/), but it has some font-related issues and apparent encoding differences.

The [Arabic](https://www.nexusmods.com/phoenixwrightaceattorneytrilogy/mods/1) patch should work just fine.

The [Thai](https://github.com/MaFIaTH/AAT-Thai-Translation-Localization-Mod) patch seems to work, but I can't test it myself.
