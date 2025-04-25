using UnityEditor;
using UnityEditor.Build.Reporting;
using System;
using System.IO;
using System.Linq;

public class BuildScript
{
    // iOSビルド用メソッド
    public static void BuildIOS()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/iOS";
        buildPlayerOptions.target = BuildTarget.iOS;
        buildPlayerOptions.options = BuildOptions.None;

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

    // Androidビルド用メソッド
    public static void BuildAndroid()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/Android/app.apk"; // 出力APKのパス
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Console.WriteLine("Android build succeeded: " + summary.totalSize + " bytes");
        }
        else
        {
            Console.WriteLine("Android build failed");
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