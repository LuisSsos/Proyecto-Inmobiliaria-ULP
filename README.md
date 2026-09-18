# Proyecto-Inmobiliaria-ULP

---

## Integrantes del grupo

* **Luis Ezequiel Sosa**
* **Iván Oscar Auriol López**
* **Florencia Magalí Castro**
* **Tomas Migliozzi Badani**

---

## Instrucciones para ejecutar el proyecto

### 1. Clonar el repositorio

Abrí tu terminal y ejecutá los siguientes comandos:

```bash
git clone https://github.com/LuisSsos/Proyecto-Inmobiliaria-ULP
cd Proyecto-Inmobiliaria-ULP
```

También podés descargarlo comprimido, descomprimirlo y abrir la carpeta en tu editor de confianza.

---

### 2. Levantá la db

Podés importar el archivo `Inmobiliaria.sql` en tu gestor de base de datos (por ejemplo phpMyAdmin con XAMPP) o copiar y pegar las líneas directamente del archivo y pegarlas en la caja de consultas de tu gestor para poder ejecutarlo.

---

### 3. Configurar conexión

Verificá que el archivo `appsettings.json` coincida con las credenciales de tu servidor MySQL. Por defecto está configurada así:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=inmobiliaria;user=root;password=;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Si usás DBeaver procurá poner el usuario y la contraseña de usuario en el `appsettings.json` o, por el contrario, quitar la contraseña y cambiar a usuario `root`.

phpMyAdmin suele venir por defecto con las configuraciones de usuario `root` y sin contraseña, pero si en algún momento lo has cambiado, tené en cuenta modificarlo para poder correr este proyecto.

---
## 4. Credenciales de prueba

Para poder probar el sistema se pueden utilizar los siguientes usuarios:

### Administradores

| Email | Contraseña | Nombre | Apellido |
|---|---|---|---|
| `admin@luvarem.com` | `1234` | Ana | García |
| `admin2@luvarem.com` | `1234` | Carlos | Pérez |

### Empleados

| Email | Contraseña | Nombre | Apellido |
|---|---|---|---|
| `empleado1@luvarem.com` | `1234` | Lucía | Sosa |
| `empleado2@luvarem.com` | `1234` | Martín | López |
| `empleado3@luvarem.com` | `1234` | Sofía | Torres |
| `empleado4@luvarem.com` | `1234` | Diego | Romero |

Los usuarios **Administrador** cuentan con acceso completo al sistema, incluyendo la gestión de otros usuarios y la eliminación de entidades.

Los usuarios **Empleado** pueden realizar las operaciones correspondientes a su rol, pero no pueden eliminar entidades ni gestionar otros usuarios.
---

### 5. Ejecutar el proyecto

En una consola, sea del VS Code o Git Bash, parate dentro del directorio del proyecto y ejecutá:

```bash
dotnet run
```

---

## 6. Algo muy importante: la navegación (Rutas)

Al ejecutar el proyecto, la consola te va a indicar en qué URL y puerto se está alojando (por ejemplo, `http://localhost:5063`).

Este puerto se define en el archivo `Properties/launchSettings.json` de cada entorno.

---

# Modelado

Se podrá ver el diagrama relacional en la carpeta `db`.

### Esquema de Base de Datos

![Esquema de Base de Datos](db/Inmobiliaria.png)

---

# Estado final del desarrollo

El proyecto se encuentra terminado y cuenta con las funcionalidades necesarias para la gestión de alquileres de una agencia inmobiliaria.

* Configuración de la base de datos **MySQL** y de la inyección de dependencias mediante `RepositorioBase`.

* Desarrollo de **modelos, repositorios y controladores** para las entidades:

  * **Propietario**
  * **Inquilino**
  * **Inmueble**
  * **Reserva**
  * **TipoInmueble**
  * **Pago**
  * **Usuario**

* Implementación de vistas Razor con operaciones CRUD para permitir listar, crear, editar y eliminar registros según los permisos correspondientes.

* Implementación de la gestión de **Propietarios**, permitiendo registrar sus datos personales y de contacto, consultar los inmuebles que posee y administrar la información correspondiente.

* Implementación de la gestión de **Inquilinos**, permitiendo registrar sus datos personales y de contacto y consultar los contratos en los que participa.

* Implementación de la gestión de **Inmuebles**, permitiendo registrar:

  * Dirección.
  * Uso residencial o comercial.
  * Tipo de inmueble.
  * Cantidad de ambientes.
  * Coordenadas.
  * Precio.

* Implementación de la gestión de **Tipos de Inmueble**, permitiendo administrar los distintos tipos de propiedades que maneja la inmobiliaria.

* Implementación de la gestión de **Contratos de alquiler**, permitiendo registrar:

  * Inquilino.
  * Inmueble.
  * Fecha de inicio.
  * Fecha de finalización.
  * Monto mensual del alquiler.

* Implementación de validaciones de negocio para los contratos:

  * No se permite guardar un contrato con fecha de inicio posterior o igual a la fecha de finalización.
  * No se permite crear un contrato con fecha de inicio anterior al día actual.
  * No se permite contratar un inmueble en fechas que se superpongan con otro contrato vigente para la misma propiedad.
  * Se verifica nuevamente la disponibilidad del inmueble al momento de crear el contrato.

* Implementación de la **búsqueda de inmuebles disponibles**, permitiendo indicar características del inmueble y un período de alquiler para obtener propiedades que no se encuentren ocupadas durante esas fechas.

* Implementación de la **suspensión de inmuebles**. Un Administrador o Empleado puede suspender temporalmente un inmueble desde el listado, haciendo que deje de aparecer disponible para nuevas operaciones. Esto no afecta los contratos ya existentes y el inmueble puede volver a activarse posteriormente.

* Implementación de la gestión de **Pagos asociados a los contratos**

* En la edición de un pago, únicamente se permite modificar el **concepto o detalle**, manteniendo sin modificaciones el número de pago, la fecha y el importe.

* Implementación de la **anulación lógica de pagos**, manteniendo el registro en el sistema y mostrando que el pago se encuentra anulado, en lugar de eliminarlo físicamente.

* Implementación de la funcionalidad para **terminar un contrato anticipadamente**, registrando la fecha efectiva de terminación y calculando la multa correspondiente según el tiempo transcurrido del contrato:

  * Si se cumplió menos de la mitad del tiempo original de alquiler, la multa corresponde a dos meses extra de alquiler.
  * Si se cumplió la mitad o más del tiempo original de alquiler, la multa corresponde a un mes extra de alquiler.
  * Se verifica que no existan meses de alquiler adeudados.
  * El sistema informa el valor de la multa y permite cargarla como pago desde la misma pantalla.
  * Se conserva la fecha de finalización original del contrato para no perder información y permitir volver a realizar el cálculo de la multa.

* Implementación de la funcionalidad para **renovar contratos de alquiler**, permitiendo generar un nuevo contrato para el mismo inquilino e inmueble, pero con nuevas fechas y un nuevo monto de alquiler.

* Implementación de **autenticación y autorización mediante cookies**:

  * Login con email y contraseña (`/auth/login`).
  * Roles diferenciados: **Administrador** y **Empleado**.
  * Los Administradores tienen acceso total al sistema, incluyendo la eliminación de entidades y la gestión de otros usuarios.
  * Los Empleados pueden crear y modificar información, pero no eliminar entidades.
  * Cada usuario puede editar su propio perfil desde `/Usuario/MiPerfil`.
  * Los empleados pueden modificar sus propios datos personales, contraseña y avatar.
  * Solo los Administradores pueden gestionar a otros usuarios.

* Implementación de **auditoría de contratos**, registrando qué usuario creó cada contrato y, cuando corresponde, qué usuario realizó su terminación.

* Implementación de **auditoría de pagos**, registrando qué usuario creó cada pago y, cuando corresponde, qué usuario realizó su anulación.

* La información de auditoría se encuentra restringida a los **Administradores** y puede consultarse desde la vista de detalles de la entidad correspondiente.

* Implementación de un **middleware de manejo de excepciones a nivel global**, que captura cualquier error no controlado de la aplicación y redirige a una vista de error personalizada, evitando mostrar el detalle técnico al usuario.

* Desarrollo del módulo de **imágenes de inmuebles**, permitiendo:

  * Subir fotos asociadas a cada propiedad.
  * Listar las fotos cargadas.
  * Eliminar fotos.
  * Guardar las imágenes mediante almacenamiento local.
  * Guardar en la base de datos la ubicación correspondiente de cada imagen.

* Incorporación de la **imagen de portada del inmueble** en el listado principal de Inmuebles, mostrando la foto cuando existe.

---

# Informes y listados

El sistema cuenta con diferentes consultas y listados para facilitar la gestión de la inmobiliaria:

* Listado de todos los **inmuebles y su propietario**, permitiendo filtrar por el estado del inmueble y consultar cuáles se encuentran disponibles.

* Listado de todos los **inmuebles correspondientes a un propietario**.

* Listado de todos los **contratos de alquiler vigentes**, teniendo en cuenta sus fechas de inicio y finalización.

* Listado de todos los **contratos correspondientes a un inmueble**, incluyendo la información del inquilino.

* Listado de los **contratos próximos a finalizar**, permitiendo elegir un plazo de 30, 60 o 90 días.

* Listado de los **pagos correspondientes a un contrato**, permitiendo cargar un nuevo pago desde la misma pantalla.

* Búsqueda de **inmuebles disponibles entre dos fechas**, mostrando las propiedades que no se encuentran ocupadas por otro contrato durante el período seleccionado.

---

# Actualización de esquema: campo `activo`

La base de datos utiliza el campo `activo` para realizar bajas lógicas, sin eliminar físicamente los registros, en las entidades correspondientes.

Si la base de datos fue clonada con una versión anterior del script `.sql`, es posible que falte alguna de estas columnas. Si al ejecutar el proyecto hay un error como `Unknown column 'activo'`, corré lo siguiente en tu BD:

```sql
ALTER TABLE usuario ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;
ALTER TABLE propietario ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;
ALTER TABLE inmueble ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;
ALTER TABLE inquilino ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;
ALTER TABLE reserva ADD COLUMN activo TINYINT(1) NOT NULL DEFAULT 1;
```

Si se clona el repo por primera vez, asegurate de usar el script `.sql` actualizado, que ya incluye estas columnas.

---

# Consideraciones

El sistema fue desarrollado para informatizar la gestión de alquileres de una agencia inmobiliaria, centralizando la administración de propietarios, inquilinos, inmuebles, contratos, pagos y usuarios.

Las operaciones disponibles dependen del rol del usuario y las acciones que impliquen eliminación se realizan mediante bajas lógicas cuando corresponde, conservando la información en el sistema.
