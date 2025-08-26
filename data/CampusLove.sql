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
    Nombre VARCHAR(100) NOT NULL UNIQUE,  
    Clave VARCHAR(255) NOT NULL,         
    Edad INT NULL,                        
    Genero ENUM('M', 'F', 'O') NULL,      
    Carrera VARCHAR(100) NULL,            
    Intereses TEXT NULL,                  
    Frases VARCHAR(255) NULL,             
    PerfilCompleto BOOLEAN NOT NULL DEFAULT FALSE 
) ENGINE=INNODB;



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
