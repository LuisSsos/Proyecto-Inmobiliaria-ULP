-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 18-09-2026 a las 02:47:56
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
CREATE DATABASE IF NOT EXISTS `inmobiliaria` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `inmobiliaria`;

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

--
-- Volcado de datos para la tabla `imagen_inmueble`
--

INSERT INTO `imagen_inmueble` (`id`, `inmueble_id`, `url`, `es_portada`, `activo`) VALUES
(1, 1, 'https://picsum.photos/seed/luvarem-1/900/600', 1, 1),
(2, 1, 'https://picsum.photos/seed/luvarem-1-2/900/600', 0, 1),
(3, 2, 'https://picsum.photos/seed/luvarem-2/900/600', 1, 1),
(4, 2, 'https://picsum.photos/seed/luvarem-2-2/900/600', 0, 1),
(5, 3, 'https://picsum.photos/seed/luvarem-3/900/600', 1, 1),
(6, 3, 'https://picsum.photos/seed/luvarem-3-2/900/600', 0, 1),
(7, 4, 'https://picsum.photos/seed/luvarem-4/900/600', 1, 1),
(8, 4, 'https://picsum.photos/seed/luvarem-4-2/900/600', 0, 1),
(9, 5, 'https://picsum.photos/seed/luvarem-5/900/600', 1, 1),
(10, 5, 'https://picsum.photos/seed/luvarem-5-2/900/600', 0, 1),
(11, 6, 'https://picsum.photos/seed/luvarem-6/900/600', 1, 1),
(12, 6, 'https://picsum.photos/seed/luvarem-6-2/900/600', 0, 1),
(13, 7, 'https://picsum.photos/seed/luvarem-7/900/600', 1, 1),
(14, 7, 'https://picsum.photos/seed/luvarem-7-2/900/600', 0, 1),
(15, 8, 'https://picsum.photos/seed/luvarem-8/900/600', 1, 1),
(16, 8, 'https://picsum.photos/seed/luvarem-8-2/900/600', 0, 1),
(17, 9, 'https://picsum.photos/seed/luvarem-9/900/600', 1, 1),
(18, 9, 'https://picsum.photos/seed/luvarem-9-2/900/600', 0, 1),
(19, 10, 'https://picsum.photos/seed/luvarem-10/900/600', 1, 1),
(20, 10, 'https://picsum.photos/seed/luvarem-10-2/900/600', 0, 1),
(21, 11, 'https://picsum.photos/seed/luvarem-11/900/600', 1, 1),
(22, 12, 'https://picsum.photos/seed/luvarem-12/900/600', 1, 1),
(23, 13, 'https://picsum.photos/seed/luvarem-13/900/600', 1, 1),
(24, 14, 'https://picsum.photos/seed/luvarem-14/900/600', 1, 1),
(25, 15, 'https://picsum.photos/seed/luvarem-15/900/600', 1, 1),
(26, 16, 'https://picsum.photos/seed/luvarem-16/900/600', 1, 1),
(27, 17, 'https://picsum.photos/seed/luvarem-17/900/600', 1, 1),
(28, 18, 'https://picsum.photos/seed/luvarem-18/900/600', 1, 1),
(29, 19, 'https://picsum.photos/seed/luvarem-19/900/600', 1, 1),
(30, 20, 'https://picsum.photos/seed/luvarem-20/900/600', 1, 1),
(31, 21, 'https://picsum.photos/seed/luvarem-21/900/600', 1, 1),
(32, 22, 'https://picsum.photos/seed/luvarem-22/900/600', 1, 1),
(33, 23, 'https://picsum.photos/seed/luvarem-23/900/600', 1, 1),
(34, 24, 'https://picsum.photos/seed/luvarem-24/900/600', 1, 1),
(35, 25, 'https://picsum.photos/seed/luvarem-25/900/600', 1, 1),
(36, 26, 'https://picsum.photos/seed/luvarem-26/900/600', 1, 1),
(37, 27, 'https://picsum.photos/seed/luvarem-27/900/600', 1, 1),
(38, 28, 'https://picsum.photos/seed/luvarem-28/900/600', 1, 1),
(39, 29, 'https://picsum.photos/seed/luvarem-29/900/600', 1, 1),
(40, 30, 'https://picsum.photos/seed/luvarem-30/900/600', 1, 1);

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
(1, 1, 1, 'Av. Illia 100', 2, -33.270000, -66.330000, 28000.00, 20.00, 'Disponible', 1),
(2, 2, 2, 'Av. Lafinur 111', 3, -33.266000, -66.325000, 35000.00, 25.00, 'Disponible', 1),
(3, 3, 3, 'Pringles 122', 4, -33.262000, -66.320000, 42000.00, 30.00, 'Disponible', 1),
(4, 4, 4, 'Junín 133', 5, -33.258000, -66.315000, 50000.00, 30.00, 'Disponible', 1),
(5, 5, 5, 'Rivadavia 144', 6, -33.254000, -66.310000, 65000.00, 40.00, 'Disponible', 1),
(6, 6, 1, 'Belgrano 155', 7, -33.250000, -66.305000, 78000.00, 20.00, 'Disponible', 1),
(7, 7, 2, 'San Martín 166', 8, -33.246000, -66.300000, 95000.00, 25.00, 'Disponible', 1),
(8, 8, 3, 'Mitre 177', 2, -33.242000, -66.330000, 120000.00, 30.00, 'Disponible', 1),
(9, 9, 4, '9 de Julio 188', 3, -33.238000, -66.325000, 28000.00, 30.00, 'Disponible', 1),
(10, 10, 5, 'Colón 199', 4, -33.270000, -66.320000, 35000.00, 40.00, 'Disponible', 1),
(11, 11, 1, 'Las Heras 210', 5, -33.266000, -66.315000, 42000.00, 20.00, 'Disponible', 1),
(12, 12, 2, 'Chacabuco 221', 6, -33.262000, -66.310000, 50000.00, 25.00, 'Disponible', 1),
(13, 13, 3, 'Maipú 232', 7, -33.258000, -66.305000, 65000.00, 30.00, 'Disponible', 1),
(14, 14, 4, 'Ayacucho 243', 8, -33.254000, -66.300000, 78000.00, 30.00, 'Disponible', 1),
(15, 15, 5, 'Mariano Moreno 254', 2, -33.250000, -66.330000, 95000.00, 40.00, 'Disponible', 1),
(16, 1, 1, 'Pedernera 265', 3, -33.246000, -66.325000, 120000.00, 20.00, 'Disponible', 1),
(17, 2, 2, 'Lavalle 276', 4, -33.242000, -66.320000, 28000.00, 25.00, 'Disponible', 1),
(18, 3, 3, 'Aristóbulo del Valle 287', 5, -33.238000, -66.315000, 35000.00, 30.00, 'Disponible', 1),
(19, 4, 4, 'Constitución 298', 6, -33.270000, -66.310000, 42000.00, 30.00, 'Disponible', 1),
(20, 5, 5, 'España 309', 7, -33.266000, -66.305000, 50000.00, 40.00, 'Disponible', 1),
(21, 6, 1, 'Mitre 320', 8, -33.262000, -66.300000, 65000.00, 20.00, 'Disponible', 1),
(22, 7, 2, 'Bolívar 331', 2, -33.258000, -66.330000, 78000.00, 25.00, 'Disponible', 1),
(23, 8, 3, 'Pringles 342', 3, -33.254000, -66.325000, 95000.00, 30.00, 'Disponible', 1),
(24, 9, 4, 'Falucho 353', 4, -33.250000, -66.320000, 120000.00, 30.00, 'Disponible', 1),
(25, 10, 5, 'Riobamba 364', 5, -33.246000, -66.315000, 28000.00, 40.00, 'Disponible', 1),
(26, 11, 1, 'Sarmiento 375', 6, -33.242000, -66.310000, 35000.00, 20.00, 'Disponible', 1),
(27, 12, 2, 'Suipacha 386', 7, -33.238000, -66.305000, 42000.00, 25.00, 'Disponible', 1),
(28, 13, 3, 'Alvear 397', 8, -33.270000, -66.300000, 50000.00, 30.00, 'Disponible', 1),
(29, 14, 4, 'Ejército de los Andes 408', 2, -33.266000, -66.330000, 65000.00, 30.00, 'Disponible', 1),
(30, 15, 5, 'Rawson 419', 3, -33.262000, -66.325000, 78000.00, 40.00, 'Disponible', 1),
(31, 1, 1, 'General Paz 430', 4, -33.258000, -66.320000, 95000.00, 20.00, 'Disponible', 1),
(32, 2, 2, '25 de Mayo 441', 5, -33.254000, -66.315000, 120000.00, 25.00, 'Disponible', 1),
(33, 3, 3, 'Ituzaingó 452', 6, -33.250000, -66.310000, 28000.00, 30.00, 'Disponible', 1),
(34, 4, 4, 'Buenos Aires 463', 7, -33.246000, -66.305000, 35000.00, 30.00, 'Disponible', 1),
(35, 5, 5, 'San Juan 474', 8, -33.242000, -66.300000, 42000.00, 40.00, 'Disponible', 1),
(36, 6, 1, 'Los Álamos 485', 2, -33.238000, -66.330000, 50000.00, 20.00, 'Disponible', 1),
(37, 7, 2, 'Los Tilos 496', 3, -33.270000, -66.325000, 65000.00, 25.00, 'Disponible', 1),
(38, 8, 3, 'Los Cedros 507', 4, -33.266000, -66.320000, 78000.00, 30.00, 'Disponible', 1),
(39, 9, 4, 'Los Paraísos 518', 5, -33.262000, -66.315000, 95000.00, 30.00, 'Disponible', 1),
(40, 10, 5, 'Los Fresnos 529', 6, -33.258000, -66.310000, 120000.00, 40.00, 'Disponible', 1),
(41, 11, 1, 'Los Nogales 540', 7, -33.254000, -66.305000, 28000.00, 20.00, 'Disponible', 1),
(42, 12, 2, 'Los Eucaliptus 551', 8, -33.250000, -66.300000, 35000.00, 25.00, 'Disponible', 1),
(43, 13, 3, 'Los Robles 562', 2, -33.246000, -66.330000, 42000.00, 30.00, 'Disponible', 1),
(44, 14, 4, 'Los Pinos 573', 3, -33.242000, -66.325000, 50000.00, 30.00, 'Disponible', 1),
(45, 15, 5, 'Av. Illia 584', 4, -33.238000, -66.320000, 65000.00, 40.00, 'Disponible', 1);

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
(1, '30000000', 'Nicolás Fernández', NULL, '2664539898', 1),
(2, '30017321', 'Federico Rodríguez', 'inquilino2@mail.com', '2664331148', 1),
(3, '30034642', 'Agustina López', 'inquilino3@mail.com', '2664571029', 1),
(4, '30051963', 'Julieta Martínez', 'inquilino4@mail.com', '2664717889', 1),
(5, '30069284', 'Matías García', 'inquilino5@mail.com', NULL, 1),
(6, '30086605', 'Carolina Díaz', NULL, '2664391704', 1),
(7, '30103926', 'Gonzalo Romero', 'inquilino7@mail.com', '2664948749', 1),
(8, '30121247', 'Florencia Suárez', 'inquilino8@mail.com', '2664106814', 1),
(9, '30138568', 'Sebastián Torres', 'inquilino9@mail.com', NULL, 1),
(10, '30155889', 'Mariana Vega', 'inquilino10@mail.com', '2664895667', 1),
(11, '30173210', 'Facundo Molina', NULL, '2664944962', 1),
(12, '30190531', 'Paula Castro', 'inquilino12@mail.com', '2664267414', 1),
(13, '30207852', 'Ezequiel Gómez', 'inquilino13@mail.com', NULL, 1),
(14, '30225173', 'Romina Pérez', 'inquilino14@mail.com', '2664832052', 1),
(15, '30242494', 'Leandro Sosa', 'inquilino15@mail.com', '2664543143', 1),
(16, '30259815', 'Daniela Fernández', NULL, '2664456778', 1),
(17, '30277136', 'Tomás Rodríguez', 'inquilino17@mail.com', NULL, 1),
(18, '30294457', 'Micaela López', 'inquilino18@mail.com', '2664391369', 1),
(19, '30311778', 'Santiago Martínez', 'inquilino19@mail.com', '2664263032', 1),
(20, '30329099', 'Belén García', 'inquilino20@mail.com', '2664325772', 1),
(21, '30346420', 'Andrés Díaz', NULL, '2664900581', 1),
(22, '30363741', 'Natalia Romero', 'inquilino22@mail.com', '2664452944', 1),
(23, '30381062', 'Maximiliano Suárez', 'inquilino23@mail.com', '2664207175', 1),
(24, '30398383', 'Cecilia Torres', 'inquilino24@mail.com', '2664197251', 1),
(25, '30415704', 'Joaquín Vega', 'inquilino25@mail.com', NULL, 1),
(26, '30433025', 'Verónica Molina', NULL, '2664498382', 1),
(27, '30450346', 'Lucas Castro', 'inquilino27@mail.com', '2664201414', 1),
(28, '30467667', 'Gabriela Gómez', 'inquilino28@mail.com', '2664476417', 1),
(29, '30484988', 'Emiliano Pérez', 'inquilino29@mail.com', NULL, 1),
(30, '30502309', 'Rocío Sosa', 'inquilino30@mail.com', '2664988662', 1),
(31, '30519630', 'Franco Fernández', NULL, '2664460663', 1),
(32, '30536951', 'Marisol Rodríguez', 'inquilino32@mail.com', '2664733052', 1),
(33, '30554272', 'Ramiro López', 'inquilino33@mail.com', NULL, 1),
(34, '30571593', 'Pilar Martínez', 'inquilino34@mail.com', '2664377370', 1),
(35, '30588914', 'Bruno García', 'inquilino35@mail.com', '2664946335', 1),
(36, '30606235', 'Antonella Díaz', NULL, '2664145561', 1),
(37, '30623556', 'Hernán Romero', 'inquilino37@mail.com', NULL, 1),
(38, '30640877', 'Malena Suárez', 'inquilino38@mail.com', '2664865179', 1),
(39, '30658198', 'Iván Torres', 'inquilino39@mail.com', '2664581741', 1),
(40, '30675519', 'Carla Vega', 'inquilino40@mail.com', '2664662275', 1);

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
(1, 1, 'Seña', '2026-08-28', 16800.00, 'Pagado', 1, NULL, 1),
(2, 1, 'Saldo', '2026-09-06', 67200.00, 'Pagado', 1, NULL, 1),
(3, 2, 'Seña', '2026-08-22', 35000.00, 'Pagado', 2, NULL, 1),
(4, 2, 'Saldo', '2026-09-01', 105000.00, 'Pagado', 2, NULL, 1),
(5, 3, 'Seña', '2026-08-16', 63000.00, 'Pagado', 3, NULL, 1),
(6, 3, 'Saldo', '2026-08-27', 147000.00, 'Pagado', 3, NULL, 1),
(7, 4, 'Seña', '2026-08-10', 90000.00, 'Pagado', 4, NULL, 1),
(8, 4, 'Saldo', '2026-08-22', 210000.00, 'Pagado', 4, NULL, 1),
(9, 5, 'Seña', '2026-08-04', 182000.00, 'Pagado', 5, NULL, 1),
(10, 5, 'Saldo', '2026-08-17', 273000.00, 'Pagado', 5, NULL, 1),
(11, 6, 'Seña', '2026-07-29', 124800.00, 'Pagado', 6, NULL, 1),
(12, 6, 'Saldo', '2026-08-12', 499200.00, 'Pagado', 6, NULL, 1),
(13, 7, 'Seña', '2026-07-23', 213750.00, 'Pagado', 1, NULL, 1),
(14, 7, 'Saldo', '2026-08-07', 641250.00, 'Pagado', 1, NULL, 1),
(15, 8, 'Seña', '2026-07-17', 360000.00, 'Pagado', 2, NULL, 1),
(16, 8, 'Saldo', '2026-08-02', 840000.00, 'Pagado', 2, NULL, 1),
(17, 9, 'Seña', '2026-07-19', 25200.00, 'Pagado', 3, NULL, 1),
(18, 9, 'Saldo', '2026-07-28', 58800.00, 'Pagado', 3, NULL, 1),
(19, 10, 'Seña', '2026-07-13', 56000.00, 'Pagado', 4, NULL, 1),
(20, 10, 'Saldo', '2026-07-23', 84000.00, 'Pagado', 4, NULL, 1),
(21, 11, 'Seña', '2026-07-07', 42000.00, 'Pagado', 5, NULL, 1),
(22, 11, 'Saldo', '2026-07-18', 168000.00, 'Pagado', 5, NULL, 1),
(23, 12, 'Seña', '2026-07-01', 75000.00, 'Pagado', 6, NULL, 1),
(24, 12, 'Saldo', '2026-07-13', 225000.00, 'Pagado', 6, NULL, 1),
(25, 13, 'Seña', '2026-06-25', 136500.00, 'Pagado', 1, NULL, 1),
(26, 13, 'Saldo', '2026-07-08', 318500.00, 'Pagado', 1, NULL, 1),
(27, 14, 'Seña', '2026-06-19', 187200.00, 'Pagado', 2, NULL, 1),
(28, 14, 'Saldo', '2026-07-03', 436800.00, 'Pagado', 2, NULL, 1),
(29, 15, 'Seña', '2026-06-13', 342000.00, 'Pagado', 3, NULL, 1),
(30, 15, 'Saldo', '2026-06-28', 513000.00, 'Pagado', 3, NULL, 1),
(31, 16, 'Seña', '2026-06-07', 240000.00, 'Pagado', 4, NULL, 1),
(32, 16, 'Saldo', '2026-06-23', 960000.00, 'Pagado', 4, NULL, 1),
(33, 17, 'Seña', '2026-06-09', 21000.00, 'Pagado', 5, NULL, 1),
(34, 17, 'Saldo', '2026-06-18', 63000.00, 'Pagado', 5, NULL, 1),
(35, 18, 'Seña', '2026-06-03', 42000.00, 'Pagado', 6, NULL, 1),
(36, 18, 'Saldo', '2026-06-13', 98000.00, 'Pagado', 6, NULL, 1),
(37, 19, 'Seña', '2026-05-28', 63000.00, 'Pagado', 1, NULL, 1),
(38, 19, 'Saldo', '2026-06-08', 147000.00, 'Pagado', 1, NULL, 1),
(39, 20, 'Seña', '2026-05-22', 120000.00, 'Pagado', 2, NULL, 1),
(40, 20, 'Saldo', '2026-06-03', 180000.00, 'Pagado', 2, NULL, 1),
(41, 21, 'Seña', '2026-05-16', 104000.00, 'Pagado', 3, NULL, 1),
(42, 21, 'Saldo', '2026-05-29', 416000.00, 'Pagado', 3, NULL, 1),
(43, 22, 'Seña', '2026-05-10', 165750.00, 'Pagado', 4, NULL, 1),
(44, 22, 'Saldo', '2026-05-24', 497250.00, 'Pagado', 4, NULL, 1),
(45, 23, 'Seña', '2026-05-04', 277875.00, 'Pagado', 5, NULL, 1),
(46, 23, 'Saldo', '2026-05-19', 648375.00, 'Pagado', 5, NULL, 1),
(47, 24, 'Seña', '2026-04-28', 396000.00, 'Pagado', 6, NULL, 1),
(48, 24, 'Saldo', '2026-05-14', 924000.00, 'Pagado', 6, NULL, 1),
(49, 25, 'Seña', '2026-04-30', 39200.00, 'Pagado', 1, NULL, 1),
(50, 25, 'Saldo', '2026-05-09', 58800.00, 'Pagado', 1, NULL, 1),
(51, 26, 'Seña', '2026-04-24', 33250.00, 'Pagado', 2, NULL, 1),
(52, 26, 'Saldo', '2026-05-04', 133000.00, 'Pagado', 2, NULL, 1),
(53, 27, 'Seña', '2026-04-18', 63000.00, 'Pagado', 3, NULL, 1),
(54, 27, 'Saldo', '2026-04-29', 189000.00, 'Pagado', 3, NULL, 1),
(55, 28, 'Seña', '2026-04-12', 97500.00, 'Pagado', 4, NULL, 1),
(56, 28, 'Saldo', '2026-04-24', 227500.00, 'Pagado', 4, NULL, 1),
(57, 29, 'Seña', '2026-04-06', 151125.00, 'Pagado', 5, NULL, 1),
(58, 29, 'Saldo', '2026-04-19', 352625.00, 'Pagado', 5, NULL, 1),
(59, 30, 'Seña', '2026-03-31', 280800.00, 'Pagado', 6, NULL, 1),
(60, 30, 'Saldo', '2026-04-14', 421200.00, 'Pagado', 6, NULL, 1);

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
(1, 'Juan Gómez', '20770487', 'propietario1@mail.com', '2664216739', 1),
(2, 'María Pérez', '21126225', 'propietario2@mail.com', '2664877572', 1),
(3, 'Carlos Sosa', '22388389', 'propietario3@mail.com', '2664356787', 1),
(4, 'Laura Fernández', '23334053', 'propietario4@mail.com', '2664246316', 1),
(5, 'Miguel Rodríguez', '24872246', 'propietario5@mail.com', '2664207473', 1),
(6, 'Ana López', '25809570', 'propietario6@mail.com', '2664876646', 1),
(7, 'Pedro Martínez', '26671858', 'propietario7@mail.com', '2664191161', 1),
(8, 'Lucía García', '27719176', 'propietario8@mail.com', '2664542417', 1),
(9, 'Diego Díaz', '28133326', 'propietario9@mail.com', '2664131244', 1),
(10, 'Sofía Romero', '29198246', 'propietario10@mail.com', '2664329258', 1),
(11, 'Martín Suárez', '30343962', 'propietario11@mail.com', '2664629903', 1),
(12, 'Valentina Torres', '31731262', 'propietario12@mail.com', '2664127824', 1),
(13, 'Pablo Vega', '32688508', 'propietario13@mail.com', '2664308496', 1),
(14, 'Camila Molina', '33850800', 'propietario14@mail.com', '2664781453', 1),
(15, 'Fernando Castro', '34835392', 'propietario15@mail.com', '2664671412', 1);

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
(1, 1, 1, '2026-09-04', '2026-09-07', '2026-09-07', 28000.00, 0.00, 'Finalizada', 1, 3, 1),
(2, 2, 2, '2026-08-29', '2026-09-02', '2026-09-02', 35000.00, 0.00, 'Finalizada', 2, 4, 1),
(3, 3, 3, '2026-08-23', '2026-08-28', '2026-08-28', 42000.00, 0.00, 'Finalizada', 3, 5, 1),
(4, 4, 4, '2026-08-17', '2026-08-23', '2026-08-23', 50000.00, 0.00, 'Finalizada', 4, 6, 1),
(5, 5, 5, '2026-08-11', '2026-08-18', '2026-08-18', 65000.00, 0.00, 'Finalizada', 5, 1, 1),
(6, 6, 6, '2026-08-05', '2026-08-13', '2026-08-13', 78000.00, 0.00, 'Finalizada', 6, 2, 1),
(7, 7, 7, '2026-07-30', '2026-08-08', '2026-08-08', 95000.00, 0.00, 'Finalizada', 1, 3, 1),
(8, 8, 8, '2026-07-24', '2026-08-03', '2026-08-03', 120000.00, 0.00, 'Finalizada', 2, 4, 1),
(9, 9, 9, '2026-07-26', '2026-07-29', '2026-07-29', 28000.00, 0.00, 'Finalizada', 3, 5, 1),
(10, 10, 10, '2026-07-20', '2026-07-24', '2026-07-24', 35000.00, 0.00, 'Finalizada', 4, 6, 1),
(11, 11, 11, '2026-07-14', '2026-07-19', '2026-07-19', 42000.00, 0.00, 'Finalizada', 5, 1, 1),
(12, 12, 12, '2026-07-08', '2026-07-14', '2026-07-14', 50000.00, 0.00, 'Finalizada', 6, 2, 1),
(13, 13, 13, '2026-07-02', '2026-07-09', '2026-07-09', 65000.00, 0.00, 'Finalizada', 1, 3, 1),
(14, 14, 14, '2026-06-26', '2026-07-04', '2026-07-04', 78000.00, 0.00, 'Finalizada', 2, 4, 1),
(15, 15, 15, '2026-06-20', '2026-06-29', '2026-06-29', 95000.00, 0.00, 'Finalizada', 3, 5, 1),
(16, 16, 16, '2026-06-14', '2026-06-24', '2026-06-24', 120000.00, 0.00, 'Finalizada', 4, 6, 1),
(17, 17, 17, '2026-06-16', '2026-06-19', '2026-06-19', 28000.00, 0.00, 'Finalizada', 5, 1, 1),
(18, 18, 18, '2026-06-10', '2026-06-14', '2026-06-14', 35000.00, 0.00, 'Finalizada', 6, 2, 1),
(19, 19, 19, '2026-06-04', '2026-06-09', '2026-06-09', 42000.00, 0.00, 'Finalizada', 1, 3, 1),
(20, 20, 20, '2026-05-29', '2026-06-04', '2026-06-04', 50000.00, 0.00, 'Finalizada', 2, 4, 1),
(21, 21, 21, '2026-05-23', '2026-06-03', '2026-05-30', 65000.00, 65000.00, 'Finalizada anticipadamente', 3, 5, 1),
(22, 22, 22, '2026-05-17', '2026-05-27', '2026-05-25', 78000.00, 39000.00, 'Finalizada anticipadamente', 4, 6, 1),
(23, 23, 23, '2026-05-11', '2026-05-23', '2026-05-20', 95000.00, 71250.00, 'Finalizada anticipadamente', 5, 1, 1),
(24, 24, 24, '2026-05-05', '2026-05-19', '2026-05-15', 120000.00, 120000.00, 'Finalizada anticipadamente', 6, 2, 1),
(25, 25, 25, '2026-05-07', '2026-05-12', '2026-05-10', 28000.00, 14000.00, 'Finalizada anticipadamente', 1, 3, 1),
(26, 26, 26, '2026-05-01', '2026-05-08', '2026-05-05', 35000.00, 26250.00, 'Finalizada anticipadamente', 2, 4, 1),
(27, 27, 27, '2026-04-25', '2026-05-04', '2026-04-30', 42000.00, 42000.00, 'Finalizada anticipadamente', 3, 5, 1),
(28, 28, 28, '2026-04-19', '2026-04-27', '2026-04-25', 50000.00, 25000.00, 'Finalizada anticipadamente', 4, 6, 1),
(29, 29, 29, '2026-04-13', '2026-04-23', '2026-04-20', 65000.00, 48750.00, 'Finalizada anticipadamente', 5, 1, 1),
(30, 30, 30, '2026-04-07', '2026-04-19', '2026-04-15', 78000.00, 78000.00, 'Finalizada anticipadamente', 6, 2, 1),
(31, 31, 31, '2026-09-16', '2026-09-20', NULL, 95000.00, 0.00, 'En curso', 1, NULL, 1),
(32, 32, 32, '2026-09-15', '2026-09-21', NULL, 120000.00, 0.00, 'En curso', 2, NULL, 1),
(33, 33, 33, '2026-09-14', '2026-09-22', NULL, 28000.00, 0.00, 'En curso', 3, NULL, 1),
(34, 34, 34, '2026-09-13', '2026-09-23', NULL, 35000.00, 0.00, 'En curso', 4, NULL, 1),
(35, 35, 35, '2026-09-12', '2026-09-24', NULL, 42000.00, 0.00, 'En curso', 5, NULL, 1),
(36, 36, 36, '2026-09-16', '2026-09-25', NULL, 50000.00, 0.00, 'En curso', 6, NULL, 1),
(37, 37, 37, '2026-09-15', '2026-09-26', NULL, 65000.00, 0.00, 'En curso', 1, NULL, 1),
(38, 38, 38, '2026-09-14', '2026-09-20', NULL, 78000.00, 0.00, 'En curso', 2, NULL, 1),
(39, 39, 39, '2026-09-13', '2026-09-21', NULL, 95000.00, 0.00, 'En curso', 3, NULL, 1),
(40, 40, 40, '2026-09-12', '2026-09-22', NULL, 120000.00, 0.00, 'En curso', 4, NULL, 1),
(41, 1, 41, '2026-09-16', '2026-09-23', NULL, 28000.00, 0.00, 'En curso', 5, NULL, 1),
(42, 2, 42, '2026-09-15', '2026-09-24', NULL, 35000.00, 0.00, 'En curso', 6, NULL, 1),
(43, 3, 43, '2026-09-14', '2026-09-25', NULL, 42000.00, 0.00, 'En curso', 1, NULL, 1),
(44, 4, 44, '2026-09-13', '2026-09-26', NULL, 50000.00, 0.00, 'En curso', 2, NULL, 1),
(45, 5, 45, '2026-09-12', '2026-09-20', NULL, 65000.00, 0.00, 'En curso', 3, NULL, 1),
(46, 11, 1, '2026-09-27', '2026-09-30', NULL, 28000.00, 0.00, 'Pendiente', 2, NULL, 1),
(47, 12, 2, '2026-09-29', '2026-10-03', NULL, 35000.00, 0.00, 'Pendiente', 3, NULL, 1),
(48, 13, 3, '2026-10-01', '2026-10-06', NULL, 42000.00, 0.00, 'Pendiente', 4, NULL, 1),
(49, 14, 4, '2026-10-03', '2026-10-09', NULL, 50000.00, 0.00, 'Pendiente', 5, NULL, 1),
(50, 15, 5, '2026-10-05', '2026-10-12', NULL, 65000.00, 0.00, 'Pendiente', 6, NULL, 1),
(51, 16, 6, '2026-10-07', '2026-10-15', NULL, 78000.00, 0.00, 'Pendiente', 1, NULL, 1),
(52, 17, 7, '2026-10-09', '2026-10-18', NULL, 95000.00, 0.00, 'Pendiente', 2, NULL, 1),
(53, 18, 8, '2026-10-11', '2026-10-21', NULL, 120000.00, 0.00, 'Pendiente', 3, NULL, 1),
(54, 19, 9, '2026-10-13', '2026-10-24', NULL, 28000.00, 0.00, 'Pendiente', 4, NULL, 1),
(55, 20, 10, '2026-10-15', '2026-10-27', NULL, 35000.00, 0.00, 'Pendiente', 5, NULL, 1),
(56, 21, 11, '2026-10-17', '2026-10-20', NULL, 42000.00, 0.00, 'Pendiente', 6, NULL, 1),
(57, 22, 12, '2026-10-19', '2026-10-23', NULL, 50000.00, 0.00, 'Pendiente', 1, NULL, 1),
(58, 23, 13, '2026-10-21', '2026-10-26', NULL, 65000.00, 0.00, 'Pendiente', 2, NULL, 1),
(59, 24, 14, '2026-10-23', '2026-10-29', NULL, 78000.00, 0.00, 'Pendiente', 3, NULL, 1),
(60, 25, 15, '2026-10-25', '2026-11-01', NULL, 95000.00, 0.00, 'Pendiente', 4, NULL, 1),
(61, 26, 16, '2026-10-27', '2026-11-04', NULL, 120000.00, 0.00, 'Pendiente', 5, NULL, 1),
(62, 27, 17, '2026-10-29', '2026-11-07', NULL, 28000.00, 0.00, 'Pendiente', 6, NULL, 1),
(63, 28, 18, '2026-10-31', '2026-11-10', NULL, 35000.00, 0.00, 'Pendiente', 1, NULL, 1),
(64, 29, 19, '2026-11-02', '2026-11-13', NULL, 42000.00, 0.00, 'Pendiente', 2, NULL, 1),
(65, 30, 20, '2026-11-04', '2026-11-16', NULL, 50000.00, 0.00, 'Pendiente', 3, NULL, 1);

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
(2, 'Departamento', 1),
(3, 'Monoambiente', 1),
(4, 'Cabaña', 1),
(5, 'Chalet', 1);

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
(1, 'admin@luvarem.com', '1234', 'Administrador', 'Ana', 'García', NULL, 1),
(2, 'admin2@luvarem.com', '1234', 'Administrador', 'Carlos', 'Pérez', NULL, 1),
(3, 'empleado1@luvarem.com', '1234', 'Empleado', 'Lucía', 'Sosa', NULL, 1),
(4, 'empleado2@luvarem.com', '1234', 'Empleado', 'Martín', 'López', NULL, 1),
(5, 'empleado3@luvarem.com', '1234', 'Empleado', 'Sofía', 'Torres', NULL, 1),
(6, 'empleado4@luvarem.com', '1234', 'Empleado', 'Diego', 'Romero', NULL, 1);

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=41;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=41;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `reserva`
--
ALTER TABLE `reserva`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=66;

--
-- AUTO_INCREMENT de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

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
