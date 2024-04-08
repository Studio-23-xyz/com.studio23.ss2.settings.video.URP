
using System.IO;
using Studio23.SS2.Settings.Video.URP.Data;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.Settings.Video.URP.Editor
{
    public class URPGraphicsConfigurationEditor : MonoBehaviour
    {
        [MenuItem("Studio-23/Settings/Video/URPGraphicsConfiguration", false, 10)]
        static void CreateDefaultProvider()
        { 
            URPGraphicsConfiguration graphicsProviderConfig = ScriptableObject.CreateInstance<URPGraphicsConfiguration>();

            // Create the resource folder path
            string resourceFolderPath = "Assets/Resources/Settings/Video/PostProcess";

            if (!Directory.Exists(resourceFolderPath))
            {
                Directory.CreateDirectory(resourceFolderPath);
            }

            // Create the ScriptableObject asset in the resource folder
            string assetPath = resourceFolderPath + "/PostProcess.asset";
            AssetDatabase.CreateAsset(graphicsProviderConfig, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("URP Post Process created at: " + assetPath);
        }
    }
}
