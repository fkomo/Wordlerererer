# move to solution root
Set-Location -Path ".."

try
{
	Set-Location -Path "Wordlerererer.App\Deploy"
	powershell .\build-release-docker.ps1
	Set-Location -Path "..\.."
}
catch
{
	Write-Output $_
}
finally
{
	# move back to app directory
	Set-Location -Path ".\Deploy"
}