# move to solution root
Set-Location -Path "..\.."

try
{
	# use appsettings.Test.json
	Copy-Item Wordlerererer.App\wwwroot\appsettings.Test.json -Destination Wordlerererer.App\wwwroot\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Wordlerererer.App\Deploy\dockerfile -Destination .\dockerfile-Wordlerererer.App -verbose
	Copy-Item Wordlerererer.App\Deploy\nginx.conf -Destination . -verbose

	# stop&remove old docker image
	docker stop Wordlerererer.App
	docker rm Wordlerererer.App
	docker image prune -a -f

	# build new docker image
	docker build -f dockerfile-Wordlerererer.App -t Wordlerererer.App-docker .

	# run new image on localhost
	docker run -d --name Wordlerererer.App -p 8051:80 Wordlerererer.App-docker

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
