# Documentacion general de algoritmos graficos

Este documento explica los algoritmos implementados en el proyecto:

```text
LINEAS
CONICAS
RELLENO
```

El proyecto esta organizado con una estructura tipo MVC:

```text
Controller: contiene la logica del algoritmo.
Model: contiene las clases de datos.
View: contiene los formularios y la parte visual.
```

## 1. Algoritmos de lineas

Los algoritmos de lineas sirven para dibujar una recta entre dos puntos:

```text
P1 = (x1, y1)
P2 = (x2, y2)
```

En el proyecto se usan tres algoritmos:

```text
DDA
Bresenham
Punto Medio de Linea
```

Cada uno calcula los puntos que deben pintarse en la pantalla para aproximar una recta.

### 1.1. Algoritmo DDA

DDA significa:

```text
Digital Differential Analyzer
```

Este algoritmo calcula la recta usando incrementos. Primero obtiene:

```text
dx = x2 - x1
dy = y2 - y1
```

Luego calcula la cantidad de pasos:

```text
k = max(|dx|, |dy|)
```

Despues calcula los incrementos:

```text
incrementoX = dx / k
incrementoY = dy / k
```

El algoritmo empieza en el punto inicial y suma esos incrementos en cada paso.

Proceso:

```text
1. Leer P1 y P2.
2. Calcular dx y dy.
3. Calcular k.
4. Calcular incrementoX e incrementoY.
5. Empezar en x = x1, y = y1.
6. Pintar round(x), round(y).
7. Sumar incrementoX e incrementoY.
8. Repetir hasta completar k pasos.
```

Ejemplo conceptual:

```text
x = x + incrementoX
y = y + incrementoY
```

Ventajas:

```text
Es facil de entender.
Funciona para cualquier pendiente.
```

Desventajas:

```text
Usa numeros decimales.
Necesita redondear.
Puede ser menos eficiente que Bresenham.
```

En el proyecto:

```text
Carpeta: LINEAS/DDA
Controlador: DDAController
Formulario: FrmDDA
```

### 1.2. Algoritmo de Bresenham

El algoritmo de Bresenham dibuja lineas usando operaciones enteras y una variable de decision.

La idea principal es decidir, en cada paso, cual pixel queda mas cerca de la recta ideal.

En vez de trabajar con decimales, usa un error acumulado.

Proceso general:

```text
1. Leer P1 y P2.
2. Calcular dx y dy.
3. Determinar la direccion de avance.
4. Inicializar la variable de decision.
5. Avanzar pixel por pixel.
6. Ajustar el error.
7. Elegir si se avanza solo en x o tambien en y.
```

Para una linea con pendiente entre 0 y 1, la decision basica es:

```text
Si el error indica que la recta esta mas arriba:
    avanzar en x y en y
Si no:
    avanzar solo en x
```

Ventajas:

```text
Es eficiente.
Usa principalmente enteros.
Es muy usado en graficacion por computadora.
```

Desventajas:

```text
Es mas dificil de entender que DDA.
Requiere manejar bien la variable de decision.
```

En el proyecto:

```text
Carpeta: LINEAS/Bresenham
Controlador: BresenhamController
Formulario: FrmBresenham
```

### 1.3. Algoritmo de Punto Medio de Linea

El algoritmo de punto medio de linea tambien usa una variable de decision.

Su idea es evaluar un punto medio entre dos posibles pixeles y decidir cual esta mas cerca de la recta.

En cada paso existen dos opciones:

```text
Elegir el pixel horizontal.
Elegir el pixel diagonal.
```

El algoritmo calcula una decision para saber que pixel elegir.

Proceso general:

```text
1. Leer P1 y P2.
2. Calcular dx y dy.
3. Definir la variable de decision inicial.
4. Pintar el punto actual.
5. Evaluar el punto medio.
6. Elegir el siguiente pixel.
7. Actualizar la variable de decision.
8. Repetir hasta llegar al punto final.
```

Ventajas:

```text
Evita calculos decimales.
Es una base importante para entender otros algoritmos de rasterizacion.
```

Desventajas:

```text
La logica de decision puede ser menos directa que DDA.
```

En el proyecto:

```text
Carpeta: LINEAS/PuntoMedioLinea
Controlador: PuntoMedioLineaController
Formulario: FrmPuntoMedioLinea
```

## 2. Algoritmos de circulo

Los algoritmos de circulo generan puntos alrededor de un centro:

```text
C = (xc, yc)
radio = r
```

En el proyecto se usan tres algoritmos:

```text
Punto Medio de Circulo
Parametrico de Circulo
Polar de Circulo
```

Todos dibujan una circunferencia, pero calculan los puntos de manera diferente.

### 2.1. Algoritmo de Punto Medio de Circulo

Este algoritmo se conoce como:

```text
Midpoint Circle Algorithm
Algoritmo de Punto Medio para Circunferencia
```

La circunferencia cumple:

```text
x^2 + y^2 = r^2
```

El algoritmo aprovecha la simetria del circulo. Calcula puntos de un octante y luego los refleja en los otros siete octantes.

Simetrias:

```text
( x,  y)
(-x,  y)
( x, -y)
(-x, -y)
( y,  x)
(-y,  x)
( y, -x)
(-y, -x)
```

Empieza con:

```text
x = 0
y = r
p = 1 - r
```

La variable `p` decide el siguiente punto.

Reglas:

```text
Si p < 0:
    avanzar hacia el Este
    p = p + 2x + 3

Si p >= 0:
    avanzar hacia el Sureste
    y = y - 1
    p = p + 2(x - y) + 5
```

Proceso general:

```text
1. Leer centro y radio.
2. Iniciar x = 0, y = r.
3. Calcular p = 1 - r.
4. Guardar el punto base.
5. Mientras x < y:
    aumentar x
    evaluar p
    si es necesario disminuir y
    guardar el punto base
6. Reflejar cada punto en los 8 octantes.
7. Dibujar todos los puntos.
```

Ventajas:

```text
Es eficiente.
Usa operaciones enteras.
Aprovecha la simetria del circulo.
```

Desventajas:

```text
Es mas dificil de entender al inicio por la variable de decision.
```

En el proyecto:

```text
Carpeta: CONICAS/PuntoMedioCirculo
Controlador: PuntoMedioCirculoController
Formulario: FrmPuntoMedioCirculo
```

### 2.2. Algoritmo Parametrico de Circulo

Este algoritmo usa ecuaciones parametricas.

La circunferencia se puede representar asi:

```text
x = xc + r cos(t)
y = yc + r sen(t)
```

Donde:

```text
xc, yc = centro
r = radio
t = angulo
```

El algoritmo recorre valores de `t`, normalmente de 0 a 360 grados, y calcula un punto para cada angulo.

Proceso general:

```text
1. Leer centro y radio.
2. Recorrer t desde 0 hasta 360 grados.
3. Calcular x = xc + r cos(t).
4. Calcular y = yc + r sen(t).
5. Redondear x e y.
6. Guardar el punto si no esta repetido.
7. Dibujar los puntos.
```

Ventajas:

```text
Es facil de entender.
Relaciona directamente el circulo con trigonometria.
```

Desventajas:

```text
Usa funciones trigonometricas.
Usa decimales.
Puede generar puntos repetidos al redondear.
```

En el proyecto:

```text
Carpeta: CONICAS/ParametricoCirculo
Controlador: ParametricoCirculoController
Formulario: FrmParametricoCirculo
```

### 2.3. Algoritmo Polar de Circulo

El algoritmo polar usa coordenadas polares.

En coordenadas polares, un punto se define con:

```text
r = radio
theta = angulo
```

Para convertir de polar a cartesiano se usa:

```text
x = r cos(theta)
y = r sen(theta)
```

Luego se suma el centro:

```text
x = xc + r cos(theta)
y = yc + r sen(theta)
```

Proceso general:

```text
1. Leer centro y radio.
2. Mantener el radio constante.
3. Recorrer theta desde 0 hasta 360 grados.
4. Convertir cada punto polar a cartesiano.
5. Redondear el resultado.
6. Dibujar los puntos generados.
```

Ventajas:

```text
Es intuitivo porque el radio se mantiene constante.
Permite entender el circulo como barrido angular.
```

Desventajas:

```text
Usa senos y cosenos.
Depende del paso angular.
Puede repetir puntos por redondeo.
```

En el proyecto:

```text
Carpeta: CONICAS/PolarCirculo
Controlador: PolarCirculoController
Formulario: FrmPolarCirculo
```

## 3. Algoritmos de relleno

Los algoritmos de relleno sirven para pintar el interior de una figura cerrada.

La figura se representa en una matriz. Cada celda puede ser:

```text
Vacia
Borde
Pintada
```

La celda inicial se llama:

```text
Semilla
```

La semilla debe estar dentro de la figura. Si la figura no esta cerrada, el relleno puede escaparse hacia afuera.

En el proyecto se usan tres algoritmos:

```text
Relleno por Semilla
Relleno por Pila
Relleno Scanline
```

### 3.1. Relleno por Semilla

Este algoritmo empieza desde una celda semilla.

Desde esa celda revisa sus vecinos:

```text
Arriba
Derecha
Abajo
Izquierda
```

En tu proyecto, este algoritmo usa una cola, por eso avanza como BFS.

Una cola funciona asi:

```text
El primero que entra es el primero que sale.
```

Eso hace que el relleno avance por capas desde la semilla.

Orden usado en el codigo:

```text
1. Arriba
2. Derecha
3. Abajo
4. Izquierda
```

Como usa una cola, los vecinos se pintan en el mismo orden en que entran. Es decir, si desde una celda puede avanzar a los cuatro lados, primero se procesara arriba, luego derecha, luego abajo y finalmente izquierda.

Ejemplo desde una semilla `S`:

```text
    2
5   S   3
    4
```

La celda `S` se pinta primero. Luego se agregan sus vecinos en este orden:

```text
Arriba -> Derecha -> Abajo -> Izquierda
```

Como es cola, ese mismo orden sera el orden de salida.

Proceso general:

```text
1. Insertar la semilla en la cola.
2. Sacar una celda de la cola.
3. Verificar si esta dentro de la matriz.
4. Verificar si esta vacia.
5. Pintarla.
6. Agregar sus vecinos vacios a la cola.
7. Repetir hasta que la cola quede vacia.
```

Ventajas:

```text
El relleno se ve ordenado.
Avanza de forma uniforme desde el centro.
Es facil ver la expansion por capas.
```

Figuras usadas:

```text
Cuadrado
Circulo
```

En el proyecto:

```text
Carpeta: RELLENO/RellenoPorSemilla
Controlador: RellenoPorSemillaController
Formulario: FrmRellenoPorSemilla
```

### 3.2. Relleno por Pila

Este algoritmo tambien empieza desde una semilla, pero usa una pila.

Una pila funciona asi:

```text
El ultimo que entra es el primero que sale.
```

Eso se conoce como LIFO.

Por esta razon, el algoritmo se mete por un camino, avanza lo mas que puede y luego vuelve a otras celdas pendientes.

En el codigo, los vecinos tambien se agregan en este orden:

```text
1. Arriba
2. Derecha
3. Abajo
4. Izquierda
```

Pero como se usa una pila, el ultimo vecino agregado es el primero que se procesa.

Eso significa que, aunque se agreguen asi:

```text
Arriba -> Derecha -> Abajo -> Izquierda
```

se procesan asi:

```text
Izquierda -> Abajo -> Derecha -> Arriba
```

Ejemplo desde una semilla `S`:

```text
    5
2   S   4
    3
```

La semilla `S` se pinta primero. Luego se insertan sus vecinos en la pila. Como izquierda fue el ultimo vecino insertado, izquierda sale primero.

Por eso el relleno por pila no se ve como una expansion pareja. Se va por una direccion, profundiza, y cuando ya no puede seguir, regresa a otros puntos guardados en la pila.

Proceso general:

```text
1. Insertar la semilla en la pila.
2. Sacar una celda de la pila.
3. Verificar si esta dentro de la matriz.
4. Verificar si esta vacia.
5. Pintarla.
6. Insertar sus vecinos vacios en la pila.
7. Repetir hasta que la pila quede vacia.
```

Ventajas:

```text
No usa recursividad directa.
Permite ver un recorrido por profundidad.
```

Desventajas:

```text
Visualmente puede parecer menos uniforme que el relleno con cola.
```

Figuras usadas:

```text
Triangulo
Rombo
```

En el proyecto:

```text
Carpeta: RELLENO/RellenoPorPila
Controlador: RellenoPorPilaController
Formulario: FrmRellenoPorPila
```

### 3.3. Relleno Scanline

El relleno scanline trabaja por lineas horizontales.

En vez de pintar una celda y luego saltar a sus vecinos, busca un tramo horizontal completo dentro de la figura.

Desde la semilla:

```text
Busca hacia la izquierda hasta encontrar borde.
Busca hacia la derecha hasta encontrar borde.
Pinta todo ese tramo horizontal.
Revisa la fila superior.
Revisa la fila inferior.
```

En este algoritmo no se mueve como:

```text
arriba -> derecha -> abajo -> izquierda
```

Su movimiento principal es horizontal.

Primero hace esto:

```text
1. Desde la semilla camina a la izquierda hasta encontrar borde.
2. Desde la semilla camina a la derecha hasta encontrar borde.
3. Con eso obtiene una linea completa.
4. Pinta esa linea.
```

Por ejemplo, si la semilla esta en medio de una figura:

```text
###########
#         #
#    S    #
#         #
###########
```

El algoritmo encuentra el tramo horizontal:

```text
###########
#         #
#*********#
#         #
###########
```

Luego necesita saber que otras lineas faltan. Para eso revisa las celdas de arriba y abajo de cada posicion pintada.

Si pinto una linea asi:

```text
#*********#
```

entonces revisa:

```text
Fila superior:
#?????????#

Linea pintada:
#*********#

Fila inferior:
#?????????#
```

Cada celda vacia encontrada arriba o abajo se agrega como nueva semilla para procesar otra linea horizontal.

En el codigo se revisa primero arriba y luego abajo:

```text
1. Revisar celda superior.
2. Revisar celda inferior.
```

Esas nuevas semillas se guardan en una cola. Por eso, si encuentra varias lineas pendientes, las procesa en el orden en que fueron encontradas.

Proceso general:

```text
1. Tomar la semilla.
2. Buscar el limite izquierdo del tramo.
3. Buscar el limite derecho del tramo.
4. Pintar toda la linea horizontal.
5. Buscar nuevas semillas arriba y abajo.
6. Repetir hasta que no queden tramos pendientes.
```

Ejemplo:

```text
Antes:

########
#      #
#   S  #
#      #
########

Despues de una linea:

########
#      #
#******#
#      #
########
```

Ventajas:

```text
Es mas eficiente que pintar celda por celda.
Rellena tramos completos.
Se ve ordenado por filas.
```

Figuras usadas:

```text
Pentagono
Hexagono
```

En el proyecto:

```text
Carpeta: RELLENO/RellenoScanline
Controlador: RellenoScanlineController
Formulario: FrmRellenoScanline
```

## 4. Comparacion general

### Lineas

| Algoritmo | Tipo de calculo | Ventaja principal |
|---|---|---|
| DDA | Incremental con decimales | Facil de entender |
| Bresenham | Enteros con error | Muy eficiente |
| Punto Medio Linea | Decision por punto medio | Base para rasterizacion |

### Circulos

| Algoritmo | Tipo de calculo | Ventaja principal |
|---|---|---|
| Punto Medio Circulo | Decision incremental | Usa simetria y enteros |
| Parametrico | Ecuaciones con coseno y seno | Facil de relacionar con trigonometria |
| Polar | Radio y angulo | Intuitivo como barrido circular |

### Rellenos

| Algoritmo | Estructura | Forma de avance |
|---|---|---|
| Relleno por Semilla | Cola | Por capas |
| Relleno por Pila | Pila | Por profundidad |
| Relleno Scanline | Tramos horizontales | Por filas |

## 5. Idea final

Los algoritmos de lineas convierten una recta matematica en puntos discretos.

Los algoritmos de circulo convierten una circunferencia matematica en puntos discretos.

Los algoritmos de relleno toman una figura cerrada y pintan su interior.

Todos son algoritmos importantes en computacion grafica porque permiten pasar de formulas geometricas a pixeles o celdas visibles en pantalla.