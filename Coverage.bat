@ECHO OFF

dotnet test MyRestaurant.Api.Tests/MyRestaurant.Api.Tests.csproj --logger "trx;LogFileName=MyRestaurant.Api.Test.Results.trx" --logger "xunit;LogFileName=MyRestaurant.Api.Test.Results.xml" --results-directory ./CoverageReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=%CD%/CoverageReports/Coverage/MyRestaurant.Api.Test.Results.coverage.cobertura.xml /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit.*]"

%UserProfile%\.nuget\packages\reportgenerator\4.8.5\tools\net5.0\ReportGenerator.exe "-reports:%CD%\CoverageReports\Coverage\*.cobertura.xml" "-targetdir:CoverageReports\Coverage" -reporttypes:HTML

start %CD%\CoverageReports\Coverage\index.html