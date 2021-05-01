@ECHO OFF

dotnet test MyRestaurant.Services.Tests/MyRestaurant.Services.Tests.csproj ^
			--logger "xunit;LogFileName=Services.Results.xml" ^
			--results-directory ./CoverageReports/UnitTests ^
			/p:CollectCoverage=true ^
			/p:CoverletOutput=%CD%/CoverageReports/Coverage/Services.cobertura.xml ^
			/p:CoverletOutputFormat=cobertura ^
			/p:Exclude="[xunit.*]"

dotnet test MyRestaurant.SeedData.Tests/MyRestaurant.SeedData.Tests.csproj ^
			--logger "xunit;LogFileName=SeedData.Results.xml" ^
			--results-directory ./CoverageReports/UnitTests ^
			/p:CollectCoverage=true ^
			/p:CoverletOutput=%CD%/CoverageReports/Coverage/SeedData.cobertura.xml ^
			/p:CoverletOutputFormat=cobertura ^
			/p:Exclude="[xunit.*]"

dotnet test MyRestaurant.Business.Tests/MyRestaurant.Business.Tests.csproj ^
			--logger "xunit;LogFileName=Business.Results.xml" ^
			--results-directory ./CoverageReports/UnitTests ^
			/p:CollectCoverage=true ^
			/p:CoverletOutput=%CD%/CoverageReports/Coverage/Business.cobertura.xml ^
			/p:CoverletOutputFormat=cobertura ^
			/p:Exclude="[xunit.*]"

dotnet test MyRestaurant.Api.Tests/MyRestaurant.Api.Tests.csproj ^
			--logger "xunit;LogFileName=Api.Results.xml" ^
			--results-directory ./CoverageReports/UnitTests ^
			/p:CollectCoverage=true ^
			/p:CoverletOutput=%CD%/CoverageReports/Coverage/Api.cobertura.xml ^
			/p:CoverletOutputFormat=cobertura ^
			/p:Exclude="[xunit.*]"

%UserProfile%\.nuget\packages\reportgenerator\4.8.5\tools\net5.0\ReportGenerator.exe ^
			"-reports:%CD%/CoverageReports/Coverage/Services.cobertura.xml;CoverageReports/Coverage/SeedData.cobertura.xml;CoverageReports/Coverage/Business.cobertura.xml;CoverageReports/Coverage/Api.cobertura.xml" ^
			"-targetdir:CoverageReports\Coverage" -reporttypes:HTML

start %CD%\CoverageReports\Coverage\index.html