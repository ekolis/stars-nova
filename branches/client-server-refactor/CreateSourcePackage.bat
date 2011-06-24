@echo off
:: REQUIRES:
:: - SVN command line client in your path (svn.exe)
:: - 7zip command line version in your path (7za.exe)

SET EXPORTPATH=stars-nova-source
SET PACKAGFILE=stars-nova-source.zip
SET LOGFILE=CreateSourcePackage.log
cd Build

echo Running Source Package Script > %LOGFILE%

IF EXIST %EXPORTPATH% RMDIR /S /Q %EXPORTPATH% >> %LOGFILE%
echo Exporting /trunk from Subversion to Build\%EXPORTPATH%...
svn export https://stars-nova.svn.sourceforge.net/svnroot/stars-nova/trunk %EXPORTPATH% >> %LOGFILE%

echo Creating %PACKAGFILE%...
IF EXIST %PACKAGFILE% DEL /Q %PACKAGFILE% >> %LOGFILE%
7za a %PACKAGFILE% %EXPORTPATH% >> %LOGFILE%

IF EXIST %PACKAGFILE% echo Successfully created source package

pause