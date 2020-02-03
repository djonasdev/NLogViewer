param([string]$IssueName)

$branchName = $IssueName

###########################################################
# clean input string                                      #
###########################################################

# character replacement
$rWhiteSpace = [regex]'[ ]{1,}'
$rSlash = [regex]'[/]{1,}'
$rBackSlash = [regex]'[\\]{1,}'
$rSeperator = [regex]'[-]{2,}'

$branchName = $rWhiteSpace.Replace($branchName, "-")
$branchName = $rSlash.Replace($branchName, "-")
$branchName = $rBackSlash.Replace($branchName, "-")
$branchName = $rSeperator.Replace($branchName, "-")
$branchName = $branchName.Trim('-')

# limit bracnhname to 40 characters length
$branchName = $branchName.Substring('0', '40')

echo "New branch '$branchName' created"

git checkout -b $branchName
git add .
git add -u

#git commit -m "Description of my changes for issue $IssueName"
#git push -u origin $branchName