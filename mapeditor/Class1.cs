using HarmonyLib;
using Il2CppAssets.Scripts.Models.MapEditorBehaviors;
using Il2CppAssets.Scripts.Models.ServerEvents;
using Il2CppAssets.Scripts.Simulation.MapEditorBehaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Analytics;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.Player;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppNewtonsoft.Json;
using MelonLoader;
using UnityEngine;

namespace mod
{
    public class Class1 : MelonMod
    {
        static MapPropManager? manager;

        static Vector3 rotationVector = new Vector3(-30, 0, 0); // make prop perfectly face camera

        static List<MapProp> props = new List<MapProp>();

        static int frame = 0;
        static string the_fucking_video = "";

        static int xCount = 44;
        static int yCount = 56;

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Input.GetKeyDown(KeyCode.F9))
            {
                frame = 0;
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse1)) // right click
            {
                var props = InGame.instance.bridge.GetAllProps();
                int i = 0;
                foreach (PropToSimulation prop in props)
                {
                    if (the_fucking_video[frame * 2420 + i] == '1')
                    {
                        prop.ScaleProp(1 / 1000);
                    }
                    else
                    {
                        prop.ScaleProp(50f);
                    }
                    i++;
                }
                frame++;
            }

        }

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            try
            {
                the_fucking_video = File.ReadAllText("C:\\...\\bad.txt");
                Console.WriteLine("File Read!");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("An error occurred while reading the file: " + ex.Message);
            }

        }


        [HarmonyLib.HarmonyPatch(typeof(MapPropManager), "CreateMapProp")]
        public class c1
        {
            [HarmonyLib.HarmonyPostfix]
            public static void Postfix(MapPropModel def, PositionalData pd, int inputIndex, PropSaveDataModel loadingSaveData, float rotation, bool playPlacementEffects, MapPropManager __instance)
            {
                if (inputIndex != -1) return;

                manager = __instance;

                for (float x = -125; x < 125; x += 1.9f * 3)
                {
                    for (float y = 106; y > -106; y -= 1.3f * 3)
                    {
                        PositionalData positionalData = new PositionalData();
                        positionalData.position = new Vector3(x, 0, y);
                        positionalData.rotation = rotationVector;
                        positionalData.scale = 0.5f;

                        MapProp mapProp = manager.CreateMapProp(def, positionalData, -2, null, -30, false);
                        props.Add(mapProp);
                    }
                }
            }
        }
    }
}
