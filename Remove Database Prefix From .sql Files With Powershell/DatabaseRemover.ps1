param (
    [string]$database = "Purchases"
)

$regexText = '\b' + $database + '\b[.](\w+[.]\w)';
$location = Get-Location
$regex = [regex]::new($regexText);
$options = [Text.RegularExpressions.RegexOptions]'IgnoreCase'

foreach ($item in [System.IO.Directory]::EnumerateFiles($location, "*.sql")) {
    [String] $file = Get-Content $item -Encoding UTF8 -Raw
    [String] $replaced = [System.Text.RegularExpressions.Regex]::Replace($file, $regex, '$1', $options)
    [String] $fileName = [System.IO.Path]::GetFullPath($item)
    [System.IO.File]::WriteAllText($fileName, $replaced)
}
