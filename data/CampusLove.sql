-- =========================================
-- DATABASE: CampusLove
-- =========================================
CREATE DATABASE IF NOT EXISTS CampusLove;
USE CampusLove;

-- =========================================
-- TABLA: Usuarios
-- =========================================
CREATE TABLE IF NOT EXISTS Usuarios (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Genero ENUM('M', 'F', 'O') NOT NULL DEFAULT 'O', -- M = Masculino, F = Femenino, O = Otro
    Carrera VARCHAR(100) NOT NULL,
    Intereses TEXT NOT NULL,
    FrasePerfil VARCHAR(255),
    CantidadLikes INT DEFAULT 0,
    CantidadMatches INT DEFAULT 0,
    FechaRegistro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

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
