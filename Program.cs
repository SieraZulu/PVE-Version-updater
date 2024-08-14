using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string menuFilePath = @"E:\Desktop\Sources\Projects\EFT PVE\Project\PVE\EscapeFromTarkovCheat\UI\menu.cs"; // Update this path
        UpdateVersion(menuFilePath);
    }

    static void UpdateVersion(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("GUILayout.Window"))
            {
                lines[i] = IncrementVersionNumber(lines[i]);
                break;
            }
        }

        File.WriteAllLines(filePath, lines);
        Console.WriteLine("Version updated successfully.");
    }

    static string IncrementVersionNumber(string line)
    {
        string versionPrefix = "PVE V";
        int startIndex = line.IndexOf(versionPrefix) + versionPrefix.Length;
        int endIndex = line.IndexOf('"', startIndex);
        string currentVersion = line.Substring(startIndex, endIndex - startIndex);

        // Split version parts
        string[] versionParts = currentVersion.Split('.');
        int major = int.Parse(versionParts[0]);
        int minor = int.Parse(versionParts[1]);
        int build = int.Parse(versionParts[2]);
        int revision = int.Parse(versionParts[3]);

        // Increment build number
        if (revision >= 99)
        {
            revision = 1;
            build++;
            if (build >= 100) // Example limit, adjust as needed
            {
                build = 1;
                minor++;
                if (minor >= 100) // Example limit, adjust as needed
                {
                    minor = 1;
                    major++;
                    // Optionally handle major version overflow here
                }
            }
        }
        else
        {
            revision++;
        }

        // Construct the new version number
        string newVersion = $"{major:D2}.{minor:D2}.{build:D2}.{revision:D2}";

        return line.Replace(currentVersion, newVersion);
    }
}

