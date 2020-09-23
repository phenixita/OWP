[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $migrationScriptsFolder)

$sqlName = Get-ChildItem -Filter *.sql -Path $migrationScriptsFolder -ErrorAction Stop| Sort-Object | Select-Object -Last 1 -ExpandProperty Name

$sqlName = $sqlName.Replace(".sql", "")
$sqlName = [string]([math]::floor(([double]$sqlName + 10) / 10 ) * 10)
$sqlName = $sqlName.PadLeft(9, '0') + ".sql"

Write-Output $sqlName
Write-Host "##vso[task.setvariable variable=db.migrationScriptName]$sqlName"