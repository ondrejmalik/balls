// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.IO;
using System.Text.RegularExpressions;

public class MapLoader
{
    public string LoadMap()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) + "\\koule";
        string file = "\\map.txt";

        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        if (File.Exists(path + file) == false)
        {
            File.Create(path + file);
        }

        StreamReader sr = new StreamReader(path + file);
        string pattern = "\"pathData\":\\s*\"(.*?)\"";
        string input = sr.ReadToEnd();
        Group g = Regex.Match(input, pattern).Groups[1];
        Console.WriteLine(g.Value);
        return g.Value.Replace("\n", "");
    }
}
