@echo off

If "%1"=="" (
	echo Usage: QuerySerie [Series Instance UID]
	goto :end    
)

movescu -d --aetitle DATAEXTR --call VMSDBDR --move DATAEXTR --port 104 --key QueryRetrieveLevel=SERIES --key SeriesInstanceUID="%1" 140.166.155.89 51402

:end