using System;
using System.Diagnostics;
using System.Threading;
using Tesseract;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        TesseractEngine engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
        Stopwatch timer = new Stopwatch();
        while(true) {

        timer.Restart();

        Rectangle bounds = new Rectangle(0, 0, 1920, 1080);

        using(Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
        {
            using(Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);    
            }
        }
        int elapsedTimeMs = (int)timer.ElapsedMilliseconds;
        int waitTime = 200 - elapsedTimeMs;
        if (waitTime > 0) {
                Thread.Sleep(waitTime);
            }
    }
}
}