
# Labels API 
La API de Disquera está diseñada para gestionar información relacionada con una disquera, incluyendo categorías, compositores, etiquetas, revisores, críticas, canciones y más. A continuación, se detallan las principales entidades y relaciones de la API:

# Categorías (Category)
Descripción: Las categorías representan los géneros o estilos musicales en los que se pueden clasificar los álbumes y canciones de la disquera.

Rutas de la API:

`GET /api/categories: Obtiene la lista de todas las categorías.`

`GET /api/categories/{id}: Obtiene una categoría específica por su ID.`

`POST /api/categories: Crea una nueva categoría.`

`PUT /api/categories/{id}: Actualiza una categoría existente.`

`DELETE /api/categories/{id}: Elimina una categoría.`


# Compositores (Composer)
Descripción: Descripción: Los compositores son los autores de las canciones de la disquera.

Rutas de la API:
`GET /api/composers`: Obtiene la lista de todos los compositores.

`GET /api/composers/{id}`: Obtiene un compositor específico por su ID.

`POST /api/composers`: Crea un nuevo compositor.

`PUT /api/composers/{id}`: Actualiza un compositor existente.

`DELETE /api/composers/{id}`: Elimina un compositor.


# Etiquetas (Label)

Descripción: Las etiquetas representan las casas discográficas asociadas con la disquera.

Rutas de la API:

`GET /api/labels`: Obtiene la lista de todas las etiquetas.

`GET /api/labels/{id}`: Obtiene una etiqueta específica por su ID.

`POST /api/labels`: Crea una nueva etiqueta.

`PUT /api/labels/{id}`: Actualiza una etiqueta existente.

`DELETE /api/labels/{id}`: Elimina una etiqueta.


# Revisores (Reviewer)

Descripción: Los revisores son personas encargadas de calificar y revisar los álbumes y canciones de la disquera.

Rutas de la API:

`GET /api/reviewers`: Obtiene la lista de todos los revisores.

`GET /api/reviewers/{id}`: Obtiene un revisor específico por su ID.

`POST /api/reviewers`: Crea un nuevo revisor.

`PUT /api/reviewers/{id}`: Actualiza un revisor existente.

`DELETE /api/reviewers/{id}`: Elimina un revisor.


# Críticas (Reviews)

Descripción: Las críticas representan las opiniones y calificaciones dadas por los revisores a álbumes y canciones específicas.

Rutas de la API:

`GET /api/reviews`: Obtiene la lista de todas las críticas.

`GET /api/reviews/{id}`: Obtiene una crítica específica por su ID.

`POST /api/reviews`: Crea una nueva crítica.

`PUT /api/reviews/{id}`: Actualiza una crítica existente.

`DELETE /api/reviews/{id}`: Elimina una crítica.


# Canciones (Song)

Descripción: Las canciones son las obras musicales individuales producidas por la disquera.

Rutas de la API:

`GET /api/songs`: Obtiene la lista de todas las canciones.

`GET /api/songs/{id}`: Obtiene una canción específica por su ID.

`POST /api/songs`: Crea una nueva canción.

`PUT /api/songs/{id}`: Actualiza una canción existente.

`DELETE /api/songs/{id}`: Elimina una canción.

