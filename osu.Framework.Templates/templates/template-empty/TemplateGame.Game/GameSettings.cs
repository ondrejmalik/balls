using System;

namespace UdpTest.Game;

public class GameSettings
{
    public static double MusicVolume { get; set; }
    public static double HitsoundVolume { get; set; }
    public static bool EnableParticles { get; set; }
    public static bool HardMode { get; set; }

    public static string ToString()
    {
        return $"{GameSettings.MusicVolume};{GameSettings.HitsoundVolume};{GameSettings.EnableParticles};{GameSettings.HardMode}";
    }

    public static void SetDefaluts()
    {
        MusicVolume = 10;
        HitsoundVolume = 10;
        EnableParticles = true;
        HardMode = false;
    }

    public static void SetSettings(string[] settings)
    {
        if (settings == null || settings.Length != 8)
        {
            SetDefaluts();
        }

        MusicVolume = Convert.ToDouble(settings[0]);
        HitsoundVolume = Convert.ToDouble(settings[1]);
        EnableParticles = Convert.ToBoolean(settings[2]);
        HardMode = Convert.ToBoolean(settings[3]);
    }
}
