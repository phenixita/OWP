[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $gitUserName = "Azure DevOps",
    [Parameter()]
    [string]
    $gitEmail = "azure@dev.ops",
    [Parameter()]
    [string]
    $migrationScriptsFolder   )

git pull
git config --global user.name $gitUserName
git config --global user.email $gitEmail
git add (Join-Path -Path $migrationScriptsFolder -ChildPath $(db.migrationScriptName))
git commit -m "Add migration script $(db.migrationScriptName) ***NO_CI***"
git push -q