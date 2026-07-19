using System;
using System.Diagnostics;
using System.Threading;
using Tesseract;
using System.IO;
using System.Drawing;


TesseractEngine engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
Stopwatch timer = new Stopwatch();
while (true)
{

    timer.Restart();

    Rectangle bounds = new Rectangle(0, 0, 1920, 1080);

    using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
    {
        using (Graphics g = Graphics.FromImage(bitmap))
        {
        }
        // 0.5 fps screenCapture loop
        int elapsedTimeMs = (int)timer.ElapsedMilliseconds;
        int waitTime = 2000 - elapsedTimeMs;
        if (waitTime > 0)
        {
            Thread.Sleep(waitTime);
        }
    }
}
