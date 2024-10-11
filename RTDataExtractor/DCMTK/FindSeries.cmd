@echo off

If "%1"=="" (
	echo Usage: FindSeries [PatientId]
	goto :end    
)

findscu -v -P --aetitle DATAEXTR --call VMSDBD --key QueryRetrieveLevel=SERIES --key PatientID="%1" 140.166.155.90 57347 -k SeriesInstanceUID -k SeriesDate

:end