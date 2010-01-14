@echo off
call script.conf.bat
IF NOT EXIST script.conf.bat EXIT /B 2

:: Copy debug files to a common folder
set COPYCMD=xcopy /Y /s
%COPYCMD% %SRCDIR%\"Nova Common\bin\debug\NovaCommon.dll" %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova Common\bin\debug\NovaCommon.pdb" %DISTDIR%\
%COPYCMD% %SRCDIR%\"Control Library\bin\debug\ControlLibrary.dll" %DISTDIR%\
%COPYCMD% %SRCDIR%\"Control Library\bin\debug\ControlLibrary.pdb" %DISTDIR%\
%COPYCMD% %SRCDIR%\"ComponentEditor\bin\debug\ComponentEditor.exe" %DISTDIR%\
%COPYCMD% %SRCDIR%\"ComponentEditor\bin\debug\ComponentEditor.pdb" %DISTDIR%\
%COPYCMD% %SRCDIR%\"Race Designer\bin\debug\RaceDesigner.exe" %DISTDIR%\
%COPYCMD% %SRCDIR%\"Race Designer\bin\debug\RaceDesigner.pdb" %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova GUI\bin\debug\Nova GUI.exe" %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova GUI\bin\debug\Nova GUI.pdb" %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova Console\bin\debug\Nova Console.exe" %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova Console\bin\debug\Nova Console.pdb" %DISTDIR%\
%COPYCMD% %SRCDIR%\components.xml %DISTDIR%\

pause