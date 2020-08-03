
Set-Location config

Write-Output("--------------- kitchen")
Set-Location ../kitchen
dotnet build --verbosity quiet --nologo

Write-Output("--------------- lexer")
Set-Location ../lexer
dotnet build --verbosity quiet --nologo

# Write-Output("--------------- parser")
# Set-Location ../parser
# dotnet build --verbosity quiet --nologo

Write-Output("--------------- done...")
Pause
