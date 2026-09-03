-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 03-09-2026 a las 21:05:01
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.1.25

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
  `es_portada` tinyint(1) DEFAULT NULL
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
  `latitud` decimal(10,0) DEFAULT NULL,
  `longitud` decimal(10,0) DEFAULT NULL,
  `precio_por_dia` decimal(10,0) DEFAULT NULL,
  `porcentaje_seña` decimal(10,0) DEFAULT NULL,
  `estado` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`id`, `propietario_id`, `tipo_inmueble_id`, `direccion`, `cupo`, `latitud`, `longitud`, `precio_por_dia`, `porcentaje_seña`, `estado`) VALUES
(1, 4, 1, 'Barrio Dabal', 1, 12, 12, 21, 10, 'Disponible');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `id` int(11) NOT NULL,
  `dni` varchar(255) DEFAULT NULL,
  `nombre_completo` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `telefono` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`id`, `dni`, `nombre_completo`, `email`, `telefono`) VALUES
(1, '42193840', 'Luis Sosa', 'luis.sosa@email.com', '+54 9 266 456-7890'),
(2, '34291048', 'Iván Auriol', 'ivan.auriol@email.com', '+54 9 266 467-8901'),
(3, '37482910', 'Romina Auriol', 'romina.auriol@email.com', '+54 9 266 478-9012'),
(4, '37829104', 'Brenda Efler', 'brenda.efler@email.com', '+54 9 266 489-0123');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `id` int(11) NOT NULL,
  `reserva_id` int(11) DEFAULT NULL,
  `concepto` varchar(255) DEFAULT NULL,
  `fecha_pago` date DEFAULT NULL,
  `importe` decimal(10,0) DEFAULT NULL,
  `estado` varchar(255) DEFAULT NULL,
  `usuario_creador_id` int(11) DEFAULT NULL,
  `usuario_anulador_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `id` int(11) NOT NULL,
  `nombre` varchar(255) DEFAULT NULL,
  `dni_cuit` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `telefono` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`id`, `nombre`, `dni_cuit`, `email`, `telefono`) VALUES
(4, 'Empresa Norte S.A.', '30-71543210-4', 'contacto@nortesa.com', '1144448888'),
(5, 'Laura Beatriz Benítez', '27-31456789-3', 'laura.benitez@email.com', '3515559876'),
(6, 'Luis Fernando', '28123456782', 'lf@email.com', '456789123'),
(7, 'Pablo Poder', '20-39482103-8', 'pablo.poder@email.com', '+54 9 266 412-3456'),
(8, 'Mariano Luzza', '20-26593841-3', 'mariano.luzza@email.com', '+54 9 266 423-4567'),
(9, 'Luis Mercado', '20-26482019-5', 'luis.mercado@email.com', '+54 9 266 434-5678'),
(28, 'Valentina Palú', '27-43568122-1', 'valenp@email.com', '+541156948452'),
(30, 'Florencia Castro', '38749316', 'fmcastro95@gmail.com', '2664565895'),
(31, 'Gloria Trevi', '9555555555555', 'gloria.trevi@email.com', '2664568454'),
(32, 'Florencia Castro', '123123123123', 'fmcastro95@gmail.com', '123');

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
  `monto_por_dia` decimal(10,0) DEFAULT NULL,
  `multa` decimal(10,0) DEFAULT NULL,
  `estado` varchar(255) DEFAULT NULL,
  `usuario_creador_id` int(11) DEFAULT NULL,
  `usuario_terminador_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `reserva`
--

INSERT INTO `reserva` (`id`, `inquilino_id`, `inmueble_id`, `fecha_desde`, `fecha_hasta`, `fecha_fin_real`, `monto_por_dia`, `multa`, `estado`, `usuario_creador_id`, `usuario_terminador_id`) VALUES
(2, 1, 1, '2026-10-01', '2026-11-01', NULL, 21, 0, 'Pendiente', 0, NULL),
(3, 1, 1, '0001-01-01', '0001-01-02', NULL, 21, 0, 'Pendiente', 0, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_inmueble`
--

CREATE TABLE `tipo_inmueble` (
  `id` int(11) NOT NULL,
  `nombre` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipo_inmueble`
--

INSERT INTO `tipo_inmueble` (`id`, `nombre`) VALUES
(1, 'Casa');

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
  `avatar` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
  ADD KEY `inmueble_id` (`inmueble_id`);

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- AUTO_INCREMENT de la tabla `reserva`
--
ALTER TABLE `reserva`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

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
  ADD CONSTRAINT `reserva_ibfk_2` FOREIGN KEY (`inmueble_id`) REFERENCES `inmueble` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
