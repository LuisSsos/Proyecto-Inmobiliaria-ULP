CREATE TABLE `propietario` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `nombre` varchar(255),
  `dni_cuit` varchar(255),
  `email` varchar(255),
  `telefono` varchar(255)
);

CREATE TABLE `tipo_inmueble` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `nombre` varchar(255)
);

CREATE TABLE `inmueble` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `propietario_id` int,
  `tipo_inmueble_id` int,
  `direccion` varchar(255),
  `cupo` int,
  `latitud` decimal,
  `longitud` decimal,
  `precio_por_dia` decimal,
  `porcentaje_seña` decimal,
  `estado` varchar(255)
);

CREATE TABLE `imagen_inmueble` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `inmueble_id` int,
  `url` varchar(255),
  `es_portada` bool
);

CREATE TABLE `inquilino` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `dni` varchar(255),
  `nombre_completo` varchar(255),
  `email` varchar(255),
  `telefono` varchar(255)
);

CREATE TABLE `usuario` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `email` varchar(255),
  `password_hash` varchar(255),
  `rol` varchar(255),
  `nombre` varchar(255),
  `apellido` varchar(255),
  `avatar` varchar(255)
);

CREATE TABLE `reserva` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `inquilino_id` int,
  `inmueble_id` int,
  `fecha_desde` date,
  `fecha_hasta` date,
  `fecha_fin_real` date,
  `monto_por_dia` decimal,
  `multa` decimal,
  `estado` varchar(255),
  `usuario_creador_id` int,
  `usuario_terminador_id` int
);

CREATE TABLE `pago` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `reserva_id` int,
  `concepto` varchar(255),
  `fecha_pago` date,
  `importe` decimal,
  `estado` varchar(255),
  `usuario_creador_id` int,
  `usuario_anulador_id` int
);

ALTER TABLE `inmueble` ADD FOREIGN KEY (`propietario_id`) REFERENCES `propietario` (`id`);

ALTER TABLE `inmueble` ADD FOREIGN KEY (`tipo_inmueble_id`) REFERENCES `tipo_inmueble` (`id`);

ALTER TABLE `imagen_inmueble` ADD FOREIGN KEY (`inmueble_id`) REFERENCES `inmueble` (`id`);

ALTER TABLE `reserva` ADD FOREIGN KEY (`inquilino_id`) REFERENCES `inquilino` (`id`);

ALTER TABLE `reserva` ADD FOREIGN KEY (`inmueble_id`) REFERENCES `inmueble` (`id`);

ALTER TABLE `reserva` ADD FOREIGN KEY (`usuario_creador_id`) REFERENCES `usuario` (`id`);

ALTER TABLE `reserva` ADD FOREIGN KEY (`usuario_terminador_id`) REFERENCES `usuario` (`id`);

ALTER TABLE `pago` ADD FOREIGN KEY (`reserva_id`) REFERENCES `reserva` (`id`);

ALTER TABLE `pago` ADD FOREIGN KEY (`usuario_creador_id`) REFERENCES `usuario` (`id`);

ALTER TABLE `pago` ADD FOREIGN KEY (`usuario_anulador_id`) REFERENCES `usuario` (`id`);
