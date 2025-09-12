SELECT Id, ConsoleId, Name, Description FROM Games
WHERE Id = @ConsoleId
LIMIT 1;