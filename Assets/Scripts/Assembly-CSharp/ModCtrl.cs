
using System.Collections.Generic;
using System.Reflection;
using System;
using System.IO;
using UnityEngine;
using System.Linq;
public static class ModCtrl
{
    static private readonly string ModsDirectory = Application.dataPath + "/Mods";
    static private List<string> modList = new List<string>();
    static public void Init()
    {        
        if (Directory.Exists(ModsDirectory))
        {
            string[] ModDirs = Directory.GetDirectories(ModsDirectory);
            foreach (string ModDir in ModDirs)
            {
                if (ModDir.StartsWith("."))
                { continue; }
                Debug.Log("Adding mod " + ModDir);
                modList.Add(ModDir);
                if (File.Exists(ModDir + "/mod.dll"))
                    invokeMod(ModDir + "/mod.dll");
            }
        }
    }

    static private void invokeMod(string dll)
    {
        Type[] modTypes = Assembly.LoadFile(dll).GetTypes();
        foreach (Type type in modTypes)
        {
            if (type.Name == "Main")
            {
                var instance = Activator.CreateInstance(type);
                foreach (MethodInfo method in type.GetMethods().Where(i => i.Name == "ModMain"))
                {
                    method.Invoke(instance, null);
                }

            }
        }
    }

    public static string load(string in_name, string in_path, int in_language = -1)
    {
        in_name = ReplaceLanguage.GetFileName(in_path, in_name, in_language);

        string mod = null;
        foreach (string modItem in modList)
        {
            if (File.Exists(modItem + in_path + in_name + ".unity3d"))
            {
                Debug.Log("Loading from mod " + modItem);
                mod = modItem;
            }
        }
        if (mod != null)
        {
            return mod + "/" + in_path + in_name + ".unity3d";
        }
        return null;
    }
}