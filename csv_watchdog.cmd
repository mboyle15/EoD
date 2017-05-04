@ECHO OFF

IF EXIST "z:\AS162.csv" (
	C:\Python27\python.exe C:\EoD\EoDParser.py
	timeout /t 10 /nobreak > NUL
	del /f z:\AS162.csv
	EXIT /B
) ELSE (
	EXIT /B
)