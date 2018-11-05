@echo off
set ServiceName="CourtProsService"
set ExeName=JIC.Fault.WindowsService.Court.exe
:question
set /p Quest="You want to Install[I]/Uninstall[U]/Quit[Q] "

if "%Quest%"=="I" (goto :install)
if "%Quest%"=="i" (goto :install)		
if "%Quest%"=="U" (goto :uninstall)	
if "%Quest%"=="u" (goto :uninstall)
if "%Quest%"=="Q" (goto :quit)
if "%Quest%"=="q" (goto :quit)
echo "Wrong Answer"
goto :question

goto :quit

:install
SC CREATE %ServiceName% binpath= "%~dp0%ExeName%"
goto :quit
:uninstall
SC Delete %ServiceName%
goto :quit


:quit
set /p Quest="Bye Bye AG"
exit

