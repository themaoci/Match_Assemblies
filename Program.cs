
// Created by TheMaoci //

using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Match_Assemblies
{
    class Program
    {
        internal static bool DEBUG = false;
        

        static void Main(string[] args)
        {
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dll");
            if (!Directory.Exists(directory)) 
            {
                Console.WriteLine("dll folder doesnt exist make sure to create one and add assembly-csharp files inside");
                Console.ReadKey();
                return;
            }
            
            List<SearchList.Searcher> ListOfSearchings = SearchList.Create();
            
            List<string> fileList = Directory.GetFiles(directory).ToList();

            for (int i = 0; i < fileList.Count; i++) {
                string TextToFile = "";
                string path = fileList[i];
                var assembly = ModuleDefMD.Load(path);
                var types = assembly.Types;
                if (DEBUG)
                    Console.WriteLine(types.Count());
                // find place in assembly where game version is storaged.
                var findAll = types.Where(type => !type.Name.Contains("`") && type.Name.StartsWith("Class") && type.Fields.Count == 1 && type.Methods.Count == 1 && type.Properties.Count == 0).ToList();
                if (findAll.Count > 0 && findAll[0].Fields.Count > 0)
                {
                    TextToFile += $"// {findAll[0].Fields[0].Constant.Value}\n";
                    Console.WriteLine($"Assembly Game Version: {findAll[0].Fields[0].Constant.Value}");
                }
                Console.WriteLine("Current => New\n");

                List<string> UsingList = new List<string>();
                List<string> InfoList = new List<string>();

                foreach (var searcher in ListOfSearchings)
                {
                    int searching = 0;
                    string Output = "", FullOutput = "";

                    if (SearchList.GClassSearcher(types, ref searching, searcher, ref Output, ref FullOutput))
                    {
                        UsingList.Add($"using {searcher.OriginalName} = {FullOutput};");
                        InfoList.Add($"{searcher.OriginalName} => {Output} [{FullOutput}]");
                    }
                }
                for (int j = 0; j < UsingList.Count; j++)
                {
                    Console.WriteLine(UsingList[j]);
                    TextToFile += $"{UsingList[j]}\n";
                }
                Console.WriteLine("");
                if (DEBUG)
                {
                    for (int k = 0; k < InfoList.Count; k++)
                    {
                        Console.WriteLine(InfoList[k]);
                    }
                }
                string fileName = $"{findAll[0].Fields[0].Constant.Value.ToString().Split('.').Last()}.cs";
                if (!Directory.Exists("output/"))
                {
                    Directory.CreateDirectory("output/");
                }
                File.WriteAllText("output/" + fileName, TextToFile);
            }
            Console.WriteLine("\nFinished... Waiting on key press...");
            Console.ReadKey();
        }
    }
}
