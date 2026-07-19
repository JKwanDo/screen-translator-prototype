using System;
using System.Diagnostics;
using System.Threading;
using Tesseract;
using System.IO;
using System.Drawing;


TesseractEngine engine = new TesseractEngine(@"./tessdata", "rus", EngineMode.Default);
Directory.CreateDirectory("tessoutput");
Stopwatch timer = new Stopwatch();
try
{
    while (true)
    {
        timer.Restart();

        Rectangle bounds = new Rectangle(0, 0, 1920, 1080);

        using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size);
            }
            using (var page = engine.Process(bitmap))
            {
                string text = page.GetText();
                var result = new { timestamp = DateTime.Now, text };
                string json = System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("tessoutput/output.json", json);
            }
            // 0.5 fps screenCapture loop
            int elapsedTimeMs = (int)timer.ElapsedMilliseconds;
            int waitTime = 60000 - elapsedTimeMs;
            if (waitTime > 0)
            {
                Thread.Sleep(waitTime);
            }
        }
    }
}
finally
{
    foreach (var file in Directory.GetFiles("tessoutput"))
        File.Delete(file);
}
