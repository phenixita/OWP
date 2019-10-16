##Here we store infra as code files.

Currently one deployment solution with one single project
All resources are deployed from a single template to keep it simple ;)
1. Azure SQL Database instance
1. Azure SQL Database
1. app service plan
1. web app
1. application insights instance and some default alert rules

To try this out, use powershell, swithch directories to the template file directory and then execute these commands: 

[optional]
```
az login
az account set --subscription "Visual Studio Enterprise" //or whatever subscription name you have got 
```

```
New-AzResourceGroupDeployment -ResourceGroupName <Target Resource Group Name> -TemplateFile .\azuredeploy.json -TemplateParameterFile .\azuredeploy.parameters.json
```

