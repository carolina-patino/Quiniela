param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../../"

$dbMigratorFolder = Join-Path $slnFolder "src/Festival.DbMigrator"

Write-Host "********* BUILDING DbMigrator *********" -ForegroundColor Green
Set-Location $dbMigratorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t mycompanyname/festival-db-migrator:$version .


$blazorFolder = Join-Path $slnFolder "src/Festival.Blazor"
$hostFolder = Join-Path $slnFolder "src/Festival.HttpApi.Host"

Write-Host "********* BUILDING Blazor Application *********" -ForegroundColor Green
Set-Location $blazorFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t mycompanyname/festival-blazor:$version .

$authServerAppFolder = Join-Path $slnFolder "src/Festival.AuthServer"
Set-Location $authServerAppFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t mycompanyname/festival-authserver:$version .

Write-Host "********* BUILDING Api.Host Application *********" -ForegroundColor Green
Set-Location $hostFolder
dotnet publish -c Release




### ALL COMPLETED
Write-Host "COMPLETED" -ForegroundColor Green
Set-Location $currentFolder