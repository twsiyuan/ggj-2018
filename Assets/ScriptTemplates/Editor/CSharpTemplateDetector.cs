using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class CSharpTemplateDetector : UnityEditor.AssetModificationProcessor
{
    static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if(Path.GetExtension(path) != ".cs")
            return;

        var index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;

        var fileName = Path.GetFileNameWithoutExtension(path);

        var file = File.ReadAllText(path);
        if(ReplaceKeyword(fileName, ref file)) {
            File.WriteAllText(path, file, Encoding.UTF8);
            AssetDatabase.Refresh();
        }
    }


    static bool ReplaceKeyword(string fileName, ref string script)
    {
        var modified = false;
        if(script.Contains("#TARGETSCRIPTNAME#")) {
            script = script.Replace("#TARGETSCRIPTNAME#", fileName.Replace("Editor", ""));
            modified = true;
        }

        return modified;
    }

}