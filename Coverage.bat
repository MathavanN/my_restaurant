@ECHO OFF

dotnet test MyRestaurant.Services.Tests/MyRestaurant.Services.Tests.csproj --logger "xunit;LogFileName=MyRestaurant.Services.Tests.Results.xml" --results-directory ./CoverageReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=%CD%/CoverageReports/Coverage/MyRestaurant.Services.Tests.Results.coverage.cobertura.xml /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit.*]"

dotnet test MyRestaurant.Business.Tests/MyRestaurant.Business.Tests.csproj --logger "xunit;LogFileName=MyRestaurant.Business.Tests.Results.xml" --results-directory ./CoverageReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=%CD%/CoverageReports/Coverage/MyRestaurant.Business.Tests.Results.coverage.cobertura.xml /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit.*]"

dotnet test MyRestaurant.Api.Tests/MyRestaurant.Api.Tests.csproj --logger "xunit;LogFileName=MyRestaurant.Api.Test.Results.xml" --results-directory ./CoverageReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=%CD%/CoverageReports/Coverage/MyRestaurant.Api.Test.Results.coverage.cobertura.xml /p:CoverletOutputFormat=cobertura /p:Exclude="[xunit.*]"

%UserProfile%\.nuget\packages\reportgenerator\4.8.5\tools\net5.0\ReportGenerator.exe "-reports:%CD%\CoverageReports\Coverage\*.cobertura.xml" "-targetdir:CoverageReports\Coverage" -reporttypes:HTML

start %CD%\CoverageReports\Coverage\index.html