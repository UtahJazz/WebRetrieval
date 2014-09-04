. ./Common.ps1
$documents = Get-ChildItem $shelfedIndexDirectory -recurse | Where { !$_.PSIsContainer -and $_.Name -ne "Descr.WD3" -and [String]::IsNullOrEmpty($_.Extension) }

foreach($document in $documents)
{
	$newName = Join-Path $forwardIndexDirectory $document.Name
	Copy-Item $document.FullName $newName
	Write-Host "$newName copied"
}