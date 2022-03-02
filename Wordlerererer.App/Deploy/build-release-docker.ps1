# move to solution root
Set-Location -Path "..\.."

try
{
	# use appsettings.Release.json
	Copy-Item Wordlerererer.App\wwwroot\appsettings.Release.json -Destination Wordlerererer.App\wwwroot\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Wordlerererer.App\Deploy\dockerfile -Destination .\dockerfile-Wordlerererer.App -verbose
	Copy-Item Wordlerererer.App\Deploy\nginx.conf -Destination . -verbose

	# build new docker image
	docker build -f dockerfile-Wordlerererer.App -t Wordlerererer.App-docker .

	# save & copy image to deploy dir
	$timestamp = (Get-Date).ToString('yyyyMMddHHmmss')
	$deployDestination = '.\Deploy\Wordlerererer.App-docker_' + $timestamp + '.tar'
	docker save -o $deployDestination Wordlerererer.App-docker

	# load image
	#docker load -i Wordlerererer.App-docker.tar

	Write-Output "... Success!"
}
catch
{
	Write-Output $_
}
finally
{
	# remove temporary files
	Remove-Item .\dockerfile-Wordlerererer.App -verbose
	Remove-Item .\nginx.conf -verbose

	# restore appsettings.Debug.json
	Copy-Item Wordlerererer.App\wwwroot\appsettings.Debug.json -Destination Wordlerererer.App\wwwroot\appsettings.json -verbose

	# move back to app directory
	Set-Location -Path ".\Wordlerererer.App\Deploy"
}