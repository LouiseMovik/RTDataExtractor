@echo off

If "%1"=="" (
	echo Usage: QuerySerie [Series Instance UID]
	goto :end    
)

movescu -d --aetitle DATAEXTR --call VMSDBD --move DATAEXTR --port 104 --key QueryRetrieveLevel=SERIES --key SeriesInstanceUID="%1" 140.166.155.90 57347

:end