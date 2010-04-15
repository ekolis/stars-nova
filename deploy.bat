@echo off
call script.conf.bat
IF NOT EXIST script.conf.bat EXIT /B 2

:: Copy release files to a common folder
set COPYCMD=xcopy /Y /s
%COPYCMD% %SRCDIR%\ComponentEditor\bin\Release\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\NewGame\bin\Release\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova AI"\bin\Release\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova Console"\bin\Release\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\"Nova GUI"\bin\Release\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\NovaLauncher\bin\Release\*.* %DISTDIR%\
%COPYCMD% %SRCDIR%\"Race Designer"\bin\Release\*.* %DISTDIR%\

%COPYCMD% %SRCDIR%\*.txt %DISTDIR%\
%COPYCMD% %SRCDIR%\Documentation\GettingStarted.html %DISTDIR%\
%COPYCMD% %SRCDIR%\components.xml %DISTDIR%\
%COPYCMD% %SRCDIR%\Graphics\*.* %DISTDIR%\Graphics\

pause
