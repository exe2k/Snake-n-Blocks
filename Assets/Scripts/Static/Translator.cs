using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Helpers;


public class Translator : MonoBehaviour
{
    /// <summary>
    /// Translation System
    /// Developed By Andreas Lishnevsky
    /// </summary>
    
    public static Translator instance = null;

    static Dictionary<string, string> Data;
    public enum Languages { EN = 0, RU = 1 };
    public Languages Language { get; private set; }

    public const string PATH_TXT = "TEXT";
    public static readonly string PATH_TXT_LANG = Path.Combine(PATH_TXT, "LANG");

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Data = CreateDictionary(Language.ToString());
    }

    private Dictionary<string, string> CreateDictionary(string filename)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        var file = FS.ReadFile(new string[] { PATH_TXT_LANG, filename });

        foreach (var line in file)
        {
            var temp = line.Split('=');

            if (temp.Length < 2) continue;
            var key = temp[0].Trim();
            var val = temp[1].Trim();

            if (key.Length <= 0) continue;
            dictionary.Add(key, val);
        }

        return dictionary;
    }

    public static void SetLanguage(Languages lang)
    {
        instance.Language = lang;
        Data = instance.CreateDictionary(lang.ToString());
    }

    public static string Translate(string key, string s="")
    {
        string result = string.Empty;
        Data.TryGetValue(key, out result);
        if(s.Length>0) result = result.Replace("%s", s);
        if (result == string.Empty) result = key;
        return result;
    }

    public static string Translate(string key, int i)
    {
        return Translate(key, i.ToString());
    }


    }
