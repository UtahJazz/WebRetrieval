$forwardIndexDirectory = "../ForwardIndex/"
$shelfedIndexDirectory = "../ShelfedIndex/quests/"

if(!(Test-Path $forwardIndexDirectory))
{
	New-Item -ItemType Directory -Path  $forwardIndexDirectory 	
}

if(!(Test-Path $shelfedIndexDirectory))
{
	New-Item -ItemType Directory -Path  $shelfedIndexDirectory 	
}
