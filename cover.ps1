PUSHD Quattro.Tests

$TestOutput = dotnet test --collect "XPlat Code Coverage"
$TestReports = $TestOutput | Select-String coverage.cobertura.xml | ForEach-Object { $_.Line.Trim() } | Join-String -Separator ';'
.\tools\reportgenerator.exe "-reports:$TestReports" -targetdir:coverage -reporttypes:Html
explorer coverage\index.htm

POPD