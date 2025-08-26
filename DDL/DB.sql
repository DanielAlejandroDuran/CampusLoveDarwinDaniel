DROP DATABASE IF EXISTS BaseProyecto;
CREATE DATABASE BaseProyecto CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE BaseProyecto;

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    UsuarioId INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Genero VARCHAR(20) NOT NULL,
    Carrera VARCHAR(100) NOT NULL,
    FrasePerfil VARCHAR(500),
    FechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    EstaActivo BOOLEAN DEFAULT TRUE,
    CreditosDisponibles INT DEFAULT 5, -- Máximo 5 créditos por día
    UltimaRecargaCreditos DATE DEFAULT (CURRENT_DATE)
);

-- Tabla de Intereses de Usuario
CREATE TABLE InteresesUsuario (
    InteresUsuarioId INT AUTO_INCREMENT PRIMARY KEY,
    UsuarioId INT NOT NULL,
    Interes VARCHAR(100) NOT NULL,
    FechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE
);

-- Tabla de Interacciones (Likes/Dislikes)
CREATE TABLE Interacciones (
    InteraccionId INT AUTO_INCREMENT PRIMARY KEY,
    UsuarioOrigenId INT NOT NULL,
    UsuarioDestinoId INT NOT NULL,
    TipoInteraccion ENUM('Like', 'Dislike') NOT NULL,
    FechaCreacion DATETIME DEFAULT CURRENT_TIMESTAMP,
    EstaActivo BOOLEAN DEFAULT TRUE,
    CONSTRAINT fk_origen FOREIGN KEY (UsuarioOrigenId) REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE,
    CONSTRAINT fk_destino FOREIGN KEY (UsuarioDestinoId) REFERENCES Usuarios(UsuarioId) ON DELETE CASCADE,
    CONSTRAINT uq_interaccion UNIQUE (UsuarioOrigenId, UsuarioDestinoId) -- evita duplicados
);
-- Agregar columna para almacenar el hash de la contraseña
ALTER TABLE Usuarios
ADD COLUMN PasswordHash VARCHAR(255) NULL;
