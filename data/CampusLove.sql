-- =========================================
-- DATABASE: CampusLove
-- =========================================
CREATE DATABASE IF NOT EXISTS CampusLove;
USE CampusLove;

-- =========================================
-- TABLA: Usuarios
-- =========================================
CREATE TABLE IF NOT EXISTS usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Clave VARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Genero ENUM('M', 'F', 'O') NOT NULL DEFAULT 'O', 
    Carrera VARCHAR(100) NOT NULL,
    Intereses TEXT NOT NULL,
    Frases VARCHAR(255)
)ENGINE=INNODB;


CREATE TABLE interacciones (
    Id INT PRIMARY KEY IDENTITY(1,1),  
    IdUsuarioOrigen INT NOT NULL,     
    IdUsuarioDestino INT NOT NULL,    
    TipoInteraccion INT NOT NULL,    
    FechaInteraccion DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Interacciones_UsuarioOrigen FOREIGN KEY (IdUsuarioOrigen) REFERENCES usuarios(Id),
    CONSTRAINT FK_Interacciones_UsuarioDestino FOREIGN KEY (IdUsuarioDestino) REFERENCES usuarios(Id)
)ENGINE=INNODB;


CREATE TABLE interacciones (
  Id INT NOT NULL AUTO_INCREMENT,
  IdUsuarioOrigen INT NOT NULL,
  IdUsuarioDestino INT NOT NULL,
  TipoInteraccion VARCHAR(20) NOT NULL, -- almacena 'LIKE' o 'DISLIKE'
  FechaInteraccion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (Id),
  INDEX IX_Interacciones_Origen (IdUsuarioOrigen),
  INDEX IX_Interacciones_Destino (IdUsuarioDestino),
  CONSTRAINT FK_Interacciones_UsuarioOrigen
    FOREIGN KEY (IdUsuarioOrigen) REFERENCES usuarios(Id) ON DELETE CASCADE,
  CONSTRAINT FK_Interacciones_UsuarioDestino
    FOREIGN KEY (IdUsuarioDestino) REFERENCES usuarios (Id) ON DELETE CASCADE
) ENGINE=INNODB;
-- =========================================
-- TABLA: Interacciones
-- =========================================
CREATE TABLE IF NOT EXISTS interacciones (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    IdUsuarioOrigen INT NOT NULL,
    IdUsuarioDestino INT NOT NULL,
    TipoInteraccion ENUM('LIKE', 'DISLIKE') NOT NULL,
    FechaInteraccion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IdUsuarioOrigen) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE,
    FOREIGN KEY (IdUsuarioDestino) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE,
    CONSTRAINT chk_TipoInteraccion CHECK (TipoInteraccion IN ('LIKE', 'DISLIKE'))
);


-- =========================================
-- TABLA: Coincidencias (Matches)
-- =========================================
CREATE TABLE IF NOT EXISTS Coincidencias (
    IdCoincidencia INT AUTO_INCREMENT PRIMARY KEY,
    IdUsuario1 INT NOT NULL,
    IdUsuario2 INT NOT NULL,
    FechaCoincidencia TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IdUsuario1) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE,
    FOREIGN KEY (IdUsuario2) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE,
    UNIQUE (IdUsuario1, IdUsuario2),
    CHECK (IdUsuario1 <> IdUsuario2) -- no puede ser match con uno mismo
);

-- =========================================
-- INDICES para optimizar búsquedas
-- =========================================
CREATE INDEX IF NOT EXISTS idx_nombre_usuario ON Usuarios(NombreCompleto);
CREATE INDEX IF NOT EXISTS idx_interacciones ON Interacciones(IdUsuarioOrigen, IdUsuarioDestino);
CREATE INDEX IF NOT EXISTS idx_coincidencias ON Coincidencias(IdUsuario1, IdUsuario2);
