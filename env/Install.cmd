@echo off
SET WWWDIR=d:\WWW
SET APP=GMS_PROFILE
SET DATADIR=%WWWDIR%\%APP%\Profiles
set ERRORCODE=0

REM Set up a timestamp
for /F "tokens=1-3 delims=/ " %%a in ('DATE/T') do set DATESTAMP=%%c-%%b-%%a
for /F "tokens=1-2 delims=: " %%b in ('TIME/T') do set TIMESTAMP=%%b%%c

REM BACKUP Existing Solution so it is not lost
echo Archive Existing Solution ...
echo ==============================================
set zipname="d:\Backup\WebApp %APP% %DATESTAMP% %TIMESTAMP%"
if exist %WWWDIR%\%APP% ..\zip\7z.exe a -r -sfx %zipname% %WWWDIR%\%APP%\*.* && echo Archived to %zipname%.
if NOT exist %WWWDIR%\%APP% echo There is no existing Solution in %WWWDIR%\%APP%
echo Done archiving.
echo ==============================================
echo.
REM COPY Existing Profile Backups so they are in the new deployement as well
echo Collecting Existing XML profiles ...
echo ==============================================
pause
if EXIST %DATADIR% xcopy %DATADIR% %~dp0..\Solution\WWW\%APP%\Profiles /c/e/f/i/r/z/y
echo Done collecting XMLs...
echo ==============================================
echo.
REM REMOVE Existing solution
echo Deleting Existing Solution directory %WWWDIR%\%APP%
echo ==============================================
pause
if EXIST %WWWDIR%\%APP% rm -rf %WWWDIR%\%APP%
echo Done deleting Solution
echo ==============================================
echo.
REM Install Existing solution
echo Installing Solution to directory %WWWDIR%\%APP%
echo ==============================================
pause
if EXIST %WWWDIR%\%APP% set BAD_DIR=%WWWDIR%\%APP% && goto DIRNOTEMPTY 
if NOT EXIST %WWWDIR%\%APP% mkdir %WWWDIR%\%APP%
echo Directory is ready for install, Installing ...
xcopy %~dp0..\Solution\WWW\* %WWWDIR%\%APP% /c/e/f/i/r/z/y
echo Installed. Applying site specific configuration..
xcopy %~dp0web.config %WWWDIR%\%APP% /c/f/i/r/z/y
echo Done Installing Solution. Exiting.
echo ==============================================
goto END

:DIRNOTEMPTY
echo ERROR Directory not empty, clean up the Directory [%BAD_DIR%] before installing...
set ERRORCODE=1 
goto END

:END
echo Exiting...
pause
exit /b %ERRORCODE%