[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $filePath
)
 
$file = Get-Content -Path $filePath -Raw -ErrorAction Stop


Write-Host "Remove USE statements"
$regEx = [regex]'USE \[*.*\];'
$match = $regEx.Match($file)
$index = $match.Index + $match.Length

Write-Host "Clean everything from beginning of file to USE [database] included."
$file = $file.Remove(0, $index)

$defaultStatements = "SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;"

if (-not($file.StartsWith($defaultStatements))) {
    $file = $file.Insert(0, $defaultStatements)

    Write-Host "Clean other USE statements"
    $file = $file -replace $regEx, ''

    Write-Host "Writing file"
    Set-Content -Path $filePath -Value $file
}

Write-Host "Sanitized!" -ForegroundColor Green