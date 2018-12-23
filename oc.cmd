@ECHO OFF

SET NugetBase=%UserProfile%\.nuget
SET OpenCover="%NugetBase%\packages\opencover\4.6.519\tools\OpenCover.Console.exe"
SET dotnet=%ProgramFiles%\dotnet\dotnet.exe

%OpenCover% -register:user -target:"%dotnet%" -targetargs:test -filter:"+[Greety*]* -[Greety.Tests*]*" -output:coverage.xml -oldstyle