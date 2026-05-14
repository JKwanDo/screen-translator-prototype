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
            g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color originalColor = bitmap.GetPixel(x, y);
                    int grayValue = (int)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);
                    Color grayColor = Color.FromArgb(originalColor.A, grayValue, grayValue, grayValue);
                    bitmap.SetPixel(x, y, grayColor);
                }
            }
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
