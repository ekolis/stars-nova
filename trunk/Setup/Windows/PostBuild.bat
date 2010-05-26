@echo off
SET LOGFILE=post-build.log
echo Post-build script running > %LOGFILE%
echo ProjectDir = %1 >> %LOGFILE%
echo Configuration = %2 >> %LOGFILE%
echo BuiltOuputPath = %3 >> %LOGFILE%

cscript.exe %1AddVersionToFileName.vbs %1 %2 %3 >> %LOGFILE%

del %1%2\setup.exe >> post-build.log >> %LOGFILE%
move %3 %1%2\stars-nova-0.1.2-windows.msi >> %LOGFILE%
