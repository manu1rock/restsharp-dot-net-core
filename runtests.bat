rem **starting test execution from batch*******
set mypath=%cd%
@echo %mypath%

@echo off
set /P TestCatVar=TestCategory? 

if not defined %TestCatVar% start dotnet test --logger trx else (
        start dotnet test --filter TestCategory=%TestCatVar% --logger trx
)
pause

rem **test execution completed*******