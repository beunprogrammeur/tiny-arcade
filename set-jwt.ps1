$jwtKey = Read-Host "Enter your JWT secret key" -AsSecureString
$jwtKeyPlain = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($jwtKey))

[System.Environment]::SetEnvironmentVariable("Jwt__Key", $jwtKeyPlain, "User")

Write-Host "JWT key has been set as a user environment variable (Jwt__Key)."