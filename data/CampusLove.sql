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
    Genero ENUM('M','F','O') NULL,
    Carrera VARCHAR(100) NULL,
    Intereses TEXT NULL,
    Frases VARCHAR(255) NULL,
    PerfilCompleto BOOLEAN NOT NULL DEFAULT FALSE
) ENGINE=INNODB;

-- =========================================
-- TABLA: Interacciones
-- =========================================
CREATE TABLE IF NOT EXISTS interacciones (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    IdUsuarioOrigen INT NOT NULL,
    IdUsuarioDestino INT NOT NULL,
    TipoInteraccion ENUM('LIKE','DISLIKE') NOT NULL,
    FechaInteraccion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IdUsuarioOrigen) REFERENCES usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (IdUsuarioDestino) REFERENCES usuarios(Id) ON DELETE CASCADE
) ENGINE=INNODB;

-- =========================================
-- TABLA: Coincidencias (Matches)
-- =========================================
CREATE TABLE IF NOT EXISTS coincidencias (
    IdCoincidencia INT AUTO_INCREMENT PRIMARY KEY,
    IdUsuario1 INT NOT NULL,
    IdUsuario2 INT NOT NULL,
    FechaCoincidencia TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IdUsuario1) REFERENCES usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (IdUsuario2) REFERENCES usuarios(Id) ON DELETE CASCADE,
    UNIQUE (IdUsuario1, IdUsuario2),
    CHECK (IdUsuario1 <> IdUsuario2)
) ENGINE=INNODB;

-- =========================================
-- INDICES para optimizar búsquedas
-- =========================================
CREATE INDEX idx_nombre_usuario ON usuarios(Nombre);
CREATE INDEX idx_interacciones ON interacciones(IdUsuarioOrigen, IdUsuarioDestino);
CREATE INDEX idx_coincidencias ON coincidencias(IdUsuario1, IdUsuario2);


-- =========================================
-- INSERTS
-- =========================================

INSERT INTO usuarios (Nombre, Clave, Edad, Genero, Carrera, Intereses, Frases, PerfilCompleto) VALUES
('María', '1234', 22, 'F', 'Ingeniería de Sistemas', 'Videojuegos, Música', 'Buscando bugs en corazones 💻❤️', TRUE),
('Juan', '1234', 24, 'M', 'Diseño Gráfico', 'Arte, Cine', 'Un match y te diseño el futuro 🎨', TRUE),
('Laura', '1234', 21, 'F', 'Psicología', 'Lectura, Café', 'Te escucho con el corazón ☕📚', TRUE),
('Carlos', '1234', 23, 'M', 'Medicina', 'Deporte, Series', 'El mejor remedio: una buena cita 🩺🍿', TRUE),
('Andrea', '1234', 22, 'F', 'Derecho', 'Debates, Viajes', 'Argumenta tu amor 💼✈️', TRUE),
('Luis', '1234', 25, 'M', 'Administración', 'Finanzas, Ajedrez', 'Invertir en amor, la mejor decisión 💰♟️', TRUE);