-- create the tables

CREATE TABLE IF NOT EXISTS Users (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    UserName TEXT NOT NULL,
    PasswordHash TEXT NOT NULL,
    Role TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Games (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    ConsoleID INTEGER NOT NULL,    
    Name TEXT NOT NULL,
    Description TEXT
);

CREATE TABLE IF NOT EXISTS Consoles (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    EmulatorID INTEGER,
    Name TEXT NOT NULL,
    RomFolder TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Emulators (
    ID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Path TEXT NOT NULL,
    Arguments TEXT NOT NULL
);


-- add a test admin, change it's password if you're actually using the app
INSERT INTO Users (UserName, PasswordHash, Role)
SELECT 'admin', 'e8+diSmPG/rhb6Au1rYZCP0vqN5F3Y4hU6PEcwB2Uyg=', 'Admin'
WHERE NOT EXISTS (SELECT 1 FROM Users);
