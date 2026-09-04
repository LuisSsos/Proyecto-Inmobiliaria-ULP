CREATE DATABASE IF NOT EXISTS `inmobiliaria`
  DEFAULT CHARACTER SET utf8mb4
  COLLATE utf8mb4_general_ci;

USE `inmobiliaria`;

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";
SET FOREIGN_KEY_CHECKS = 0;

-- ========================================================
-- ELIMINAR TABLAS EXISTENTES
-- ========================================================

DROP TABLE IF EXISTS `pago`;
DROP TABLE IF EXISTS `imagen_inmueble`;
DROP TABLE IF EXISTS `reserva`;
DROP TABLE IF EXISTS `inmueble`;
DROP TABLE IF EXISTS `inquilino`;
DROP TABLE IF EXISTS `propietario`;
DROP TABLE IF EXISTS `tipo_inmueble`;

SET FOREIGN_KEY_CHECKS = 1;

-- ========================================================
-- TABLA: tipo_inmueble
-- ========================================================

CREATE TABLE `tipo_inmueble` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_general_ci;

-- ========================================================
-- TABLA: propietario
-- ========================================================

CREATE TABLE `propietario` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(255) NOT NULL,
  `dni_cuit` VARCHAR(255) NOT NULL,
  `email` VARCHAR(255) DEFAULT NULL,
  `telefono` VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_general_ci;

-- ========================================================
-- TABLA: inquilino
-- ========================================================

CREATE TABLE `inquilino` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `dni` VARCHAR(255) NOT NULL,
  `nombre_completo` VARCHAR(255) NOT NULL,
  `email` VARCHAR(255) DEFAULT NULL,
  `telefono` VARCHAR(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_general_ci;

-- ========================================================
-- TABLA: inmueble
-- ========================================================

CREATE TABLE `inmueble` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `propietario_id` INT(11) NOT NULL,
  `tipo_inmueble_id` INT(11) NOT NULL,
  `direccion` VARCHAR(255) NOT NULL,
  `cupo` INT(11) NOT NULL,
  `latitud` DECIMAL(10,7) DEFAULT NULL,
  `longitud` DECIMAL(10,7) DEFAULT NULL,
  `precio_por_dia` DECIMAL(10,2) NOT NULL,
  `porcentaje_sena` DECIMAL(5,2) NOT NULL,
  `estado` VARCHAR(255) NOT NULL,

  PRIMARY KEY (`id`),

  KEY `propietario_id` (`propietario_id`),
  KEY `tipo_inmueble_id` (`tipo_inmueble_id`),

  CONSTRAINT `inmueble_ibfk_1`
    FOREIGN KEY (`propietario_id`)
    REFERENCES `propietario` (`id`),

  CONSTRAINT `inmueble_ibfk_2`
    FOREIGN KEY (`tipo_inmueble_id`)
    REFERENCES `tipo_inmueble` (`id`)
) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_general_ci;

-- ========================================================
-- TABLA: imagen_inmueble
-- ========================================================

CREATE TABLE `imagen_inmueble` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `inmueble_id` INT(11) NOT NULL,
  `url` VARCHAR(255) NOT NULL,
  `es_portada` TINYINT(1) DEFAULT 0,

  PRIMARY KEY (`id`),

  KEY `inmueble_id` (`inmueble_id`),

  CONSTRAINT `imagen_inmueble_ibfk_1`
    FOREIGN KEY (`inmueble_id`)
    REFERENCES `inmueble` (`id`)
) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_general_ci;

-- ========================================================
-- TABLA: reserva
-- ========================================================

CREATE TABLE `reserva` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `inquilino_id` INT(11) NOT NULL,
  `inmueble_id` INT(11) NOT NULL,
  `fecha_desde` DATE NOT NULL,
  `fecha_hasta` DATE NOT NULL,
  `fecha_fin_real` DATE DEFAULT NULL,
  `monto_por_dia` DECIMAL(10,2) NOT NULL,
  `multa` DECIMAL(10,2) DEFAULT 0.00,
  `estado` VARCHAR(255) NOT NULL,

  PRIMARY KEY (`id`),

  KEY `inquilino_id` (`inquilino_id`),
  KEY `inmueble_id` (`inmueble_id`),

  CONSTRAINT `reserva_ibfk_1`
    FOREIGN KEY (`inquilino_id`)
    REFERENCES `inquilino` (`id`),

  CONSTRAINT `reserva_ibfk_2`
    FOREIGN KEY (`inmueble_id`)
    REFERENCES `inmueble` (`id`)
) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_general_ci;

-- ========================================================
-- TABLA: pago
-- ========================================================

CREATE TABLE `pago` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `reserva_id` INT(11) NOT NULL,
  `concepto` VARCHAR(255) NOT NULL,
  `fecha_pago` DATE NOT NULL,
  `importe` DECIMAL(10,2) NOT NULL,
  `estado` VARCHAR(255) NOT NULL,

  PRIMARY KEY (`id`),

  KEY `reserva_id` (`reserva_id`),

  CONSTRAINT `pago_ibfk_1`
    FOREIGN KEY (`reserva_id`)
    REFERENCES `reserva` (`id`)
) ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_general_ci;



CREATE TABLE `usuario` (
  `id` int(11) NOT NULL,
  `email` varchar(255) DEFAULT NULL,
  `password_hash` varchar(255) DEFAULT NULL,
  `rol` varchar(255) DEFAULT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(255) DEFAULT NULL,
  `avatar` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


-- ========================================================
-- INSERTS
-- ========================================================


-- ========================================================
-- TIPOS DE INMUEBLE
-- ========================================================

INSERT INTO `tipo_inmueble`
(`id`, `nombre`)
VALUES
(1, 'Casa'),
(2, 'Departamento'),
(3, 'Cabaña'),
(4, 'Quinta'),
(5, 'Monoambiente'),
(6, 'Chalet');


-- ========================================================
-- PROPIETARIOS
-- ========================================================

INSERT INTO `propietario`
(`id`, `nombre`, `dni_cuit`, `email`, `telefono`)
VALUES
(1, 'Empresa Norte S.A.', '30-71543210-4',
 'contacto@nortesa.com', '1144448888'),

(2, 'Laura Beatriz Benítez', '27-31456789-3',
 'laura.benitez@email.com', '3515559876'),

(3, 'Luis Fernando', '28123456782',
 'lf@email.com', '456789123'),

(4, 'Pablo Poder', '20-39482103-8',
 'pablo.poder@email.com', '+54 9 266 412-3456'),

(5, 'Mariano Luzza', '20-26593841-3',
 'mariano.luzza@email.com', '+54 9 266 423-4567'),

(6, 'Luis Mercado', '20-26482019-5',
 'luis.mercado@email.com', '+54 9 266 434-5678'),

(7, 'Valentina Palú', '27-43568122-1',
 'valenp@email.com', '+54 11 5694-8452'),

(8, 'Florencia Castro', '38749316',
 'fmcastro95@gmail.com', '2664565895'),

(9, 'Gloria Trevi', '9555555555555',
 'gloria.trevi@email.com', '2664568454'),

(10, 'Carlos Ramírez', '20-29876543-6',
 'carlos.ramirez@email.com', '+54 9 266 401-2233');


-- ========================================================
-- INQUILINOS
-- ========================================================

INSERT INTO `inquilino`
(`id`, `dni`, `nombre_completo`, `email`, `telefono`)
VALUES
(1, '42193840', 'Luis Sosa',
 'luis.sosa@email.com', '+54 9 266 456-7890'),

(2, '34291048', 'Iván Auriol',
 'ivan.auriol@email.com', '+54 9 266 467-8901'),

(3, '37482910', 'Romina Auriol',
 'romina.auriol@email.com', '+54 9 266 478-9012'),

(4, '37829104', 'Brenda Efler',
 'brenda.efler@email.com', '+54 9 266 489-0123'),

(5, '40123456', 'Martín González',
 'martin.gonzalez@email.com', '+54 9 266 490-1122'),

(6, '39567821', 'Sofía Fernández',
 'sofia.fernandez@email.com', '+54 9 266 491-2233'),

(7, '42987654', 'Nicolás Acosta',
 'nicolas.acosta@email.com', '+54 9 266 492-3344'),

(8, '36543218', 'Carolina Molina',
 'carolina.molina@email.com', '+54 9 266 493-4455');


-- ========================================================
-- INMUEBLES
-- ========================================================

INSERT INTO `inmueble`
(
  `id`,
  `propietario_id`,
  `tipo_inmueble_id`,
  `direccion`,
  `cupo`,
  `latitud`,
  `longitud`,
  `precio_por_dia`,
  `porcentaje_sena`,
  `estado`
)
VALUES

-- 1
(
  1,
  1,
  1,
  'Barrio Dabal',
  6,
  -32.3429000,
  -65.0118000,
  45000.00,
  30.00,
  'Disponible'
),

-- 2
(
  2,
  2,
  2,
  'Av. del Sol 1250',
  4,
  -32.3445000,
  -65.0132000,
  38000.00,
  30.00,
  'Disponible'
),

-- 3
(
  3,
  3,
  3,
  'Camino al Algarrobo 850',
  5,
  -32.3502000,
  -65.0179000,
  52000.00,
  40.00,
  'Disponible'
),

-- 4
(
  4,
  4,
  4,
  'Ruta Provincial 1 km 4',
  10,
  -32.3378000,
  -65.0054000,
  85000.00,
  40.00,
  'Disponible'
),

-- 5
(
  5,
  5,
  5,
  'Calle Pringles 420',
  2,
  -32.3451000,
  -65.0096000,
  28000.00,
  20.00,
  'Disponible'
),

-- 6
(
  6,
  6,
  6,
  'Los Nogales 760',
  8,
  -32.3524000,
  -65.0211000,
  68000.00,
  30.00,
  'Disponible'
),

-- 7
(
  7,
  7,
  1,
  'Calle El Tala 315',
  5,
  -32.3407000,
  -65.0089000,
  42000.00,
  30.00,
  'Reservado'
),

-- 8
(
  8,
  8,
  2,
  'Av. Libertador 980',
  3,
  -32.3463000,
  -65.0147000,
  35000.00,
  25.00,
  'Disponible'
),

-- 9
(
  9,
  9,
  3,
  'Camino de las Sierras 120',
  6,
  -32.3561000,
  -65.0258000,
  60000.00,
  35.00,
  'Disponible'
),

-- 10
(
  10,
  10,
  4,
  'Ruta 5 km 2',
  12,
  -32.3319000,
  -65.0007000,
  95000.00,
  40.00,
  'Mantenimiento'
),

-- 11
(
  11,
  2,
  5,
  'Calle Belgrano 640',
  2,
  -32.3438000,
  -65.0121000,
  30000.00,
  20.00,
  'Disponible'
),

-- 12
(
  12,
  5,
  6,
  'Calle Los Aromos 450',
  7,
  -32.3497000,
  -65.0193000,
  72000.00,
  35.00,
  'Disponible'
);


-- ========================================================
-- RESERVAS
-- ========================================================

INSERT INTO `reserva`
(
  `id`,
  `inquilino_id`,
  `inmueble_id`,
  `fecha_desde`,
  `fecha_hasta`,
  `fecha_fin_real`,
  `monto_por_dia`,
  `multa`,
  `estado`
)
VALUES

-- Reserva 1: finalizada
(
  1,
  1,
  1,
  '2026-01-10',
  '2026-01-15',
  '2026-01-15',
  45000.00,
  0.00,
  'Finalizada'
),

-- Reserva 2: finalizada
(
  2,
  2,
  2,
  '2026-02-05',
  '2026-02-10',
  '2026-02-10',
  38000.00,
  0.00,
  'Finalizada'
),

-- Reserva 3: cancelada
(
  3,
  3,
  3,
  '2026-03-12',
  '2026-03-17',
  NULL,
  52000.00,
  0.00,
  'Cancelada'
),

-- Reserva 4: finalizada con multa
(
  4,
  4,
  4,
  '2026-04-01',
  '2026-04-07',
  '2026-04-08',
  85000.00,
  15000.00,
  'Finalizada'
),

-- Reserva 5: finalizada
(
  5,
  5,
  5,
  '2026-05-10',
  '2026-05-14',
  '2026-05-14',
  28000.00,
  0.00,
  'Finalizada'
),

-- Reserva 6: finalizada
(
  6,
  6,
  6,
  '2026-06-01',
  '2026-06-08',
  '2026-06-08',
  68000.00,
  0.00,
  'Finalizada'
),

-- Reserva 7: pendiente
(
  7,
  7,
  7,
  '2026-10-05',
  '2026-10-10',
  NULL,
  42000.00,
  0.00,
  'Pendiente'
),

-- Reserva 8: confirmada
(
  8,
  8,
  8,
  '2026-10-15',
  '2026-10-20',
  NULL,
  35000.00,
  0.00,
  'Confirmada'
),

-- Reserva 9: pendiente
(
  9,
  1,
  9,
  '2026-11-01',
  '2026-11-08',
  NULL,
  60000.00,
  0.00,
  'Pendiente'
),

-- Reserva 10: confirmada
(
  10,
  2,
  11,
  '2026-11-10',
  '2026-11-15',
  NULL,
  30000.00,
  0.00,
  'Confirmada'
),

-- Reserva 11: pendiente
(
  11,
  3,
  12,
  '2026-12-01',
  '2026-12-10',
  NULL,
  72000.00,
  0.00,
  'Pendiente'
),

-- Reserva 12: cancelada
(
  12,
  4,
  1,
  '2026-12-15',
  '2026-12-20',
  NULL,
  45000.00,
  0.00,
  'Cancelada'
);


-- ========================================================
-- PAGOS
-- ========================================================

INSERT INTO `pago`
(
  `id`,
  `reserva_id`,
  `concepto`,
  `fecha_pago`,
  `importe`,
  `estado`
)
VALUES

(1, 1, 'Seña de reserva',
 '2026-01-05',
 67500.00,
 'Aprobado'),

(2, 1, 'Saldo de reserva',
 '2026-01-15',
 157500.00,
 'Aprobado'),

(3, 2, 'Seña de reserva',
 '2026-01-30',
 57000.00,
 'Aprobado'),

(4, 2, 'Saldo de reserva',
 '2026-02-10',
 133000.00,
 'Aprobado'),

(5, 4, 'Pago total de reserva',
 '2026-04-08',
 525000.00,
 'Aprobado'),

(6, 5, 'Pago total de reserva',
 '2026-05-14',
 112000.00,
 'Aprobado'),

(7, 6, 'Seña de reserva',
 '2026-05-20',
 190400.00,
 'Aprobado'),

(8, 6, 'Saldo de reserva',
 '2026-06-08',
 285600.00,
 'Aprobado'),

(9, 7, 'Seña de reserva',
 '2026-09-20',
 63000.00,
 'Aprobado'),

(10, 8, 'Seña de reserva',
 '2026-09-25',
 52500.00,
 'Aprobado');


-- ========================================================
-- AUTO_INCREMENT
-- ========================================================

ALTER TABLE `tipo_inmueble`
  AUTO_INCREMENT = 7;

ALTER TABLE `propietario`
  AUTO_INCREMENT = 11;

ALTER TABLE `inquilino`
  AUTO_INCREMENT = 9;

ALTER TABLE `inmueble`
  AUTO_INCREMENT = 13;

ALTER TABLE `imagen_inmueble`
  AUTO_INCREMENT = 1;

ALTER TABLE `reserva`
  AUTO_INCREMENT = 13;

ALTER TABLE `pago`
  AUTO_INCREMENT = 11;