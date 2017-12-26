#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var artifactsDir  = Directory("./artifacts/");
var rootAbsoluteDir = MakeAbsolute(Directory("./")).FullPath;

var report = Directory("./reports");
var xunitReport = report + Directory("xunit");

var slnProject= "./EifelMono.Core.sln";

Task("Clean")
    .Does(() =>
{
    CleanDirectory(artifactsDir);
});

Task("Restore")
  .Does(()=> {
      NuGetRestore(slnProject);
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