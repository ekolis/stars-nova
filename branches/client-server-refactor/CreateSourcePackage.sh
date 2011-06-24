#!/bin/bash
#
# REQUIRES:
# - SVN command line client
# - GNU tar

if [ $# -lt 2 ]; then
	echo Error: Missing command line parameters
	echo
	echo "Command line: $0 <svn path> <version>"
	echo Example: $0 /branches/0.4 0.4.5
	exit
fi 

EXPORTPATH=stars-nova-${2}-source/
PACKAGFILE=stars-nova-${2}-source.tar.gz
LOGFILE=CreateSourcePackage.log
cd Build/

echo Running Source Package Script > $LOGFILE

if [ -d "$EXPORTPATH" ]; then
        rm -rf $EXPORTPATH >> $LOGFILE
fi
echo Exporting $1 from Subversion to Build/${EXPORTPATH}...
svn export https://stars-nova.svn.sourceforge.net/svnroot/stars-nova${1} $EXPORTPATH >> $LOGFILE

echo Creating $PACKAGFILE...
if [ -f "$PACKAGFILE" ]; then 
        rm $PACKAGFILE >> $LOGFILE
fi

tar -czf $PACKAGFILE $EXPORTPATH >> $LOGFILE

if [ -f "$PACKAGFILE" ]; then
        echo Successfully created source package
else
        echo Failed to create source package
fi

