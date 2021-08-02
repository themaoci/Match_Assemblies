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
        static bool DEBUG = false;
        static bool searchingForGClasses(IList<TypeDef> types, ref int searching, gclassSearch searchClass, ref string Output, ref string FullOutput) {
            
            var FilteredTypes = types.Where(type => type.Name.StartsWith(searchClass.StartingWith));
            foreach (TypeDef type in FilteredTypes)
            {
                foreach (var method in type.Methods)
                {
                    if (searchClass.methodsItContain.Contains(method.Name))
                    {
                        searching++;
                        if (DEBUG)
                            Console.WriteLine($"method.Name:{method.Name}, {type.FullName} [{searching} == {searchClass.CountMatches}]");

                        if (searching == searchClass.CountMatches)
                        {
                            Output = $"{type.Name}";
                            FullOutput = $"{type.FullName}";

                            return searching == searchClass.CountMatches;
                        }
                    }
                }
                foreach (var property in type.Properties)
                {
                    if (searchClass.propertiesItContain.Contains(property.Name))
                    {
                        searching++;
                        if (DEBUG)
                            Console.WriteLine($"{property.Name} - {searching} == {searchClass.CountMatches}");
                        if (searching == searchClass.CountMatches)
                        {
                            Output = $"{type.Name}";
                            FullOutput = $"{type.FullName}";

                            return searching == searchClass.CountMatches;
                        }
                    }
                }
                foreach (var field in type.Fields)
                {
                    if (searchClass.fieldsItContain.Contains(field.Name))
                    {
                        searching++;
                        if (DEBUG)
                            Console.WriteLine($"{field.Name} - {searching} == {searchClass.CountMatches}");
                        if (searching == searchClass.CountMatches)
                        {
                            Output = $"{type.Name}";
                            FullOutput = $"{type.FullName}";
                            return searching == searchClass.CountMatches;
                        }
                    }
                }
                searching = 0;
            }
            return searching == searchClass.CountMatches;
        }
        class gclassSearch {
            public gclassSearch(string originalName, string startingWith, List<string> MethodsToLookFor = null, List<string> PropertiesToLookFor = null, List<string> FieldsToLookFor = null) 
            {
                OriginalName = originalName;
                StartingWith = startingWith;
                if(MethodsToLookFor != null)
                    methodsItContain = MethodsToLookFor;
                if(PropertiesToLookFor != null)
                    propertiesItContain = PropertiesToLookFor;
                if(FieldsToLookFor != null)
                    fieldsItContain = FieldsToLookFor;
            }
            public int CountMatches {
                get 
                {
                    return methodsItContain.Count + propertiesItContain.Count + fieldsItContain.Count;
                }
            }
            public string OriginalName = "";
            public string StartingWith = "";
            public List<string> methodsItContain = new List<string>();
            public List<string> propertiesItContain = new List<string>();
            public List<string> fieldsItContain = new List<string>();
        }
        static void Main(string[] args)
        {
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dll");
            if (!Directory.Exists(directory)) {
                Console.WriteLine("dll folder doesnt exist make sure to create one and add assembly files inside");
                Console.ReadKey();
                return;
            }

            List<gclassSearch> ListOfSearchings = new List<gclassSearch>();
            #region Adding Searching Names
            ListOfSearchings.Add(new gclassSearch(
                "MainMenuController", "GClass",
                new List<string> { "OnProfileChangeApplied", "ShowScreen" },
                new List<string> { "SelectedLocation", "InventoryController" }));

            ListOfSearchings.Add(new gclassSearch(
                "IHealthController", "GInterface",
                new List<string> { "GetBodyPartsInCriticalCondition", "AddImmunityNotificationEffect", "PropagateAllEffects" },
                new List<string> { "DamageCoeff", "TemperatureRate", "CarryingWeightRelativeModifier" }));

            ListOfSearchings.Add(new gclassSearch(
                "StDamage", "GStruct",
                new List<string> { "GetOverDamage" },
                new List<string> { "Blunt" },
                new List<string> { "BlockedBy", "DidBodyDamage", "DidArmorDamage" }));

            ListOfSearchings.Add(new gclassSearch(
                "IEffect", "GInterface",
                new List<string> { "AddWholeTime", "Propagate" },
                new List<string> { "DisplayableVariations", "CurStateTimeLeft" }));

            ListOfSearchings.Add(new gclassSearch(
                "ISession", "GInterface",
                new List<string> { "GetPhpSessionId" }));

            ListOfSearchings.Add(new gclassSearch(
                "ClientConfig", "GClass",
                new List<string> { "LoadApplicationConfig" },
                new List<string> { "BackendCacheDir", "ConfigFilePath", "BackendCacheDir", "BackendUrl" },
                new List<string> { "DEFAULT_BACKEND_URL" }));

            ListOfSearchings.Add(new gclassSearch(
                "IBundleLock", "GInterface",
                new List<string> { "Lock", "Unlock" },
                new List<string> { "IsLocked" }));

            ListOfSearchings.Add(new gclassSearch(
                "BindableState(gclass+1)", "GClass",
                new List<string> { "BindWithoutValue", "Unbind" },
                new List<string> { "Value" },
                new List<string> { "ChangedEvents" }));

            ListOfSearchings.Add(new gclassSearch(
                "BotDifficultyHandler", "GClass",
                new List<string> { "CheckOnExcude", "LoadDifficultyStringInternal" },
                new List<string> { },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "WaveInfo", "GClass",
                new List<string> { },
                new List<string> { },
                new List<string> { "Role", "Limit", "Difficulty" }));

            ListOfSearchings.Add(new gclassSearch(
                "BotsPresets", "GClass",
                new List<string> { "GetNewProfile", "method_0" },
                new List<string> { },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "BotData", "GInterface",
                new List<string> { "ChooseProfile", "CanAtZoneByType", "ShallChooseByData", "IsBossOrFollowerByTime", "GetDebugData" },
                new List<string> { "SpawnParams" },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "PoolManager", "GClass",
                new List<string> { "CreateCleanLootPrefabAsync", "LoadBundlesAndCreatePools" },
                new List<string> { "PoolsCancellationToken" },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "JobPriority", "GClass",
                new List<string> { "GetYieldDelegate" },
                new List<string> { "General", "Immediate", "Low" },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "Requirement", "GClass",
                new List<string> { },
                new List<string> { "Fulfilled", "SourceAreaLevel", "SourceAreaType", "Type" },
                new List<string> { "OnFulfillmentChange" }));

            ListOfSearchings.Add(new gclassSearch(
                "HideoutInstance", "GClass",
                new List<string> { "GetAvailableItemsByFilter" },
                new List<string> { "ProductionSchemes" },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "LocationInfo", "GClass",
                new List<string> { "GetIconCoords" },
                new List<string> { "NightTimeAllowedLocations" },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "ConverterBucket", "GClass",
                new List<string> { },
                new List<string> { "Converters" },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "ClientMetrics", "GClass",
                new List<string> { },
                new List<string> { "GameUpdateBinMetricCollector" },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "ISpawnPoints", "GInterface",
                new List<string> { "DestroySpawnPoint" },
                new List<string> { },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "AmmoInfo", "GClass",
                new List<string> { },
                new List<string> { "AmmoLifeTimeSec" },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "Equipment", "GClass",
                new List<string> { "GetContainerSlots" },
                new List<string> {  },
                new List<string> { "AllSlotNames" }));

            ListOfSearchings.Add(new gclassSearch(
                "DamageInfo", "GStruct",
                new List<string> { },
                new List<string> { },
                new List<string> { "HittedBallisticCollider" }));

            ListOfSearchings.Add(new gclassSearch(
                "UI_Button", "",
                new List<string> { "SetHeaderText" },
                new List<string> { "HeaderText" },
                new List<string> { "OnClick" }));

            ListOfSearchings.Add(new gclassSearch(
                "MenuController", "",
                new List<string> { },
                new List<string> { "SelectedKeyCard" },
                new List<string> { }));

            ListOfSearchings.Add(new gclassSearch(
                "WeatherSettings", "GStruct",
                new List<string> { },
                new List<string> { },
                new List<string> { "IsRandomTime" }));

            ListOfSearchings.Add(new gclassSearch(
                "BotsSettings", "GStruct",
                new List<string> { },
                new List<string> { },
                new List<string> { "IsScavWars", "BotAmount" }));

            ListOfSearchings.Add(new gclassSearch(
                "WavesSettings", "GStruct",
                new List<string> { },
                new List<string> { },
                new List<string> { "IsTaggedAndCursed", "IsBosses" }));

            ListOfSearchings.Add(new gclassSearch(
                "MatchmakerScreenCreator", "GClass",
                new List<string> { },
                new List<string> { },
                new List<string> { "AIAmount", "AIDifficulty" }));
            #endregion
            List<string> fileList = Directory.GetFiles(directory).ToList();
            
            for (int i = 0; i < fileList.Count; i++) {
                string path = fileList[i];
                var assembly = ModuleDefMD.Load(path);
                var types = assembly.Types;
                if (DEBUG)
                    Console.WriteLine(types.Count());
                var findAll = types.Where(type => !type.Name.Contains("`") && type.Name.StartsWith("Class") && type.Fields.Count == 1 && type.Methods.Count == 1 && type.Properties.Count == 0).ToList();
                if (findAll.Count > 0 && findAll[0].Fields.Count > 0)
                    Console.WriteLine($"Assembly Game Version: {findAll[0].Fields[0].Constant.Value}");
                Console.WriteLine("Current => New\n");
                List<string> UsingList = new List<string>();
                List<string> InfoList = new List<string>();

                foreach (var gclassSearch in ListOfSearchings)
                {
                    int searching = 0;
                    string Output = "", FullOutput = "";

                    if (searchingForGClasses(types, ref searching, gclassSearch, ref Output, ref FullOutput))
                    {
                        UsingList.Add($"using {gclassSearch.OriginalName} = {FullOutput};");
                        InfoList.Add($"{gclassSearch.OriginalName} => {Output} [{FullOutput}]");
                    }
                }
                for (int j = 0; j < UsingList.Count; j++)
                {
                    Console.WriteLine(UsingList[j]);
                }
                Console.WriteLine("");
                if (DEBUG)
                    for (int k = 0; k < InfoList.Count; k++)
                    {
                        Console.WriteLine(InfoList[k]);
                    }

            }
            Console.ReadKey();


            Console.WriteLine("\nFinished... Waiting on key press...");
            Console.ReadKey();
        }
    }
}
