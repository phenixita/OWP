# .\Deploy-Tools\SqlPackage\sqlpackage.exe /Action:Script /SourceFile:"xxxx.dacpac" /TargetServerName:"xxx" /TargetDatabaseName:"databaseDaCatena" /DeployScriptPath:"c:\temp\dlm\migscript0000.sql" /DeployReportPath:"C:\temp\dlm\MigrationReport.xml" /Profile:"Database.publish.xml"


& "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\150\sqlpackage.exe" `
    /Action:Script `
    /SourceFile:"src\OwpPortal.Database\bin\Release\OwpPortal.Database.dacpac" `
    /TargetServerName:"localhost" `
    /TargetDatabaseName:"model" `
    /DeployScriptPath:"c:\temp\dlm\migscript0000.sql" `
    /DeployReportPath:"C:\temp\dlm\MigrationReport.xml" `
    /Profile:".ci\databasePublishProfile.xml" 

