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
        static bool DEBUG = true;
        static bool searchingForGClasses(IList<TypeDef> types, ref int searching, gclassSearch searchClass, ref string Output) {
            var FilteredTypes = types.Where(type => type.Name.StartsWith(searchClass.StartingWith));
            foreach (TypeDef type in FilteredTypes)
            {
                foreach (var method in type.Methods)
                {
                    if (searchClass.methodsItContain.Contains(method.Name))
                    {
                        if(DEBUG)
                            Console.WriteLine($"method.Name:{method.Name}, {type.FullName}");
                        searching++;
                        
                        if (searching == searchClass.CountMatches)
                        {
                            Output = $"{type.Name}";
                            
                            return searching == searchClass.CountMatches;
                        }
                    }
                }
                foreach (var property in type.Properties)
                {
                    if (searchClass.propertiesItContain.Contains(property.Name))
                    {
                        Console.WriteLine(property.Name);
                        searching++;
                        if (searching == searchClass.CountMatches)
                        {
                            Output = $"{type.Name}";

                            return searching == searchClass.CountMatches;
                        }
                    }
                }
            }
            return searching == searchClass.CountMatches;
        }
        class gclassSearch {
            public gclassSearch(string originalName, string startingWith, List<string> MethodsToLookFor, List<string> PropertiesToLookFor) 
            {
                OriginalName = originalName;
                StartingWith = startingWith;
                methodsItContain = MethodsToLookFor;
                propertiesItContain = PropertiesToLookFor;
            }
            public int CountMatches {
                get 
                {
                    return methodsItContain.Count + propertiesItContain.Count;
                }
            }
            public string OriginalName = "";
            public string StartingWith = "";
            public List<string> methodsItContain = new List<string>();
            public List<string> propertiesItContain = new List<string>();
        }
        static void Main(string[] args)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assembly-CSharp_o.dll");
            var assembly = ModuleDefMD.Load(path);

            List<gclassSearch> ListOfSearchings = new List<gclassSearch>();
            ListOfSearchings.Add(new gclassSearch(
                "MainMenuController", "GClass",
                new List<string> { "OnProfileChangeApplied", "ShowScreen" },
                new List<string> { "SelectedLocation", "InventoryController" }));
            ListOfSearchings.Add(new gclassSearch(
                "IHealthController", "GInterface",
                new List<string> { "GetBodyPartsInCriticalCondition", "AddImmunityNotificationEffect", "PropagateAllEffects" },
                new List<string> { "DamageCoeff", "TemperatureRate", "CarryingWeightRelativeModifier" }));
            //var types = Original_Assembly.GetType(findName);
            //var getTypes = .Single(Class => Class.Name == findName);
            var types = assembly.Types; //.Single(type => type.Name == findName);
            if(DEBUG)
                Console.WriteLine(types.Count());
            Console.WriteLine("Current => New");
            foreach (var gclassSearch in ListOfSearchings) {
                int searching = 0;
                string Output = "";

                if (searchingForGClasses(types, ref searching, gclassSearch, ref Output))
                {
                    Console.WriteLine(gclassSearch.OriginalName + " => " + Output);
                    Console.WriteLine(" ");
                }

            }

            Console.WriteLine("Finished... Waiting on key press...");
            Console.ReadKey();
        }
    }
}
