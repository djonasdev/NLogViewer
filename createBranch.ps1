param([string]$IssueName)

$branchName = $IssueName.ToLower().Replace('/', '-').Replace(' ', '-')
$branchName = $branchName.Substring('0', '40')

echo "New branch '$branchName' created"

git checkout -b $branchName
git add .
git add -u
#git commit -m "Description of my changes for issue $IssueName"
#git push -u origin $branchName