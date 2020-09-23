$filePath = "C:\temp\dlm\migscript0000.sql"
$file = Get-Content -Path $filePath -Raw

# Remove USE statements
$regEx = [regex]'USE \[*.*\];'
$match = $regEx.Match($file)
$index = $match.Index + $match.Length

# Clean everything from beginning of file to USE [database] included.
$file = $file.Remove(0, $index)

# Re-insert useful statements.
$file = $file.Insert(0, "SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;
SET NUMERIC_ROUNDABORT OFF;")

# Clean other USE statements
$file = $file -replace $regEx, ''

Set-Content -Path $filePath -Value $file