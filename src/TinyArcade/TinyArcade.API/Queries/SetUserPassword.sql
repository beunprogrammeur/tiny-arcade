UPDATE Users
SET PasswordHash = @PasswordHash
WHERE UserName = @UserName;