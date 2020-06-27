using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary
{
    public static class Examples
    {
        public const string SuccessMessage = "The file was correctly loaded";
        public static string ExampleLoadTextFile(string file)
        {
            if (file.Length < 10)
            {
                throw new FileNotFoundException();
            }

            return SuccessMessage;
        }
    }
}
