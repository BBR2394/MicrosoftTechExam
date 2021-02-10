using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Baptiste.BertrandRapello.Ocr.Tests
{
    public class OcrUnitTest
    {
        [Fact]
        public async Task ImagesShouldBeReadCorrectly() {
            var executingPath = GetExecutingPath(); 
            var images = new List<byte[]>(); 
            foreach (var imagePath in Directory.EnumerateFiles(Path.Combine(executingPath, "Images"))) 
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                images.Add(imageBytes); 
            }
            var ocrObj = new Ocr();
            ocrObj.Read(images[0]);
            var txtres = ocrObj.Text;
            Assert.Equal(@"développeur C# au sens large. Car si\nvous savez coder en C#, potentiellemer\nvous savez coder avec toutes les", ocrObj.Read(images[0]));
            Assert.Equal(@"Malheureusement les cabinets de\nrecrutement et les annonces, ainsi que les\nétudes sur les salaires, gardent cette\nterminologie obsoléte.", ocrObj.Read(images[1]));
            Assert.Equal(@"Quel salaire peut-on espérer ? Comme\ntoujours, les écarts moyens sont\nimportants. La base serait :", ocrObj.Read(images[2]));
        }
        
        private static string GetExecutingPath() {
            var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var executingPath = Path.GetDirectoryName(executingAssemblyPath);
            return executingPath;
        }
    }
}
