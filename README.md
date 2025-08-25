# CampusLoveDarwinDaniel

Autores:
Darwin Felipe Arenas Carvajal 
Daniel Alejandro Duran Franco

Campus Love es una aplicación de consola en C# que simula un sistema de emparejamiento. El proyecto busca implementar un flujo completo donde los usuarios pueden registrarse, ver perfiles de otras personas, hacer “like” o “dislike” y revisar sus coincidencias (matches). 



﻿﻿Se debe permitir la simulación de interacciones entre usuarios de forma iterativa, implementando estructuras de control, colecciones, patrones de diseño, principios SOLID, y formateo adecuado de datos.



Adicionalmente, se podrá agregar un sistema de "créditos de interacción" que limite la cantidad de likes por día, usando lógica con Math, validaciones y operadores.



Contexto y Especificaciones:



Diseñar una aplicación de consola basada en arquitectura limpia, principios SOLID y patrones de diseño, que simula un sistema de emparejamiento completo entre usuarios.

El sistema debe permitir registrar:

Registro de usuarios (nombre, edad, género, intereses, carrera, frase de perfil).
Visualización de perfiles disponibles (uno por uno) para hacer Like o Dislike.
Ver coincidencias si dos usuarios se dan Like mutuamente.
Listar todas las coincidencias de un usuario.
Limitar la cantidad de likes diarios por usuario con lógica de Math.
Mostrar estadísticas de interacción usando LINQ (por ejemplo, el usuario con más likes recibidos).
Usar CultureInfo y NumberFormat para mostrar créditos, nombres en formato título, etc.


Requisitos funcionales:



Menú en consola con las siguientes opciones:
Registrarse como nuevo usuario
Ver perfiles y dar Like o Dislike
Ver mis coincidencias (matches)
Ver estadísticas del sistema (usuarios con más likes, más matches, etc.)
Salir


Guardar usuarios y sus interacciones en listas o diccionarios
Permitir simulación de múltiples usuarios (modo multicliente ficticio).
Aplicar patrón Factory para creación de usuarios o interacciones.
Usar principios SOLID y separación clara de responsabilidades.


Requisitos no funcionales:



Interacción con la consola de forma amigable, clara y fluida.
Código organizado, con clases separadas (Usuario, Interaccion, MatchService, etc.).
Separación clara entre lógica de validación, lógica de negocio y presentación.
Validación de entrada (edad, texto, género).
Uso de conversiones con int.Parse, TryParse, ToUpper, ToLower, etc.
Manejo adecuado de entrada de datos.
Diagramas



Diagrama de clases: Usuario, Match, GestorUsuarios, Interaccion, etc.


Herramientas y tecnologías:

Lenguaje: C#
Plataforma: .NET Core 8.0
IDE sugerido: VS Code


Sugerencias:

Usa List<Usuario>, Dictionary<int, List<int>> para guardar likes.
Usa LINQ para buscar matches, ordenar por likes, contar usuarios.
Aplica el patrón Strategy para definir reglas de emparejamiento (por intereses, edad, carrera).
Usa Math.Min, Math.Max para controlar cantidad de likes diarios.


Recursos:

Documentación LINQ
Material sobre patrones de diseño Refactoring Guru
Usuarios campus love




Resultado esperado

La entrega de esta asignación es un enlace a un repositorio en GitHub debe contener  el proyecto solicitado (es decir, un paquete por componente y cada componente con su estructura MVC). Adicionalmente a esto, los commits y el código deben ser realizados en inglés y el Readme debe estar organizado y haciendo una descripción adecuada del proyecto definiendo y especificando todos los aspectos en su totalidad.



Junto con el código fuente se deben entregar (El trainer podría solicitar más o menos ítems de este apartado):

Diagrama de base de datos (ER) completo para todo el software.
Diagrama de clases completo para todo el software.
Archivos SQL  que soporten el diagrama.
Una lista de las tablas de base de datos que utiliza cada componente (una tabla podría aparecer en más de un componente).
Una lista de las clases que utiliza cada componente (una clase la podría utilizar más de un componente).


REQUERIMIENTO ADICIONAL DEL PROYECTO
USO DE BASE DE DATOS Y PERSISTENCIA