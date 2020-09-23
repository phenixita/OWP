[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $migrationScriptsReportPath)

[xml]$fileContents = Get-Content -Path $migrationScriptsReportFullPath
$operations = $fileContents.DeploymentReport.GetElementsByTagName("Operation")

if ($operations.Count -gt 0)
{
    Write-Host "Differences found!"
    Write-Host "##vso[task.setvariable variable=db.hasDifferces]1"
}
else
{
    Write-Host "No differences found!"
    Write-Host "##vso[task.setvariable variable=db.hasDifferces]0"
}