SELECT Id, EmulatorId, Name, RomFolder 
FROM Consoles
WHERE Id = @Id
LIMIT 1;