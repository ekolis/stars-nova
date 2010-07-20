@echo off
SET LOGFILE=post-build.log
echo Running Post-build Scripts > %LOGFILE%
echo ProjectDir = %1 >> %LOGFILE%
echo Configuration = %2 >> %LOGFILE%
echo BuiltOuputPath = %3 >> %LOGFILE%

cscript.exe %1AddVersionToFileName.vbs %1 %2 %3 >> %LOGFILE% 2>&1

del %1%2\setup.exe >> post-build.log >> %LOGFILE% 2>&1