#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public static class Tool_RemoveMissingComponents
{
    /// <summary>
    /// Removes missing scripts from the currently selected GameObject in the Hierarchy.
    /// </summary>
    [MenuItem("GameObject/Remove Missing Scripts")]
    public static void RemoveMissingScriptsFromSelected()
    {
        // Get the currently selected GameObject
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            Debug.LogError("No GameObject is currently selected. Please select a GameObject in the Hierarchy.");
            return;
        }

        // Remove missing scripts from the selected GameObject and its children recursively
        int removedCount = RemoveMissingScriptsRecursively(selectedObject);

        if (removedCount > 0)
        {
            Debug.Log($"Removed {removedCount} missing scripts from {selectedObject.name}");
        }
        else
        {
            Debug.Log("No missing scripts found in the selected GameObject.");
        }
    }

    /// <summary>
    /// Recursively removes missing scripts from a GameObject and its children.
    /// </summary>
    /// <param name="gameObject">The GameObject to check and modify.</param>
    /// <returns>The number of missing scripts removed.</returns>
    private static int RemoveMissingScriptsRecursively(GameObject gameObject)
    {
        int removedCount = 0;

        // Remove missing scripts from the current GameObject
        removedCount += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);

        // Recursively check and remove missing scripts from children
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            removedCount += RemoveMissingScriptsRecursively(gameObject.transform.GetChild(i).gameObject);
        }

        return removedCount;
    }
}

public static class Tool_RemoveMissingComponent
{
    /// <summary>
    /// DOES :
    /// Remove missing scripts in prefabs found at PATH, then save prefab.
    /// Saved prefab will have no missing scripts left.
    /// Will not mod prefabs that dont have missing scripts.
    ///
    /// NOTE :
    /// If prefab has another prefab#2 that is not in PATH, that prefab#2 will still have missing scripts.
    /// The instance of the prefab#2 in prefab will not have missing scripts (thus counted has override of prefab#2)
    ///
    /// HOW TO USE :
    /// Copy code in script anywhere in project.
    /// Set the PATH var in method <see cref="RemoveMissingScripstsInPrefabsAtPath"/>.
    /// Clik the button.
    /// </summary>

    [MenuItem("Tools/FixingStuff/Remove MissingComponents in Prefabs at Path")]
    public static void RemoveMissingScripstsInPrefabsAtPath()
    {
        string PATH = "Assets";

        EditorUtility.DisplayProgressBar("Modify Prefab", "Please wait...", 0);
        string[] ids = AssetDatabase.FindAssets("t:Prefab", new string[] { PATH });
        for (int i = 0; i < ids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(ids[i]);
            GameObject prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            int delCount = 0;
            RecursivelyModifyPrefabChilds(instance, ref delCount);

            if (delCount > 0)
            {
                Debug.Log($"Removed({delCount}) on {path}", prefab);
                PrefabUtility.SaveAsPrefabAssetAndConnect(instance, path, InteractionMode.AutomatedAction);
            }

            UnityEngine.Object.DestroyImmediate(instance);
            EditorUtility.DisplayProgressBar("Modify Prefab", "Please wait...", i / (float)ids.Length);
        }
        AssetDatabase.SaveAssets();
        EditorUtility.ClearProgressBar();
    }

    private static void RecursivelyModifyPrefabChilds(GameObject obj, ref int delCount)
    {
        if (obj.transform.childCount > 0)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                var _childObj = obj.transform.GetChild(i).gameObject;
                RecursivelyModifyPrefabChilds(_childObj, ref delCount);
            }
        }

        int innerDelCount = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(obj);
        delCount += innerDelCount;
    }

}

public static class Tool_RemoveMissingScriptsFromScenes
{
    /// <summary>
    /// Removes missing scripts from all GameObjects in all scenes found in the specified folder path.
    /// </summary>
    [MenuItem("Tools/FixingStuff/Remove Missing Scripts from Scenes in Path")]
    public static void RemoveMissingScriptsInScenesAtPath()
    {
        string folderPath = "Assets/Scenes"; // Specify your folder path here

        // Find all the scene files in the specified folder
        string[] scenePaths = AssetDatabase.FindAssets("t:Scene", new[] { folderPath });
        
        EditorUtility.DisplayProgressBar("Removing Missing Scripts", "Processing scenes...", 0);

        for (int i = 0; i < scenePaths.Length; i++)
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(scenePaths[i]);
            Debug.Log($"Processing scene: {scenePath}");

            // Open the scene
            EditorSceneManager.OpenScene(scenePath);

            // Get all root objects in the current scene
            GameObject[] rootGameObjects = GameObject.FindObjectsOfType<GameObject>(true);
            int removedCount = 0;

            foreach (var rootObject in rootGameObjects)
            {
                removedCount += RemoveMissingScriptsRecursively(rootObject);
            }

            if (removedCount > 0)
            {
                Debug.Log($"Removed {removedCount} missing scripts from {scenePath}");
                // Save the scene after the modifications
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            }
            else
            {
                Debug.Log($"No missing scripts found in {scenePath}.");
            }

            EditorUtility.DisplayProgressBar("Removing Missing Scripts", $"Processing {i + 1}/{scenePaths.Length}...", (i + 1) / (float)scenePaths.Length);
        }

        EditorUtility.ClearProgressBar();
        AssetDatabase.SaveAssets();
        Debug.Log("Finished removing missing scripts from all scenes.");
    }

    /// <summary>
    /// Recursively removes missing scripts from a GameObject and its children.
    /// </summary>
    /// <param name="gameObject">The GameObject to check and modify.</param>
    /// <returns>The number of missing scripts removed.</returns>
    private static int RemoveMissingScriptsRecursively(GameObject gameObject)
    {
        int removedCount = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);

        // Recursively check and remove missing scripts from children
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            removedCount += RemoveMissingScriptsRecursively(gameObject.transform.GetChild(i).gameObject);
        }

        return removedCount;
    }
}
#endif