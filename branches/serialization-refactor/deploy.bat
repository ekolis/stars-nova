@echo off
call script.conf.bat
IF NOT EXIST script.conf.bat EXIT /B 2

:: Copy release files to a common folder
set COPYCMD=xcopy /Y /s
%COPYCMD% %SRCDIR%\components.xml %DISTDIR%\
%COPYCMD% %SRCDIR%\Documentation\GettingStarted.html %DISTDIR%\
%COPYCMD% %SRCDIR%\Graphics\*.* %DISTDIR%\Graphics\
%COPYCMD% %SRCDIR%\ComponentEditor\bin\Release\*.* %DISTDIR%\ComponentEditor\
%COPYCMD% %SRCDIR%\"Race Designer"\bin\Release\*.* %DISTDIR%\"Race Designer"\
%COPYCMD% %SRCDIR%\"Nova GUI"\bin\Release\*.* %DISTDIR%\"Nova GUI"\
%COPYCMD% %SRCDIR%\"Nova Console"\bin\Release\*.* %DISTDIR%\"Nova Console"\

pause
