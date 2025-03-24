USE incident_db;
-- Table des départements
CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE
);

-- Table des rôles
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);

-- Table des utilisateurs
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    Phone NVARCHAR(50),
    RoleID INT,
    DepartmentID INT,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID),
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID)
);

-- Table des types d'incidents
CREATE TABLE IncidentTypes (
    TypeID INT PRIMARY KEY IDENTITY(1,1),
    TypeName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255)
);

-- Table des priorités des incidents
CREATE TABLE Priorities (
    PriorityID INT PRIMARY KEY IDENTITY(1,1),
    PriorityName NVARCHAR(50) NOT NULL UNIQUE,
    SLA_Days INT NOT NULL -- Délai de résolution en jours
);

-- Table des incidents
CREATE TABLE Incidents (
    IncidentID INT PRIMARY KEY IDENTITY(1,1),
    IncidentTypeID INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    CreatedBy INT NOT NULL,
    AssignedTo INT,
    Status NVARCHAR(50) DEFAULT 'Nouveau',
    PriorityID INT DEFAULT 2, -- Par défaut à "Moyenne"
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IncidentTypeID) REFERENCES IncidentTypes(TypeID),
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserID),
    FOREIGN KEY (AssignedTo) REFERENCES Users(UserID),
    FOREIGN KEY (PriorityID) REFERENCES Priorities(PriorityID)
);

-- Table des équipements
CREATE TABLE Equipments (
    EquipmentID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Type NVARCHAR(100) NOT NULL,
    SerialNumber NVARCHAR(50) UNIQUE NOT NULL,
    Location NVARCHAR(100),
    AssignedTo INT,
    MaintenanceDate DATE,
    FOREIGN KEY (AssignedTo) REFERENCES Users(UserID)
);

-- Table des commentaires sur les incidents
CREATE TABLE IncidentComments (
    CommentID INT PRIMARY KEY IDENTITY(1,1),
    IncidentID INT NOT NULL,
    Comment NVARCHAR(MAX) NOT NULL,
    CommentedBy INT NOT NULL,
    CommentedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IncidentID) REFERENCES Incidents(IncidentID),
    FOREIGN KEY (CommentedBy) REFERENCES Users(UserID)
);

-- Table des logs des incidents
CREATE TABLE IncidentLogs (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    IncidentID INT NOT NULL,
    Action NVARCHAR(255) NOT NULL,
    ActionBy INT NOT NULL,
    ActionDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IncidentID) REFERENCES Incidents(IncidentID),
    FOREIGN KEY (ActionBy) REFERENCES Users(UserID)
);

-- Table des notifications
CREATE TABLE Notifications (
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    IncidentID INT,
    Message NVARCHAR(255) NOT NULL,
    Status NVARCHAR(50) DEFAULT 'Non lue', -- 'Envoyée', 'Lue'
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (IncidentID) REFERENCES Incidents(IncidentID)
);

-- Table de l'historique des activités des utilisateurs
CREATE TABLE UserActivities (
    ActivityID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Activity NVARCHAR(255) NOT NULL,
    ActivityDate DATETIME DEFAULT GETDATE(),
    IPAddress NVARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Table des pièces jointes
CREATE TABLE Attachments (
    AttachmentID INT PRIMARY KEY IDENTITY(1,1),
    IncidentID INT NOT NULL,
    FilePath NVARCHAR(255) NOT NULL,
    UploadedBy INT NOT NULL,
    UploadedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (IncidentID) REFERENCES Incidents(IncidentID),
    FOREIGN KEY (UploadedBy) REFERENCES Users(UserID)
);

-- Table des rapports des incidents
CREATE TABLE Reports (
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    IncidentID INT NOT NULL,
    ResolutionDetails NVARCHAR(MAX),
    ResolvedBy INT,
    ResolvedAt DATETIME,
    FOREIGN KEY (IncidentID) REFERENCES Incidents(IncidentID),
    FOREIGN KEY (ResolvedBy) REFERENCES Users(UserID)
);
