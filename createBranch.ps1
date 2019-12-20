param([string]$IssueName)

$branchName=$(echo $IssueName | tr '[:upper:]' '[:lower:]' | tr '/' '-' | tr ' ' '-')
git checkout -b $branchName
git add .
git add -u
git commit -m "Description of my changes for issue $IssueName"
git push -u origin $branchName