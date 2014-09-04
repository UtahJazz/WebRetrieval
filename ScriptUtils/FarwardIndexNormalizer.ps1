. ./Common.ps1

$documentExtension = "htm"
$documents = Get-ChildItem $forwardIndexDirectory

$id = 0
foreach($document in $documents)
{
	if(!($document.Name -match "[0-9]*.htm"))
	{
		$newName = "$id.$documentExtension"
		while(Test-Path "$forwardIndexDirectory$newName")
		{
			$id += 1
			$newName = "$id.$documentExtension"
		}
		Write-Host "$forwardIndexDirectory$newName"	
		
		Rename-Item $document.FullName "$id.$documentExtension"
		$id += 1
	}
}