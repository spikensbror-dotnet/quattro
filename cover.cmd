@ECHO OFF

PUSHD Quattro.Tests

dotnet test --collect:cobertura
REM Previous line will generate coverage.cobertura.xml
tools\reportgenerator.exe -reports:coverage.cobertura.xml -targetdir:coverage -reporttypes:Html
explorer coverage\index.htm

POPD