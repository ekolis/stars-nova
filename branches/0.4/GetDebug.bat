@echo off
call script.conf.bat
IF NOT EXIST script.conf.bat EXIT /B 2

:: Copy debug files to a common folder
set COPYCMD=xcopy /Y /s
%COPYCMD% %SRCDIR%\ComponentEditor\bin\Debug\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\NewGame\bin\Debug\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova AI"\bin\Debug\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova Console"\bin\Debug\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova GUI"\bin\Debug\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\NovaLauncher\bin\Debug\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\"Race Designer"\bin\Debug\*.* %DISTDIR%\

%COPYCMD% %SRCDIR%\components.xml %DISTDIR%\
%COPYCMD% %SRCDIR%\Graphics\*.* %DISTDIR%\Graphics\

pause