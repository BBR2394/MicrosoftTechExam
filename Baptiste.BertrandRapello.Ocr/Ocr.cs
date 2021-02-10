using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Tesseract;

namespace Baptiste.BertrandRapello.Ocr
{
    public class Ocr
    {
        public string Text { get; set; }
        public float Confidence { get; set; }
        public List<string> OcrResult { get; }
        
        public String Read(byte[] image)
        {
            var executingAssemblyPath = Assembly.GetExecutingAssembly().Location; var executingPath = Path.GetDirectoryName(executingAssemblyPath);
            using var engine = new TesseractEngine(Path.Combine(executingPath, @"../fra.traineddata"), "fra", EngineMode.Default);
            using var pix = Pix.LoadFromMemory(image);
            var test = engine.Process(pix);
            this.Text = test.GetText();
            this.Confidence = test.GetMeanConfidence();
            this.OcrResult.Add(this.Text);
            return this.Text;
        }
    }
}