# move to solution root
Set-Location -Path ".."

try
{
	# copy "release" dictionary
	Copy-Item Deploy\Dictionaries\eng-5letter-compressed.bin -Destination Wordlerererer.App\wwwroot\content\dictionary -verbose

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
	# restore old "dev" dictionary
	Copy-Item Deploy\Dictionaries\eng-all.txt -Destination Wordlerererer.App\wwwroot\content\dictionary -verbose

	# move back to app directory
	Set-Location -Path ".\Deploy"
}