@echo off

If "%1"=="" (
	echo Usage: FindStudies [PatientId]
	goto :end    
)

findscu -v -P --aetitle DATAEXTR --call VMSDBD --key QueryRetrieveLevel=STUDY --key PatientID="%1" 140.166.155.90 57347 -k StudyInstanceUID -k StudyDate

:end