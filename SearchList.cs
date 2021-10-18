using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_Assemblies
{
    class SearchList
    {
        public class Searcher
        {
            public Searcher(string originalName, string startingWith, List<string> MethodsToLookFor = null, List<string> PropertiesToLookFor = null, List<string> FieldsToLookFor = null)
            {
                OriginalName = originalName;
                StartingWith = startingWith;
                if (MethodsToLookFor != null)
                    methodsItContain = MethodsToLookFor;
                if (PropertiesToLookFor != null)
                    propertiesItContain = PropertiesToLookFor;
                if (FieldsToLookFor != null)
                    fieldsItContain = FieldsToLookFor;
            }
            public int CountMatches
            {
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

        internal static List<Searcher> Create()
        {
            List<Searcher> ListOfSearchings = new List<Searcher>();
            #region Adding Searching Names
            ListOfSearchings.Add(new Searcher(
                "MainMenuController", "GClass",
                new List<string> { "OnProfileChangeApplied", "ShowScreen" },
                new List<string> { "SelectedLocation", "InventoryController" }));

            ListOfSearchings.Add(new Searcher(
                "IHealthController", "GInterface",
                new List<string> { "GetBodyPartsInCriticalCondition", "AddImmunityNotificationEffect", "PropagateAllEffects" },
                new List<string> { "DamageCoeff", "TemperatureRate", "CarryingWeightRelativeModifier" }));

            ListOfSearchings.Add(new Searcher(
                "StDamage", "GStruct",
                new List<string> { "GetOverDamage" },
                new List<string> { "Blunt" },
                new List<string> { "BlockedBy", "DidBodyDamage", "DidArmorDamage" }));

            ListOfSearchings.Add(new Searcher(
                "IEffect", "GInterface",
                new List<string> { "AddWholeTime", "Propagate" },
                new List<string> { "DisplayableVariations", "CurStateTimeLeft" }));

            ListOfSearchings.Add(new Searcher(
                "ISession", "GInterface",
                new List<string> { "GetPhpSessionId" }));

            ListOfSearchings.Add(new Searcher(
                "ClientConfig", "GClass",
                new List<string> { "LoadApplicationConfig" }));

            ListOfSearchings.Add(new Searcher(
                "ClientConfig", "GClass",
                new List<string> { "LoadApplicationConfig" },
                new List<string> { "BackendCacheDir", "ConfigFilePath", "BackendCacheDir", "BackendUrl" },
                new List<string> { "DEFAULT_BACKEND_URL" }));

            ListOfSearchings.Add(new Searcher(
                "IBundleLock", "GInterface",
                new List<string> { "Lock", "Unlock" },
                new List<string> { "IsLocked" }));

            ListOfSearchings.Add(new Searcher(
                "BindableState(gclass+1)", "GClass",
                new List<string> { "BindWithoutValue", "Unbind" },
                new List<string> { "Value" },
                new List<string> { "ChangedEvents" }));

            ListOfSearchings.Add(new Searcher(
                "BotDifficultyHandler", "GClass",
                new List<string> { "CheckOnExcude", "LoadDifficultyStringInternal" },
                new List<string> { },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "WaveInfo", "GClass",
                new List<string> { },
                new List<string> { },
                new List<string> { "Role", "Limit", "Difficulty" }));

            ListOfSearchings.Add(new Searcher(
                "BotsPresets", "GClass",
                new List<string> { "GetNewProfile", "method_0" },
                new List<string> { },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "BotData", "GInterface",
                new List<string> { "ChooseProfile", "CanAtZoneByType", "ShallChooseByData", "IsBossOrFollowerByTime", "GetDebugData" },
                new List<string> { "SpawnParams" },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "PoolManager", "GClass",
                new List<string> { "CreateCleanLootPrefabAsync", "LoadBundlesAndCreatePools" },
                new List<string> { "PoolsCancellationToken" },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "JobPriority", "GClass",
                new List<string> { "GetYieldDelegate" },
                new List<string> { "General", "Immediate", "Low" },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "Requirement", "GClass",
                new List<string> { },
                new List<string> { "Fulfilled", "SourceAreaLevel", "SourceAreaType", "Type" },
                new List<string> { "OnFulfillmentChange" }));

            ListOfSearchings.Add(new Searcher(
                "HideoutInstance", "GClass",
                new List<string> { "GetAvailableItemsByFilter" },
                new List<string> { "ProductionSchemes" },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "LocationInfo", "GClass",
                new List<string> { "GetIconCoords" },
                new List<string> { "NightTimeAllowedLocations" },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "ConverterBucket", "GClass",
                new List<string> { },
                new List<string> { "Converters" },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "ClientMetrics", "GClass",
                new List<string> { },
                new List<string> { "GameUpdateBinMetricCollector" },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "ISpawnPoints", "GInterface",
                new List<string> { "DestroySpawnPoint" },
                new List<string> { },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "AmmoInfo", "GClass",
                new List<string> { },
                new List<string> { "AmmoLifeTimeSec" },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "Equipment", "GClass",
                new List<string> { "GetContainerSlots" },
                new List<string> { },
                new List<string> { "AllSlotNames" }));

            ListOfSearchings.Add(new Searcher(
                "DamageInfo", "GStruct",
                new List<string> { },
                new List<string> { },
                new List<string> { "HittedBallisticCollider" }));

            ListOfSearchings.Add(new Searcher(
                "UI_Button", "",
                new List<string> { "SetHeaderText" },
                new List<string> { "HeaderText" },
                new List<string> { "OnClick" }));

            ListOfSearchings.Add(new Searcher(
                "MenuController", "",
                new List<string> { },
                new List<string> { "SelectedKeyCard" },
                new List<string> { }));

            ListOfSearchings.Add(new Searcher(
                "WeatherSettings", "GStruct",
                new List<string> { },
                new List<string> { },
                new List<string> { "IsRandomTime" }));

            ListOfSearchings.Add(new Searcher(
                "BotsSettings", "GStruct",
                new List<string> { },
                new List<string> { },
                new List<string> { "IsScavWars", "BotAmount" }));

            ListOfSearchings.Add(new Searcher(
                "WavesSettings", "GStruct",
                new List<string> { },
                new List<string> { },
                new List<string> { "IsTaggedAndCursed", "IsBosses" }));

            ListOfSearchings.Add(new Searcher(
                "MatchmakerScreenCreator", "GClass",
                new List<string> { },
                new List<string> { },
                new List<string> { "AIAmount", "AIDifficulty" }));
            #endregion
            return ListOfSearchings;
        }
        internal static bool GClassSearcher(IList<TypeDef> types, ref int searching, SearchList.Searcher searchClass, ref string Output, ref string FullOutput)
        {
            var FilteredTypes = types.Where(type => type.Name.StartsWith(searchClass.StartingWith));
            foreach (TypeDef type in FilteredTypes)
            {
                foreach (var method in type.Methods)
                {
                    if (searchClass.methodsItContain.Contains(method.Name))
                    {
                        searching++;
                        if (Program.DEBUG)
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
                        if (Program.DEBUG)
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
                        if (Program.DEBUG)
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
    }
}
