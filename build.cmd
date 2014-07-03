@ECHO OFF

(set MSBuildExe="c:\build\msbuild\MSBuild.exe")
(set BuildFile="build.proj")
(set BuildLog="build.log")

IF NOT EXIST %MSBuildExe% ( echo Cannot find MSBuild executable: %MSBuildExe% & Exit /b 1)

%MSBuildExe% /l:FileLogger,Microsoft.Build.Engine;logfile=%BuildLog%;verbosity=diagnostic;encoding=utf-8 %BuildFile%
(set BUILD_ERRORLEVEL=%ERRORLEVEL%)

MOVE %BuildLog% dist\
Exit /b %BUILD_ERRORLEVEL%
