'Copy installation files to a folder on the desktop
set target=C:\Users\Dan\Desktop\Nova
set source="D:\dan\src\stars\nova\Archive\2009-06-09 nova source merge"
set mycopy=xcopy /Y /s
%mycopy% %source%\components.xml %target%\
%mycopy% %source%\GettingStarted.html %target%\
%mycopy% %source%\Graphics\*.* %target%\Graphics\
%mycopy% %source%\ComponentEditor\bin\Release\*.* %target%\ComponentEditor\
%mycopy% %source%\"Race Designer"\bin\Release\*.* %target%\"Race Designer"\
%mycopy% %source%\"Nova GUI"\bin\Release\*.* %target%\"Nova GUI"\
%mycopy% %source%\"Nova Console"\bin\Release\*.* %target%\"Nova Console"\

pause
