Get-ChildItem -Recurse -Depth 10 |
Where-Object { $_.FullName -notmatch 'node_modules|\.git|.next|obj|bin' } |
Select-Object FullName |
Export-Csv Test.csv -NoTypeInformation