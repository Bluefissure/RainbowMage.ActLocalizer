@echo off

if not exist ".\Thirdparty\Advanced Combat Tracker.exe" (
	echo [�G���[]
	echo  �r���h�����s���邽�߂ɂ́A"Thirdparty"�f�B���N�g����"Advanced Combat Tracker.exe"���R�s�[����K�v������܂��B
	goto END
)

set DOTNET_PATH=%windir%\Microsoft.NET\Framework\v4.0.30319
if not exist %DOTNET_PATH% (
	echo [�G���[]
	echo  .NET Framework�̃f�B���N�g���i%DOTNET_PATH%�j��������܂���B
	echo  �r���h�����s���邽�߂ɂ�.NET Framework 4.5.1���C���X�g�[������Ă���K�v������܂��B
	goto END
)


%DOTNET_PATH%\msbuild /t:Rebuild /p:Configuration=Release /p:OutputPath=..\Build RainbowMage.ActLocalizer.sln


:END
pause
