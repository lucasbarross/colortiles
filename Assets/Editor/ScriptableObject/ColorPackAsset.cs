using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif

public class ColorPackAsset
{
    [MenuItem("Assets/Create/Color Pack")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<ColorPack>();
    }
}