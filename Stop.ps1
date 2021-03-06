$dockerCompose = "docker-compose -f docker-compose.yml -f docker-compose.override.yml docker-compose.core.mlhost.yml"

Write-Host "Stopping docker containers..." -ForegroundColor Green
Write-Host "$dockerCompose down"
Invoke-Expression "$dockerCompose down"