using UnityEngine;

namespace Logging
{
    public static class LogHandler
    {
        public static string GetColor(string name)
        {
            var hue = (uint) name.GetHashCode() / (float) uint.MaxValue;
            var color = Color.HSVToRGB(hue, 0.6f, 1f);
            return ColorUtility.ToHtmlStringRGB(color);
        }
    }
}