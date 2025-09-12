SELECT e.ID, e.Name, e.Path, e.Arguments
FROM Games g
JOIN Consoles c ON g.ConsoleID = c.ID
JOIN Emulators e ON c.EmulatorID = e.ID
WHERE g.ID = @Id