# move to solution root
Set-Location -Path "..\.."

# path to referenced libraries/projects
$ujeby = '..\Ujeby\Deploy\'
$ujebyCommon = $ujeby + 'Ujeby.Common.dll'
$ujebyBlazorBase = $ujeby + 'Ujeby.Blazor.Base.dll'

try
{
	# gather referenced libraries
	New-Item -Force -ItemType directory -Path .\Deploy\3rd
	Copy-Item $ujebyCommon -Destination .\Deploy\3rd\Ujeby.Common.dll -verbose -force
	Copy-Item $ujebyBlazorBase -Destination .\Deploy\3rd\Ujeby.Blazor.Base.dll -verbose -force

	# use appsettings.Release.json
	Copy-Item Wordlerererer.App\wwwroot\appsettings.Release.json -Destination Wordlerererer.App\wwwroot\appsettings.json -verbose

	# copy config files to solution root (temporary)
	Copy-Item Wordlerererer.App\Deploy\dockerfile -Destination .\dockerfile-wordlerererer.app -verbose
	Copy-Item Wordlerererer.App\Deploy\nginx.conf -Destination . -verbose

	# build new docker image
	docker build -f dockerfile-wordlerererer.app -t wordlerererer.app-docker .

	# save & copy image to deploy dir
	$timestamp = (Get-Date).ToString('yyyyMMddHHmmss')
	$deployDestination = '.\Deploy\wordlerererer.app-docker_' + $timestamp + '.tar'
	docker save -o $deployDestination wordlerererer.app-docker

	# load image
	#docker load -i wordlerererer.app-docker.tar

	Write-Output "... Success!"
}
catch
{
	Write-Output $_
}
finally
{
	# remove temporary files
	Remove-Item .\dockerfile-wordlerererer.app -verbose
	Remove-Item .\nginx.conf -verbose

	# restore appsettings.Debug.json
	Copy-Item Wordlerererer.App\wwwroot\appsettings.Debug.json -Destination Wordlerererer.App\wwwroot\appsettings.json -verbose

	# move back to app directory
	Set-Location -Path ".\Wordlerererer.App\Deploy"
}