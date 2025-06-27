using System.Collections.Generic;
using System.IO;
using Jenny;

public class CustomOutputPathPostProcessor : IPostProcessor
{
    public string Name => "CustomOutputPathPostProcessor";
    public int Order { get; } = 199;
    public bool RunInDryMode { get; } = true;

    public CodeGenFile[] PostProcess(CodeGenFile[] files)
    {
        List<CodeGenFile> newFiles = new  List<CodeGenFile>();
        foreach (var file in files)
        {
            if (file.FileContent.Contains("UnityEngine"))
            {
                continue;
            }
            newFiles.Add(file);
        }

        return newFiles.ToArray();
    }
}