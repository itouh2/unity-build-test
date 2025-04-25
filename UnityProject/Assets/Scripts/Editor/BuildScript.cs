using UnityEditor;
using UnityEditor.Build.Reporting;
using System;
using System.IO;
using System.Linq; // 重要：GetScenePathsメソッドで必要

public class BuildScript
{
    // iOSビルド用メソッド
    public static void BuildIOS()
    {
        // ビルド設定
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/iOS";
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.None;

        // ビルド実行
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Console.WriteLine("iOS build succeeded: " + summary.totalSize + " bytes");
        }
        else
        {
            Console.WriteLine("iOS build failed");
            EditorApplication.Exit(1);
        }
    }

    // シーンパスを取得するヘルパーメソッド
    private static string[] GetScenePaths()
    {
        return EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();
    }
}