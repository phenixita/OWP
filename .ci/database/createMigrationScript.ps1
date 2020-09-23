[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $sqlpackageExeContainingFolder, # Custom directory that contains sqlpackage.exe.
    [Parameter()]
    [string]
    $repoRootFolder, # Root folder of the repository.
    [Parameter()]
    [string]
    $dacpacSourceFile, # Full path or relative to root folder of the repo.
    [Parameter()]
    [string]
    $TargetServerName,  
    [Parameter()]
    [string]
    $targetDatabaseName, 
    [Parameter()]
    [string]
    $DeployScriptPath, 
    [Parameter()] 
    [string]
    $DeployReportPath,
    [Parameter()] 
    [string]
    $ProfilePath # Full path or relative to root folder of the repo.
)

if (-not(Test-Path $repoRootFolder)) {
    Write-Error "Repo root folder [$repoRootFolder] not found!"
    exit 1
}
 
$sqlpackageFullPath = "sqlpackage.exe"
if ($sqlpackageExeContainingFolder) {
    $sqlpackageFullPath = Join-Path -Path $sqlpackageExeContainingFolder -ChildPath "sqlpackage.exe"

    if (-not(Test-Path -Path $sqlpackageFullPath)) {
        Write-Error "sqlpackage.exe not found in $sqlpackageFullPath"
        exit 1
    }
}
else {
    Write-Host "Searching for sqlpackage.exe in default locations..."
    $sqlpackageFullPath = (Get-ChildItem -Path "$env:SystemDrive\Program Files (x86)\" -Filter "sqlpackage.exe" -Recurse -ErrorAction SilentlyContinue | Select-Object -First 1 | select FullName).FullName
    if ($sqlpackageFullPath) {
        Write-Host "Found $sqlpackageFullPath"
    }
    else {
        Write-Error "SqlPackage.exe not found!"
        exit 1
    }
}


Push-Location $repoRootFolder

& $sqlpackageFullPath `
    /Action:Script `
    /SourceFile:$dacpacSourceFile `
    /TargetServerName:$targetServerName `
    /TargetDatabaseName:$targetDatabaseName `
    /DeployScriptPath:$DeployScriptPath `
    /DeployReportPath:$DeployReportPath `
    /Profile:$ProfilePath

Pop-Location

Write-Host "Done!" -ForegroundColor Green