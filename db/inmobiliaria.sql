-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 17-09-2026 a las 03:23:14
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `imagen_inmueble`
--

CREATE TABLE `imagen_inmueble` (
  `id` int(11) NOT NULL,
  `inmueble_id` int(11) DEFAULT NULL,
  `url` varchar(255) DEFAULT NULL,
  `es_portada` tinyint(1) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `id` int(11) NOT NULL,
  `propietario_id` int(11) DEFAULT NULL,
  `tipo_inmueble_id` int(11) DEFAULT NULL,
  `direccion` varchar(255) DEFAULT NULL,
  `cupo` int(11) DEFAULT NULL,
  `latitud` decimal(10,6) DEFAULT NULL,
  `longitud` decimal(10,6) DEFAULT NULL,
  `precio_por_dia` decimal(10,2) DEFAULT NULL,
  `porcentaje_sena` decimal(5,2) DEFAULT NULL,
  `estado` varchar(255) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`id`, `propietario_id`, `tipo_inmueble_id`, `direccion`, `cupo`, `latitud`, `longitud`, `precio_por_dia`, `porcentaje_sena`, `estado`, `activo`) VALUES
(1, 20, 2, 'almirante brown', 20, 1.000000, 1.000000, 10.00, 100.00, 'Disponible', 1),
(2, 21, 2, 'La punta', 20, 90.000000, 100.000000, 100000.00, 20.00, 'Disponible', 1),
(3, 20, 2, 'la cajeta de tu tia', 7, -10.000000, -30.000000, 35000.00, 25.00, 'Disponible', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `id` int(11) NOT NULL,
  `dni` varchar(255) DEFAULT NULL,
  `nombre_completo` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `telefono` varchar(255) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`id`, `dni`, `nombre_completo`, `email`, `telefono`, `activo`) VALUES
(5, '2030403', 'Luis Sosa', 'luis@gmail.com', '20304060', 1),
(6, '21321412', 'ivan gomez', NULL, '20304060', 1),
(7, '98766544', 'chino mandarin', 'chinomandar@test.com', '2664646464', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `id` int(11) NOT NULL,
  `reserva_id` int(11) DEFAULT NULL,
  `concepto` varchar(255) DEFAULT NULL,
  `fecha_pago` date DEFAULT NULL,
  `importe` decimal(10,2) DEFAULT NULL,
  `estado` varchar(255) DEFAULT NULL,
  `usuario_creador_id` int(11) DEFAULT NULL,
  `usuario_anulador_id` int(11) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`id`, `reserva_id`, `concepto`, `fecha_pago`, `importe`, `estado`, `usuario_creador_id`, `usuario_anulador_id`, `activo`) VALUES
(1, 24, 'Seña', '2026-10-23', 20000.00, 'Anulado', 7, 7, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `id` int(11) NOT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `dni_cuit` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `telefono` varchar(255) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`id`, `nombre`, `dni_cuit`, `email`, `telefono`, `activo`) VALUES
(20, 'pedro gomez', '231241212', 'juancito@gmail.com', '266421213', 1),
(21, 'ivan', '20345678', 'Aurelio_Ivan@gmail.com', '266421213', 1),
(22, 'chino ', '20202020', 'malvado@test.com', '20202020', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `reserva`
--

CREATE TABLE `reserva` (
  `id` int(11) NOT NULL,
  `inquilino_id` int(11) DEFAULT NULL,
  `inmueble_id` int(11) DEFAULT NULL,
  `fecha_desde` date DEFAULT NULL,
  `fecha_hasta` date DEFAULT NULL,
  `fecha_fin_real` date DEFAULT NULL,
  `monto_por_dia` decimal(10,2) DEFAULT NULL,
  `multa` decimal(10,2) DEFAULT NULL,
  `estado` varchar(255) DEFAULT NULL,
  `usuario_creador_id` int(11) DEFAULT NULL,
  `usuario_terminador_id` int(11) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `reserva`
--

INSERT INTO `reserva` (`id`, `inquilino_id`, `inmueble_id`, `fecha_desde`, `fecha_hasta`, `fecha_fin_real`, `monto_por_dia`, `multa`, `estado`, `usuario_creador_id`, `usuario_terminador_id`, `activo`) VALUES
(3, 6, 1, '2030-11-23', '2063-10-24', NULL, 10.00, 0.00, 'Cancelada', NULL, NULL, 0),
(11, 5, 1, '2026-10-01', '2026-10-05', NULL, 25000.00, 0.00, 'Disponible', NULL, NULL, 0),
(12, 6, 1, '2026-11-10', '2026-11-15', NULL, 45000.00, 0.00, 'Pendiente', NULL, NULL, 0),
(17, 5, 2, '2026-08-01', '2027-01-01', '2026-10-15', 25000.00, 0.00, 'Finalizada Anticipadamente', NULL, NULL, 0),
(19, 5, 1, '2026-09-20', '2026-09-25', NULL, 10000.00, 0.00, 'pendiente', 4, NULL, 0),
(20, 5, 1, '2026-11-18', '2026-11-25', '2026-11-19', 10.00, 30.00, 'Finalizada anticipadamente', NULL, NULL, 0),
(21, 5, 1, '2026-11-26', '2026-12-25', NULL, 10.00, 0.00, 'Pendiente', NULL, NULL, 0),
(22, 6, 2, '2026-11-11', '2026-12-11', NULL, 100000.00, 0.00, 'Pendiente', NULL, NULL, 0),
(23, 6, 1, '2026-12-26', '2026-12-31', '2026-12-30', 10.00, 2.50, 'Finalizada anticipadamente', 7, 7, 1),
(24, 5, 2, '2026-11-27', '2026-12-31', '2026-11-30', 100000.00, 1550000.00, 'Finalizada anticipadamente', 8, 7, 1),
(25, 5, 2, '2026-11-15', '2026-12-18', '2026-11-26', 10.00, 110.00, 'Finalizada anticipadamente', NULL, 7, 1),
(26, 5, 1, '2026-09-16', '2026-09-20', '2026-09-17', 10.00, 15.00, 'Finalizada anticipadamente', 7, 7, 1),
(27, 6, 3, '2026-09-16', '2026-09-29', NULL, 35000.00, 0.00, 'Pendiente', 7, NULL, 1),
(28, 6, 3, '2026-09-30', '2026-10-29', NULL, 35000.00, 0.00, 'Pendiente', 7, NULL, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_inmueble`
--

CREATE TABLE `tipo_inmueble` (
  `id` int(11) NOT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipo_inmueble`
--

INSERT INTO `tipo_inmueble` (`id`, `nombre`, `activo`) VALUES
(1, 'Casa', 1),
(2, 'Monoambiente', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id` int(11) NOT NULL,
  `email` varchar(255) DEFAULT NULL,
  `password_hash` varchar(255) DEFAULT NULL,
  `rol` varchar(255) DEFAULT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `apellido` varchar(255) DEFAULT NULL,
  `avatar` varchar(255) DEFAULT NULL,
  `activo` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`id`, `email`, `password_hash`, `rol`, `nombre`, `apellido`, `avatar`, `activo`) VALUES
(4, 'admin@test.com', '1234', 'Administrador', 'Admin', 'Prueba', NULL, 0),
(5, 'juan@gomez.com', '5234523523', 'Empleado', 'ivannnnn', 'gomezz', NULL, 0),
(6, 'admin@test.com', '123454356346', 'Administrador', 'Adminnk', 'Prueba', NULL, 0),
(7, 'admin@test.com', '1234', 'Administrador', 'chino', 'Prueba', NULL, 1),
(8, 'juan@gomez.com', '1234', 'Empleado', 'juan', 'gomez', NULL, 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `imagen_inmueble`
--
ALTER TABLE `imagen_inmueble`
  ADD PRIMARY KEY (`id`),
  ADD KEY `inmueble_id` (`inmueble_id`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`id`),
  ADD KEY `propietario_id` (`propietario_id`),
  ADD KEY `tipo_inmueble_id` (`tipo_inmueble_id`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`id`),
  ADD KEY `reserva_id` (`reserva_id`),
  ADD KEY `usuario_creador_id` (`usuario_creador_id`),
  ADD KEY `usuario_anulador_id` (`usuario_anulador_id`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `reserva`
--
ALTER TABLE `reserva`
  ADD PRIMARY KEY (`id`),
  ADD KEY `inquilino_id` (`inquilino_id`),
  ADD KEY `inmueble_id` (`inmueble_id`),
  ADD KEY `usuario_creador_id` (`usuario_creador_id`),
  ADD KEY `usuario_terminador_id` (`usuario_terminador_id`);

--
-- Indices de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `imagen_inmueble`
--
ALTER TABLE `imagen_inmueble`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT de la tabla `reserva`
--
ALTER TABLE `reserva`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `imagen_inmueble`
--
ALTER TABLE `imagen_inmueble`
  ADD CONSTRAINT `imagen_inmueble_ibfk_1` FOREIGN KEY (`inmueble_id`) REFERENCES `inmueble` (`id`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`propietario_id`) REFERENCES `propietario` (`id`),
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`tipo_inmueble_id`) REFERENCES `tipo_inmueble` (`id`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`reserva_id`) REFERENCES `reserva` (`id`),
  ADD CONSTRAINT `pago_ibfk_2` FOREIGN KEY (`usuario_creador_id`) REFERENCES `usuario` (`id`),
  ADD CONSTRAINT `pago_ibfk_3` FOREIGN KEY (`usuario_anulador_id`) REFERENCES `usuario` (`id`);

--
-- Filtros para la tabla `reserva`
--
ALTER TABLE `reserva`
  ADD CONSTRAINT `reserva_ibfk_1` FOREIGN KEY (`inquilino_id`) REFERENCES `inquilino` (`id`),
  ADD CONSTRAINT `reserva_ibfk_2` FOREIGN KEY (`inmueble_id`) REFERENCES `inmueble` (`id`),
  ADD CONSTRAINT `reserva_ibfk_3` FOREIGN KEY (`usuario_creador_id`) REFERENCES `usuario` (`id`),
  ADD CONSTRAINT `reserva_ibfk_4` FOREIGN KEY (`usuario_terminador_id`) REFERENCES `usuario` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
