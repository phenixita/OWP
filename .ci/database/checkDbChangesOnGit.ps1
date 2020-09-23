[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $repoRootFolder)

if (-not(Test-Path $repoRootFolder)) { Write-Error "Repo root folder [$repoRootFolder] not found!" ; exit 1 }

Set-LocalGroup $repoRootFolder

Write-Host "##vso[task.setvariable variable=db.skip]1"

$editedFiles = git diff HEAD HEAD~ --name-only

Write-Output "Edited Files:" $editedFiles

$editedFiles | ForEach-Object {
    Switch -Wildcard ($_ ) {
        'Sources/Database/*' { Write-Host "##vso[task.setvariable variable=db.skip]0"; break; }
        'Sources/Db-Migration-Scripts/*' { Write-Host "##vso[task.setvariable variable=db.skip]0" ; break; }
    }
}