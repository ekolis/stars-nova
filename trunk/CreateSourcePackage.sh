#!/bin/bash
#
# REQUIRES:
# - SVN command line client
# - GNU tar

EXPORTPATH=stars-nova-source/
PACKAGFILE=stars-nova-source.tar.gz
LOGFILE=CreateSourcePackage.log
cd Build/

echo Running Source Package Script > $LOGFILE

if [ -d "$EXPORTPATH" ]; then
        rm -rf $EXPORTPATH >> $LOGFILE
fi
echo Exporting /trunk from Subversion to Build/${EXPORTPATH}...
svn export https://stars-nova.svn.sourceforge.net/svnroot/stars-nova/trunk $EXPORTPATH >> $LOGFILE

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

