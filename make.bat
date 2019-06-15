@echo off

if not exist ".\Thirdparty\Advanced Combat Tracker.exe" (
	echo [Error]
	echo  In order to execute the build, you need to copy "Advanced Combat Tracker.exe" to the "Thirdparty" directory.
	goto END
)

set DOTNET_PATH=%windir%\Microsoft.NET\Framework\v4.0.30319
if not exist %DOTNET_PATH% (
	echo [Error]
	echo  The .NET Framework directory ^(%DOTNET_PATH%^) can not be found.
	echo  In order to execute build, .NET Framework 4.5.1 ^(or higher?^) must be installed.
	goto END
)


%DOTNET_PATH%\msbuild /t:Rebuild /p:Configuration=Release /p:OutputPath=..\Build RainbowMage.ActLocalizer.sln


:END
pause
