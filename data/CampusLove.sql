
-- =========================================
-- DATABASE: CampusLove
-- =========================================
CREATE DATABASE CampusLove;
USE CampusLove;

-- =========================================
-- TABLA: Usuarios
-- =========================================
CREATE TABLE Usuarios (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    NombreCompleto VARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Genero ENUM('M', 'F', 'O') NOT NULL, -- M = Masculino, F = Femenino, O = Otro
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
CREATE TABLE Interacciones (
    IdInteraccion INT AUTO_INCREMENT PRIMARY KEY,
    IdUsuarioOrigen INT NOT NULL,
    IdUsuarioDestino INT NOT NULL,
    TipoInteraccion ENUM('LIKE', 'DISLIKE') NOT NULL,
    FechaInteraccion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IdUsuarioOrigen) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE,
    FOREIGN KEY (IdUsuarioDestino) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE
);


-- =========================================
-- TABLA: Matches (Coincidencias)
-- =========================================

CREATE TABLE Coincidencias (
    IdCoincidencia INT AUTO_INCREMENT PRIMARY KEY,
    IdUsuario1 INT NOT NULL,
    IdUsuario2 INT NOT NULL,
    FechaCoincidencia TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IdUsuario1) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE,
    FOREIGN KEY (IdUsuario2) REFERENCES Usuarios(IdUsuario) ON DELETE CASCADE,
    UNIQUE (IdUsuario1, IdUsuario2) -- evita duplicados
;)
-- =========================================
-- INDICES para optimizar búsquedas
-- =========================================

CREATE INDEX idx_nombre_usuario ON Usuarios(NombreCompleto);
CREATE INDEX idx_interacciones ON Interacciones(IdUsuarioOrigen, IdUsuarioDestino);
CREATE INDEX idx_coincidencias ON Coincidencias(IdUsuario1, IdUsuario2);