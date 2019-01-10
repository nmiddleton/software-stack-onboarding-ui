rem DELETE RUBBISH
rd /S /Q TestResults
dir /A:D /B >temp.txt
for /F %%i in (temp.txt) do rd /S /Q %%i\bin
for /F %%i in (temp.txt) do rd /S /Q %%i\obj
del temp.txt

rem CREATE ARCHIVE
for /F "tokens=1-3 delims=/ " %%a in ('DATE/T') do set dat=%%c-%%b-%%a
for /F "tokens=1-2 delims=: " %%b in ('TIME/T') do set tim=%%b%%c
set name="TR.Profile %dat% %tim%"
zip -r -9 %name% *.*

rem pause "Press any key"