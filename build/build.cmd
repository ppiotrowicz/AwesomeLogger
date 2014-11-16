echo off
SETLOCAL

SET msbuild="C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
SET project_dir=%~dp0..

SET CONFIGURATION=Release

%msbuild% /verbosity:minimal /m /p:Configuration=%CONFIGURATION% /t:Rebuild %project_dir%\AwesomeTestLogger.sln

xcopy /y %project_dir%\output\AwesomeTestLogger.dll "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\Extensions\"

ENDLOCAL
