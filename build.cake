#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var artifactsDir  = Directory("./artifacts/");
var rootAbsoluteDir = MakeAbsolute(Directory("./")).FullPath;

var report = Directory("./reports");
var xunitReport = report + Directory("xunit");

var slnProject= "./EifelMono.Core.sln";



Task("Restore")
    .Does(()=> {
        NuGetRestore(slnProject);
 });

 Task("NuGetClear")
    .Does(() =>
{
    CleanDirectories("./**/bin/Release");
});

 Task("NuGetPacK")
    .IsDependentOn("NuGetClear")
    .IsDependentOn("BuildRelease")
    .Does(()=> {
        MSBuild(slnProject, 
            new MSBuildSettings()
                .SetConfiguration(configuration)
                .SetVerbosity(Verbosity.Minimal));

        CreateDirectory("./../FileNuGet");    
        foreach(var file in GetFiles("./**/bin/Release/*.nupkg"))
            CopyFile(file, "./../FileNuGet/"+ file.GetFilename());
 });

 Task("BuildRelease")
  .IsDependentOn("Restore")
  .Does(()=> {
      MSBuild(slnProject, 
        new MSBuildSettings()
            .SetConfiguration("Release")
            .SetVerbosity(Verbosity.Minimal));
 });

 Task("Build")
  .IsDependentOn("Restore")
  .Does(()=> {
      MSBuild(slnProject, 
        new MSBuildSettings()
            .SetConfiguration(configuration)
            .SetVerbosity(Verbosity.Minimal));
 });

Task("Default")
.Does(() => {
   Information("Hello Cake!");
});

RunTarget(target);