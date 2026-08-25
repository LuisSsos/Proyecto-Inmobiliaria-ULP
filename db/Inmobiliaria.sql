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

INSERT INTO `propietario` (`nombre`, `dni_cuit`, `email`, `telefono`) VALUES
('Pablo Poder', '20-39482103-8', 'pablo.poder@email.com', '+54 9 266 412-3456'),
('Mariano Luzza', '20-26593841-3', 'mariano.luzza@email.com', '+54 9 266 423-4567'),
('Luis Mercado', '20-26482019-5', 'luis.mercado@email.com', '+54 9 266 434-5678'),
('Florencia Castro', '27-38192043-4', 'florencia.castro@email.com', '+54 9 266 445-6789');


INSERT INTO `inquilino` (`dni`, `nombre_completo`, `email`, `telefono`) VALUES
('42193840', 'Luis Sosa', 'luis.sosa@email.com', '+54 9 266 456-7890'),
('34291048', 'Iván Auriol', 'ivan.auriol@email.com', '+54 9 266 467-8901'),
('37482910', 'Romina Auriol', 'romina.auriol@email.com', '+54 9 266 478-9012'),
('37829104', 'Brenda Efler', 'brenda.efler@email.com', '+54 9 266 489-0123');