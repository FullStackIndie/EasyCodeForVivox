namespace EasyCodeForVivox.Extensions
{
    public static class EasyDebug
    {
        public const string Aqua = "#00ffffff";
        public const string Blue = "#0000ffff";
        public const string Brown = "#a52a2aff";
        public const string Cyan = "#00ffffff";
        public const string Darkblue = "#0000a0ff";
        public const string Fuchsia = "#ff00ffff";
        public const string Green = "#008000ff";
        public const string Grey = "#808080ff";
        public const string Lightblue = "#add8e6ff";
        public const string Lime = "#00ff00ff";
        public const string Magenta = "#ff00ffff";
        public const string Maroon = "#800000ff";
        public const string Navy = "#000080ff";
        public const string Olive = "#808000ff";
        public const string Orange = "#ffa500ff";
        public const string Purple = "#800080ff";
        public const string Red = "#ff0000ff";
        public const string Silver = "#c0c0c0ff";
        public const string Teal = "#008080ff";
        public const string White = "#ffffffff";
        public const string Yellow = "#ffff00ff";

        public static string Color(this string msg, string color)
        {
            return $"<color={color}>{msg}</color>";
        }

        public static string Bold(this string msg)
        {
            return $"<b>{msg}</b>";
        }

        public static string Italic(this string msg)
        {
            return $"<i>{msg}</i>";
        }
    }
}
