using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microgroove.Models;
using KBCsv;
using System.Data;
using Microgroove.Interface;
using System.Reflection;

namespace Microgroove
{
    public class Program
    {
        // Please change the output path
        private static string path = @"C:\Users\v-daflo\Danny\webdev\C#Practice\Microgroove\Microgroove\Microgroove\Microgroove\InputFiles\testFile.csv";
        private static string outputPath = @"C:\Users\v-daflo\Danny\webdev\C#Practice\Microgroove\Microgroove\Microgroove\Microgroove\OutputFiles\output.json";

        public static void Main(string[] args)
        {
            FileReader fileReader = new FileReader();

            HashSet<MicroGrooveModel> content = fileReader.ReadCsvData(path);

            fileReader.WriteFile(outputPath, content);                       

           
        }




    }
}
