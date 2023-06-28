using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using System;

namespace camera
{
    class Program
    {
        static void Main(string[] args)
        {
            var OurWindow = new NativeWindowSettings()
            {
                Size = new Vector2i(1920, 1080),
                Title = "UAS GRAFKOMx"
            };

            using (var win = new Windows(GameWindowSettings.Default, OurWindow))
            {
                win.Run();
            }
        }
    }
}
