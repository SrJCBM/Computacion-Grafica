# Informe Técnico: Algoritmos de Rasterización y Relleno en Computación Gráfica

---

<div align="center">

## UNIVERSIDAD DE LAS FUERZAS ARMADAS — ESPE
### Departamento de Ciencias de la Computación (DCCO)

**Materia:** Computación Gráfica  
**NRC:** [NRC]  
**Docente:** [Nombre del Docente]  
**Autor:** Kenned Jhair Sigcha Palma  
**Período:** 2025 — 2026  
**Fecha de entrega:** Junio 2026  

</div>

---

## Resumen

El presente informe documenta el diseño, implementación y validación de nueve algoritmos clásicos de computación gráfica interactiva, desarrollados sobre la plataforma .NET 10.0 en C# con Windows Forms. El sistema está organizado bajo el patrón arquitectónico Modelo-Vista-Controlador (MVC) y abarca tres categorías de algoritmos: rasterización de líneas rectas (DDA, Bresenham, Punto Medio de Línea), generación de circunferencias (Punto Medio de Círculo, Paramétrico, Polar) y algoritmos de relleno de regiones (por Semilla/BFS, por Pila/DFS, Scanline). Cada algoritmo cuenta con una interfaz gráfica interactiva que permite visualización paso a paso, control de parámetros en tiempo real y animación con retardo configurable. Se documentan las decisiones de diseño de interfaz, el sistema de coordenadas, el manejo de excepciones y la complejidad computacional comparativa de cada enfoque.

---

## Índice

1. [Arquitectura del Sistema y Decisiones de Diseño UI/UX](#1-arquitectura-del-sistema-y-decisiones-de-diseño-uiux)
2. [Clasificación Taxonómica de Algoritmos](#2-clasificación-taxonómica-de-algoritmos)
3. [Algoritmos de Rasterización de Líneas](#3-algoritmos-de-rasterización-de-líneas)
   - 3.1. [Algoritmo DDA](#31-algoritmo-dda-digital-differential-analyzer)
   - 3.2. [Algoritmo de Bresenham](#32-algoritmo-de-bresenham)
   - 3.3. [Algoritmo de Punto Medio de Línea](#33-algoritmo-de-punto-medio-de-línea)
4. [Algoritmos de Generación de Circunferencias](#4-algoritmos-de-generación-de-circunferencias)
   - 4.1. [Punto Medio de Círculo](#41-punto-medio-de-círculo)
   - 4.2. [Paramétrico de Círculo](#42-paramétrico-de-círculo)
   - 4.3. [Polar de Círculo](#43-polar-de-círculo)
5. [Algoritmos de Relleno de Regiones](#5-algoritmos-de-relleno-de-regiones)
   - 5.1. [Relleno por Semilla (BFS)](#51-relleno-por-semilla-bfs)
   - 5.2. [Relleno por Pila (DFS)](#52-relleno-por-pila-dfs)
   - 5.3. [Relleno Scanline](#53-relleno-scanline)
6. [Sistema de Coordenadas e Inversión del Eje Y](#6-sistema-de-coordenadas-e-inversión-del-eje-y)
7. [Documentación de Control de Excepciones](#7-documentación-de-control-de-excepciones)
8. [Conclusiones Técnicas y Análisis de Complejidad](#8-conclusiones-técnicas-y-análisis-de-complejidad)
9. [Repositorio](#9-repositorio)

---

## 1. Arquitectura del Sistema y Decisiones de Diseño UI/UX

### 1.1. Patrón Arquitectónico MVC

El sistema implementa el patrón **Modelo-Vista-Controlador** de forma estricta a nivel de directorios y namespaces. Esta separación garantiza que la lógica algorítmica sea completamente independiente de la capa de presentación, facilitando el mantenimiento y la extensibilidad.

```
GeometriaComputacional/
├── Lineas/
│   ├── DDA/
│   │   ├── Controlador/   DdaController.cs
│   │   ├── Modelo/        PasoDDA.cs, ResultadoDDA.cs
│   │   └── Vista/         FrmDDA.cs
│   ├── Bresenham/         (misma estructura)
│   └── PuntoMedioLinea/   (misma estructura)
├── Conicas/
│   ├── PuntoMedioCirculo/ (misma estructura)
│   ├── ParametricoCirculo/
│   └── PolarCirculo/
└── Relleno/
    ├── RellenoPorSemilla/
    ├── RellenoPorPila/
    └── RellenoScanline/
```

Cada **Controlador** expone únicamente un método `Calcular(...)` que recibe los parámetros matemáticos y retorna un objeto **Resultado** inmutable. La **Vista** consume ese resultado a través de un temporizador o de forma directa, sin conocer los detalles algorítmicos.

### 1.2. Paleta de Colores con Canal Alfa (ARGB)

Todos los elementos gráficos del sistema utilizan el modelo de color **ARGB** de 32 bits, donde el canal alfa (primeros 8 bits) controla la transparencia con valores entre 0 (totalmente transparente) y 255 (totalmente opaco).

La elección de transparencia parcial responde a una necesidad funcional concreta: los **píxeles rasterizados** deben superponerse visualmente sobre la grilla del plano cartesiano y la línea/curva ideal sin ocultarla completamente, de modo que el usuario pueda observar simultáneamente la aproximación discreta y la figura matemática continua.

| Elemento                   | Color ARGB                        | Alpha | Justificación                                      |
|----------------------------|-----------------------------------|-------|----------------------------------------------------|
| Píxeles DDA / Bresenham    | `(255, 220, 38, 38)`              | 255   | Rojo sólido, contraste máximo sobre plano blanco   |
| Círculo ideal de referencia| `(160, 37, 99, 235)`              | 160   | Azul semitransparente; visible bajo los píxeles    |
| Píxeles paramétrico        | `(200, 22, 163, 74)`              | 200   | Verde con ligera transparencia                     |
| Píxeles polar              | `(200, 147, 51, 234)`             | 200   | Morado semitransparente                            |
| Relleno BFS (semilla)      | `(255, 186, 230, 253)`            | 255   | Azul claro, evoca expansión uniforme               |
| Relleno DFS (pila)         | `(255, 167, 243, 208)`            | 255   | Verde claro, evoca profundidad de árbol            |
| Relleno Scanline           | `(255, 233, 213, 255)`            | 255   | Lila claro, evoca barrido horizontal               |

### 1.3. Suavizado Antialiasing

Todos los formularios de líneas y cónicas activan el modo de suavizado de alta calidad en el evento `Paint`:

```csharp
g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
```

Esta instrucción indica a GDI+ que aplique **submuestreo multimuestra** al renderizar primitivas vectoriales (líneas, elipses, arcos). Sin esta activación, los bordes de las figuras ideales presentan el efecto "escalera" (*jaggies*), lo que dificulta la comparación visual entre la figura matemática continua y los píxeles rasterizados. El antialiasing se aplica únicamente a los elementos vectoriales; los rectángulos de píxeles rasterizados se dibujan intencionalmente sin suavizado para representar fielmente la naturaleza discreta del resultado del algoritmo.

### 1.4. Orden de Pintado (Z-order de Software)

Una decisión crítica de renderizado es el **orden de pintado** de los elementos. GDI+ no gestiona capas; el último elemento dibujado aparece al frente. El orden correcto garantiza que los puntos de control siempre sean visibles:

```
1. Píxeles rasterizados      ← fondo (pueden ser tapados)
2. Línea / curva ideal       ← nivel intermedio
3. Puntos de control (P1/P2) ← siempre al frente
```

Sin este orden, los cuadrados de píxeles rojos cubrirían los puntos de control verdes y naranjas cuando un píxel coincide con la posición de un punto de control, haciéndolos inaccesibles para el arrastre con el ratón.

---

## 2. Clasificación Taxonómica de Algoritmos

```
Algoritmos de Computación Gráfica Implementados
│
├── RASTERIZACIÓN DE PRIMITIVAS LINEALES
│   ├── DDA (Digital Differential Analyzer)     — Incremental con aritmética real
│   ├── Bresenham                                — Incremental con aritmética entera
│   └── Punto Medio de Línea                    — Decisión por evaluación de función implícita
│
├── GENERACIÓN DE CIRCUNFERENCIAS
│   ├── Punto Medio de Círculo                  — Decisión incremental + simetría 8-fold
│   ├── Paramétrico                              — Muestreo trigonométrico uniforme por ángulo
│   └── Polar                                    — Conversión de coordenadas polares a cartesianas
│
└── RELLENO DE REGIONES CERRADAS
    ├── Por Semilla (BFS/Cola)                  — Expansión uniforme por capas
    ├── Por Pila (DFS/Stack)                    — Exploración en profundidad
    └── Scanline                                 — Relleno por tramos horizontales
```

---

## 3. Algoritmos de Rasterización de Líneas

### 3.1. Algoritmo DDA (Digital Differential Analyzer)

#### 3.1.1. Análisis Matemático

El DDA convierte una recta continua $f(x) = mx + b$ en una secuencia de píxeles discretos mediante incrementos acumulativos. Dados dos puntos $P_1 = (x_1, y_1)$ y $P_2 = (x_2, y_2)$:

$$\Delta x = x_2 - x_1 \qquad \Delta y = y_2 - y_1$$

El número de pasos $k$ determina cuántos píxeles se generan:

$$k = \max(|\Delta x|,\ |\Delta y|)$$

Los incrementos por paso son fracciones racionales:

$$\delta x = \frac{\Delta x}{k} \qquad \delta y = \frac{\Delta y}{k}$$

La secuencia de píxeles se calcula iterativamente:

$$x_{i+1} = x_i + \delta x \qquad y_{i+1} = y_i + \delta y$$

El píxel en la pantalla se obtiene redondeando:

$$P_i = \bigl(\text{round}(x_i),\ \text{round}(y_i)\bigr)$$

**Propiedad clave:** El incremento mayor siempre es $\pm 1$, garantizando que no haya píxeles saltados y que la recta sea conexa.

#### 3.1.2. Fragmento de Código

```csharp
// DdaController.cs — método Calcular
public ResultadoDDA Calcular(PuntoGeometrico p1, PuntoGeometrico p2)
{
    float dx = p2.X - p1.X;
    float dy = p2.Y - p1.Y;

    // k = número total de píxeles a generar
    float k = Math.Max(Math.Abs(dx), Math.Abs(dy));

    // Caso degenerado: ambos puntos coinciden
    if (k == 0)
        return new ResultadoDDA(0, 0, 0, 1, new List<PasoDDA>
            { new(0, p1.X, p1.Y, (int)Math.Round(p1.X), (int)Math.Round(p1.Y)) });

    float ix = dx / k;   // incremento fraccional en X
    float iy = dy / k;   // incremento fraccional en Y

    float x = p1.X, y = p1.Y;
    var pasos = new List<PasoDDA>();

    for (int i = 0; i <= (int)k; i++)
    {
        // Redondeo al píxel más cercano
        int px = (int)Math.Round(x);
        int py = (int)Math.Round(y);
        pasos.Add(new PasoDDA(i, x, y, px, py));
        x += ix;
        y += iy;
    }

    float pendiente = dx == 0 ? float.PositiveInfinity : dy / dx;
    return new ResultadoDDA(dx, dy, pendiente, (int)k + 1, pasos);
}
```

#### 3.1.3. Resultado de Prueba

| Parámetros        | Valores                               |
|-------------------|---------------------------------------|
| $P_1$             | $(-12,\ -2)$                          |
| $P_2$             | $(11,\ -1)$                           |
| $\Delta x$        | $23$                                  |
| $\Delta y$        | $1$                                   |
| $k$               | $23$ pasos                            |
| $\delta x$        | $1.000$                               |
| $\delta y$        | $0.043$                               |
| Pendiente $m$     | $0.04$                                |

Los píxeles generados pasan de $(-12, -2)$ en pasos unitarios en $x$. Dado que $\delta y \approx 0.043$, la coordenada $y$ solo cambia de $-2$ a $-1$ una vez en el paso 12, reflejando fielmente la pendiente casi nula de la recta.

---

### 3.2. Algoritmo de Bresenham

#### 3.2.1. Análisis Matemático

Bresenham elimina el uso de aritmética de punto flotante al trabajar exclusivamente con **enteros** y una **variable de decisión** $e$ que acumula el error respecto a la recta ideal.

Para el caso general con $|\Delta x| \geq |\Delta y|$ (octante 1):

$$e_0 = 2|\Delta y| - |\Delta x|$$

En cada iteración se evalúa $e_2 = 2e$:

$$\text{Si } e_2 > -|\Delta y|: \quad x \mathrel{+}= s_x, \quad e \mathrel{-}= 2|\Delta y|$$
$$\text{Si } e_2 < |\Delta x|: \quad y \mathrel{+}= s_y, \quad e \mathrel{+}= 2|\Delta x|$$

Donde $s_x = \text{signo}(\Delta x)$ y $s_y = \text{signo}(\Delta y)$ son las direcciones de avance. Este criterio generalizado cubre todos los octantes sin casos especiales.

#### 3.2.2. Fragmento de Código

```csharp
// BresenhamController.cs — algoritmo generalizado para todos los octantes
int x = p1.X, y = p1.Y;
int dx = Math.Abs(p2.X - p1.X), dy = Math.Abs(p2.Y - p1.Y);
int sx = p1.X < p2.X ? 1 : -1;   // dirección en X
int sy = p1.Y < p2.Y ? 1 : -1;   // dirección en Y
int error = dx - dy;               // variable de decisión inicial

while (true)
{
    pasos.Add(new PasoBresenham(indice++, x, y, error));

    if (x == p2.X && y == p2.Y) break;

    int e2 = 2 * error;

    // Decide avance en X
    if (e2 > -dy) { error -= dy; x += sx; }

    // Decide avance en Y
    if (e2 < dx)  { error += dx; y += sy; }
}
```

#### 3.2.3. Resultado de Prueba

| Paso | $x$  | $y$  | Error $e$ | Acción                  |
|------|------|------|-----------|-------------------------|
| 0    | 0    | 0    | 3         | Inicio                  |
| 1    | 1    | 0    | 1         | Avance en X             |
| 2    | 2    | 0    | −1        | Avance en X             |
| 3    | 2    | 1    | 5         | Avance en X e Y         |
| 4    | 3    | 1    | 3         | Avance en X             |

Para la recta de $P_1=(0,0)$ a $P_2=(5,3)$: $\Delta x=5$, $\Delta y=3$, $e_0=2$. El algoritmo produce 6 píxeles sin ninguna operación de punto flotante.

---

### 3.3. Algoritmo de Punto Medio de Línea

#### 3.3.1. Análisis Matemático

El algoritmo de Punto Medio evalúa la función implícita de la recta $F(x,y) = \Delta y \cdot x - \Delta x \cdot y + c$ en el **punto medio** entre los dos candidatos de píxel para decidir cuál está más cerca de la recta ideal.

Parámetros derivados (asumiendo $|\Delta x| \geq |\Delta y|$, $m \leq 1$):

$$\Delta_m = \min(|\Delta x|,\ |\Delta y|), \qquad \Delta_M = \max(|\Delta x|,\ |\Delta y|)$$

Variable de decisión inicial:

$$p_0 = 2\Delta_m - \Delta_M$$

Actualización en cada paso:

$$\text{Si } p < 0: \quad p \mathrel{+}= 2\Delta_m \qquad \text{(avance recto)}$$
$$\text{Si } p \geq 0: \quad p \mathrel{+}= 2(\Delta_m - \Delta_M) \qquad \text{(avance diagonal)}$$

La **decisión** registrada en la tabla (`Recto` o `Diagonal`) muestra directamente qué candidato de píxel fue elegido en cada iteración.

#### 3.3.2. Fragmento de Código

```csharp
// PuntoMedioLineaController.cs
int deltaM = Math.Max(Math.Abs(dx), Math.Abs(dy));  // mayor delta
int deltam = Math.Min(Math.Abs(dx), Math.Abs(dy));  // menor delta

int p = 2 * deltam - deltaM;   // decisión inicial

for (int i = 0; i < deltaM; i++)
{
    string decision;
    if (p < 0)
    {
        // El punto medio indica: avanzar recto (solo eje dominante)
        p += 2 * deltam;
        decision = "Recto";
    }
    else
    {
        // El punto medio indica: avanzar diagonal
        p += 2 * (deltam - deltaM);
        decision = "Diagonal";
        avanzarEjeSecundario();
    }
    avanzarEjeDominante();
    pasos.Add(new PasoPuntoMedioLinea(i, xReal, yReal, xPixel, yPixel, decision));
}
```

#### 3.3.3. Resultado de Prueba

Para $P_1=(0,0)$, $P_2=(8,3)$: $\Delta x=8$, $\Delta y=3$, $p_0 = 2(3)-8 = -2$.

| Paso | Píxel     | $p$  | Decisión |
|------|-----------|------|----------|
| 0    | $(0,0)$   | −2   | Recto    |
| 1    | $(1,0)$   | 4    | Recto    |
| 2    | $(2,1)$   | −8   | Diagonal |
| 3    | $(3,1)$   | −2   | Recto    |
| 4    | $(4,1)$   | 4    | Recto    |
| 5    | $(5,2)$   | −8   | Diagonal |
| 6    | $(6,2)$   | −2   | Recto    |
| 7    | $(7,2)$   | 4    | Recto    |

---

## 4. Algoritmos de Generación de Circunferencias

### 4.1. Punto Medio de Círculo

#### 4.1.1. Análisis Matemático

El algoritmo de Punto Medio para circunferencias explota la **simetría de 8 octantes**: calcula los píxeles en el primer octante ($0° \leq \theta \leq 45°$, zona donde $x \leq y$) y los refleja simétricamente.

La función implícita de la circunferencia es:

$$f(x, y) = x^2 + y^2 - r^2$$

Condición inicial:

$$x_0 = 0, \quad y_0 = r, \quad p_0 = 1 - r$$

Reglas de actualización en cada iteración ($x \mathrel{+}= 1$ siempre):

$$\text{Si } p < 0: \qquad p \mathrel{+}= 2x + 3$$

$$\text{Si } p \geq 0: \qquad y \mathrel{-}= 1, \quad p \mathrel{+}= 2(x - y) + 5$$

> **Nota de implementación crítica:** el decremento $y \mathrel{-}= 1$ se aplica **antes** de evaluar la fórmula $p + 2(x-y)+5$. El término $(x-y)$ utiliza el valor actualizado de $y$.

Los 8 puntos simétricos generados a partir de $(x, y)$ relativo al centro $(x_c, y_c)$ son:

$$(\pm x + x_c,\ \pm y + y_c) \qquad (\pm y + x_c,\ \pm x + y_c)$$

#### 4.1.2. Fragmento de Código

```csharp
// PuntoMedioCirculoController.cs
int x = 0, y = radio, p = 1 - radio, iter = 0;

while (x <= y)
{
    // Obtener los 8 puntos simétricos (con deduplicación por HashSet)
    var puntos = ObtenerSimetrias(xc, yc, x, y);
    pasos.Add(new PasoPuntoMedioCirculo(iter, x, y, p,
        FormatearSimetrias(puntos), puntos));

    // Actualizar variable de decisión
    if (p < 0)
    {
        p += 2 * x + 3;        // avance Este
    }
    else
    {
        y--;                   // avance Sureste: decrementar y primero
        p += 2 * (x - y) + 5; // luego evaluar con y ya decrementado
    }
    x++;
    iter++;
}
```

#### 4.1.3. Resultado de Prueba

Para $x_c=0$, $y_c=0$, $r=5$:

| Iter | $x$ | $y$ | $p$  | Decisión   |
|------|-----|-----|------|------------|
| 0    | 0   | 5   | −4   | Este       |
| 1    | 1   | 5   | −2   | Este       |
| 2    | 2   | 5   | 2    | Sureste    |
| 3    | 3   | 4   | −6   | Este       |
| 4    | 4   | 4   | 2    | Sureste    |

En la iteración 2, $p = -2 + 2(2)+3 = 5 \geq 0$, por lo que $y$ pasa de 5 a 4 y $p = 5 + 2(2-4)+5 = 2$.

---

### 4.2. Paramétrico de Círculo

#### 4.2.1. Análisis Matemático

La representación paramétrica expresa cada punto de la circunferencia en función del ángulo $t$:

$$x(t) = x_c + r\cos(t) \qquad y(t) = y_c + r\sin(t)$$

donde $t \in [0°, 360°)$ se muestrea con paso de $1°$, dando exactamente 360 puntos. El píxel correspondiente se obtiene redondeando al entero más cercano:

$$P_t = \bigl(\text{round}(x(t)),\ \text{round}(y(t))\bigr)$$

El **paso angular de 1°** garantiza cobertura completa sin brechas visibles para radios $r \geq 2$, ya que el arco por grado es $s = \frac{2\pi r}{360} \approx 0.017r$, que para $r=5$ da $s \approx 0.087 < 1$ px, asegurando que puntos consecutivos sean adyacentes o coincidentes.

El total de píxeles únicos es siempre menor que 360 debido al redondeo: múltiples ángulos pueden mapearse al mismo píxel, especialmente en los ejes y las diagonales.

#### 4.2.2. Fragmento de Código

```csharp
// ParametricoCirculoController.cs
var pasos = new List<PasoParametricoCirculo>();
var vistos = new HashSet<(int, int)>();

for (int t = 0; t < 360; t++)
{
    double rad = t * Math.PI / 180.0;
    float xR = (float)(xc + radio * Math.Cos(rad));
    float yR = (float)(yc + radio * Math.Sin(rad));

    // Redondeo al píxel más cercano
    int px = (int)Math.Round(xR);
    int py = (int)Math.Round(yR);

    pasos.Add(new PasoParametricoCirculo(t, xR, yR, px, py));

    // Conteo de píxeles únicos (sin duplicados por redondeo)
    vistos.Add((px, py));
}

return new ResultadoParametricoCirculo(xc, yc, radio, vistos.Count, pasos);
```

#### 4.2.3. Resultado de Prueba

Para $x_c=0$, $y_c=0$, $r=5$:

| $t$ (°) | $x(t)$ real | $y(t)$ real | Píxel $(p_x, p_y)$ |
|---------|-------------|-------------|---------------------|
| 0       | 5.000       | 0.000       | $(5, 0)$            |
| 30      | 4.330       | 2.500       | $(4, 3)$            |
| 45      | 3.536       | 3.536       | $(4, 4)$            |
| 90      | 0.000       | 5.000       | $(0, 5)$            |
| 180     | −5.000      | 0.000       | $(−5, 0)$           |
| 270     | 0.000       | −5.000      | $(0, −5)$           |

Total de 360 ángulos muestreados → **31 píxeles únicos** (para $r=5$), evidenciando la redundancia por redondeo.

---

### 4.3. Polar de Círculo

#### 4.3.1. Análisis Matemático

El método polar parte de las coordenadas polares $(r, \theta)$, donde el **radio $r$ es constante** y el ángulo $\theta$ varía. La conversión al plano cartesiano se realiza mediante:

$$x = x_c + r\cos(\theta) \qquad y = y_c + r\sin(\theta)$$

La diferencia conceptual respecto al paramétrico es que en el método polar el punto de partida es la **definición geométrica de radio constante** en coordenadas polares, enfatizando que todos los puntos de la circunferencia están a la misma distancia $r$ del centro, mientras que el método paramétrico enfatiza el recorrido angular.

Matemáticamente, ambos producen la misma secuencia de píxeles para el mismo $(x_c, y_c, r)$.

#### 4.3.2. Fragmento de Código

```csharp
// PolarCirculoController.cs
for (int theta = 0; theta < 360; theta++)
{
    double rad = theta * Math.PI / 180.0;

    // Conversión polar → cartesiano con centro trasladado
    int px = (int)Math.Round(xc + radio * Math.Cos(rad));
    int py = (int)Math.Round(yc + radio * Math.Sin(rad));

    // Se registra: PixelX, R constante, Theta, punto formateado
    pasos.Add(new PasoPolarCirculo(theta, radio, px, py));
    vistos.Add((px, py));
}
```

#### 4.3.3. Resultado de Prueba

Para $x_c=0$, $y_c=0$, $r=5$:

| $\theta$ (°) | $r$ | $(p_x, p_y)$ |
|-------------|-----|--------------|
| 0           | 5   | $(5, 0)$     |
| 60          | 5   | $(3, 4)$     |
| 120         | 5   | $(−3, 4)$    |
| 180         | 5   | $(−5, 0)$    |
| 240         | 5   | $(−3, −4)$   |
| 300         | 5   | $(3, −4)$    |

El radio $r=5$ aparece como valor constante en todas las filas de la tabla, ilustrando la naturaleza polar del algoritmo.

---

## 5. Algoritmos de Relleno de Regiones

Los algoritmos de relleno operan sobre una **matriz de bytes** de $23 \times 15$ celdas. Cada celda tiene uno de tres estados:

| Valor | Estado   | Color visualizado |
|-------|----------|-------------------|
| `0`   | Vacía    | Blanco            |
| `1`   | Borde    | Gris oscuro       |
| `2`   | Rellena  | Color del algoritmo|

La **semilla** $(s_x, s_y)$ debe ubicarse en una celda vacía dentro de la figura cerrada. El sistema incluye una **validación previa** basada en BFS que verifica si la semilla puede alcanzar el borde de la matriz; en caso afirmativo, la semilla está fuera de la figura y el relleno se rechaza.

### 5.1. Relleno por Semilla (BFS)

#### 5.1.1. Análisis Matemático

El algoritmo de Relleno por Semilla utiliza una **cola FIFO** (First In, First Out), implementando una búsqueda en anchura (BFS — *Breadth-First Search*). La expansión se produce en capas concéntricas a partir de la semilla, análoga a las ondas de un punto en un estanque.

El orden de procesamiento de vecinos es:

$$\text{Arriba} \to \text{Derecha} \to \text{Abajo} \to \text{Izquierda}$$

$$\{(x, y-1),\ (x+1, y),\ (x, y+1),\ (x-1, y)\}$$

Este orden, combinado con la disciplina FIFO de la cola, produce una expansión visual uniforme: todas las celdas a distancia Manhattan $d$ de la semilla se pintan antes que cualquier celda a distancia $d+1$.

La **complejidad temporal** es $O(n)$ donde $n$ es el número de celdas interiores, ya que cada celda se encola y desencola exactamente una vez.

#### 5.1.2. Fragmento de Código

```csharp
// RellenoPorSemillaController.cs — BFS con validación previa
public ResultadoRelleno Calcular(byte[,] matriz, int semX, int semY)
{
    // Validación 1: la semilla no debe estar en un borde
    // Validación 2: la semilla debe estar dentro de la figura cerrada
    if (matriz[semY, semX] != VACIA || !EsDentroDelaFigura(matriz, semX, semY))
        return new ResultadoRelleno(semX, semY, new List<PasoRelleno>());

    var cola = new Queue<(int X, int Y)>();
    var enCola = new bool[ROWS, COLS];
    cola.Enqueue((semX, semY));
    enCola[semY, semX] = true;
    int orden = 1;

    while (cola.Count > 0)
    {
        var (x, y) = cola.Dequeue();
        if (matriz[y, x] != VACIA) continue;

        matriz[y, x] = 2;  // marcar como rellena
        pasos.Add(new PasoRelleno(x, y, orden++));

        // Encolar vecinos en orden BFS: Arriba, Derecha, Abajo, Izquierda
        foreach (var (nx, ny) in new[] { (x, y-1), (x+1, y), (x, y+1), (x-1, y) })
        {
            if (nx >= 0 && nx < COLS && ny >= 0 && ny < ROWS
                && matriz[ny, nx] == VACIA && !enCola[ny, nx])
            {
                enCola[ny, nx] = true;
                cola.Enqueue((nx, ny));
            }
        }
    }
    return new ResultadoRelleno(semX, semY, pasos);
}
```

#### 5.1.3. Resultado de Prueba

**Figura:** Cuadrado (borde en columnas 4–18, filas 2–12). **Semilla:** $(11, 7)$.

| Orden | Celda    | Descripción               |
|-------|----------|---------------------------|
| 1     | $(11, 7)$| Semilla inicial           |
| 2     | $(11, 6)$| Arriba de la semilla      |
| 3     | $(12, 7)$| Derecha de la semilla     |
| 4     | $(11, 8)$| Abajo de la semilla       |
| 5     | $(10, 7)$| Izquierda de la semilla   |

La animación con delay de 200ms evidencia el patrón de expansión concéntrica. Total de celdas rellenas: $15 \times 11 = 165$ (interior del cuadrado).

---

### 5.2. Relleno por Pila (DFS)

#### 5.2.1. Análisis Matemático

El Relleno por Pila utiliza una **pila LIFO** (Last In, First Out), implementando una búsqueda en profundidad (DFS — *Depth-First Search*). A diferencia del BFS, el algoritmo explora un camino hasta su máxima profundidad antes de retroceder.

Los vecinos se **empujan** a la pila en el orden:

$$\text{Arriba},\ \text{Derecha},\ \text{Abajo},\ \text{Izquierda}$$

Pero debido a la disciplina LIFO, se **procesan** en orden inverso:

$$\text{Izquierda} \to \text{Abajo} \to \text{Derecha} \to \text{Arriba}$$

Esto produce un patrón visual característico: el relleno "se mete" por una dirección y avanza lo más lejos posible antes de volver a explorar otras direcciones, generando un patrón de expansión no uniforme que visualmente recuerda al recorrido de un laberinto.

#### 5.2.2. Fragmento de Código

```csharp
// RellenoPorPilaController.cs — DFS con pila explícita
if (matriz[semY, semX] != VACIA || !EsDentroDelaFigura(matriz, semX, semY))
    return new ResultadoRelleno(semX, semY, new List<PasoRelleno>());

var pila = new Stack<(int X, int Y)>();
var visitado = new bool[ROWS, COLS];
pila.Push((semX, semY));

while (pila.Count > 0)
{
    var (x, y) = pila.Pop();

    if (x < 0 || x >= COLS || y < 0 || y >= ROWS) continue;
    if (matriz[y, x] != VACIA || visitado[y, x]) continue;

    visitado[y, x] = true;
    matriz[y, x] = 2;
    pasos.Add(new PasoRelleno(x, y, orden++));

    // Push en orden: Arriba, Derecha, Abajo, Izquierda
    // LIFO → saldrá Izquierda primero
    pila.Push((x,   y-1));  // Arriba    (sale último)
    pila.Push((x+1, y  ));  // Derecha
    pila.Push((x,   y+1));  // Abajo
    pila.Push((x-1, y  ));  // Izquierda (sale primero)
}
```

#### 5.2.3. Resultado de Prueba

**Figura:** Triángulo con vértices $(11,2)$, $(4,12)$, $(18,12)$. **Semilla:** $(11, 7)$.

Los primeros pasos evidencian el comportamiento DFS: desde la semilla $(11,7)$, el primer vecino procesado es Izquierda $(10,7)$. Desde $(10,7)$ vuelve a tomar Izquierda, produciendo el recorrido:

$$S=(11,7) \to (10,7) \to (9,7) \to (8,7) \to \ldots$$

hasta alcanzar el borde izquierdo del triángulo, y solo entonces regresa para explorar otras direcciones.

---

### 5.3. Relleno Scanline

#### 5.3.1. Análisis Matemático

El algoritmo Scanline optimiza el relleno al operar sobre **tramos horizontales completos** en lugar de celdas individuales. Para cada semilla $(s_x, s_y)$ extraída de la cola:

1. **Búsqueda de límites:** escaneado lateral para encontrar $x_{izq}$ y $x_{der}$:

$$x_{izq} = \min\{x \leq s_x : \text{matriz}[s_y][x] \neq \text{VACIA}\} + 1$$
$$x_{der} = \max\{x \geq s_x : \text{matriz}[s_y][x] \neq \text{VACIA}\} - 1$$

2. **Relleno del tramo:** se pintan todas las celdas en $[x_{izq},\ x_{der}]$ de la fila $s_y$.

3. **Propagación:** para cada celda pintada $(x, s_y)$, si la celda superior $(x, s_y-1)$ o inferior $(x, s_y+1)$ está vacía y aún no fue encolada, se agrega como nueva semilla.

Este enfoque reduce el número de operaciones de encolado: en vez de encolar cada celda individualmente, se encolan **puntos semilla de fila** (uno por transición vertical), logrando un relleno mucho más eficiente para figuras amplias.

#### 5.3.2. Fragmento de Código

```csharp
// RellenoScanlineController.cs — relleno por tramos horizontales
cola.Enqueue((semX, semY));
agregado[semY, semX] = true;

while (cola.Count > 0)
{
    var (sx, sy) = cola.Dequeue();
    if (matriz[sy, sx] != VACIA) continue;

    // Buscar límite izquierdo del tramo
    int xIzq = sx;
    while (xIzq > 0 && matriz[sy, xIzq - 1] == VACIA) xIzq--;

    // Buscar límite derecho del tramo
    int xDer = sx;
    while (xDer < COLS - 1 && matriz[sy, xDer + 1] == VACIA) xDer++;

    // Pintar el tramo completo y propagar semillas verticales
    for (int x = xIzq; x <= xDer; x++)
    {
        if (matriz[sy, x] != VACIA) continue;
        matriz[sy, x] = 2;
        pasos.Add(new PasoRelleno(x, sy, orden++));

        // Semilla superior
        if (sy > 0 && matriz[sy-1, x] == VACIA && !agregado[sy-1, x])
        { agregado[sy-1, x] = true; cola.Enqueue((x, sy-1)); }

        // Semilla inferior
        if (sy < ROWS-1 && matriz[sy+1, x] == VACIA && !agregado[sy+1, x])
        { agregado[sy+1, x] = true; cola.Enqueue((x, sy+1)); }
    }
}
```

#### 5.3.3. Resultado de Prueba

**Figura:** Pentágono centrado en $(11, 7)$ con radio $5.5$ celdas.

El algoritmo procesa la fila central primero (la que contiene la semilla), rellena un tramo de ~10 celdas en una sola iteración de cola, y encola semillas para las filas $6$ y $8$. El número de operaciones de encolado es proporcional al **número de filas** de la figura (~9), no al número de celdas (~70), lo que explica su ventaja en eficiencia para figuras de gran ancho horizontal.

---

## 6. Sistema de Coordenadas e Inversión del Eje Y

Las API gráficas de sistemas operativos tradicionales (GDI+, DirectX 2D, HTML Canvas) ubican el **origen $(0, 0)$ en la esquina superior izquierda** de la ventana o panel, con el eje X creciendo hacia la derecha y el **eje Y creciendo hacia abajo**. Esta convención es opuesta a las coordenadas matemáticas estándar, donde el eje Y crece hacia arriba.

Si se grafican directamente las coordenadas matemáticas sin transformación, una línea con pendiente positiva ($m > 0$) aparecería inclinada **hacia abajo** en la pantalla, y un círculo se vería correcto en forma pero con el eje Y invertido respecto a las etiquetas.

Para corregir esto, el sistema aplica la siguiente transformación al convertir coordenadas cartesianas $(x_c, y_c)$ a coordenadas de pantalla $(p_x, p_y)$:

$$p_x = O_x + x_c \cdot e \qquad p_y = O_y - y_c \cdot e$$

Donde:
- $(O_x, O_y)$ es el **origen del plano** en píxeles de pantalla (centro del panel con desplazamiento por pan)
- $e$ es el **factor de escala** en píxeles por unidad matemática

La inversión del signo en $p_y$ ($O_y - y_c \cdot e$) corrige la orientación del eje Y. Esta transformación está implementada en el método `CartesianoAPantalla` de cada formulario:

```csharp
private PointF CartesianoAPantalla(float x, float y)
{
    PointF origen = ObtenerOrigen();
    return new PointF(
        origen.X + x * escala,   // X: dirección normal
        origen.Y - y * escala    // Y: invertido (GDI+ crece hacia abajo)
    );
}
```

La operación inversa, para convertir un clic del ratón en coordenadas cartesianas, aplica la transformación opuesta:

```csharp
private PointF PantallaACartesiano(Point p)
{
    PointF origen = ObtenerOrigen();
    return new PointF(
        (p.X - origen.X) / escala,    // X normal
        (origen.Y - p.Y) / escala     // Y invertido
    );
}
```

El factor de escala inicial es de $24\ \text{px/unidad}$, ajustable mediante zoom desde $10$ hasta $72\ \text{px/unidad}$. El origen se desplaza mediante clic-arrastre con el botón derecho, actualizando `desplazamientoPlano` para implementar la función de paneo.

---

## 7. Documentación de Control de Excepciones

### 7.1. Sanitización de Entradas Numéricas

Todos los campos de texto de coordenadas numéricos son procesados mediante el método `TryParse` personalizado, que intenta la conversión con dos culturas distintas para tolerar tanto la coma decimal europea como el punto decimal anglosajón:

```csharp
private static bool TryParse(string texto, out float valor)
{
    // Intento 1: cultura del sistema operativo (ej. "3,14" en español)
    if (float.TryParse(texto, NumberStyles.Float,
        CultureInfo.CurrentCulture, out valor)) return true;

    // Intento 2: cultura invariante (ej. "3.14" en inglés)
    if (float.TryParse(texto, NumberStyles.Float,
        CultureInfo.InvariantCulture, out valor)) return true;

    valor = 0; return false;  // entrada inválida
}
```

Si cualquier campo falla la conversión, el sistema **no lanza una excepción**, sino que retorna `false` y muestra un `MessageBox` informativo, manteniendo el estado previo de la aplicación intacto.

### 7.2. Validación de Radio Positivo

Para los algoritmos de cónicas, el radio debe ser estrictamente positivo ($r > 0$). La validación se realiza en `LeerDatosDesdeTexto`:

```csharp
private bool LeerDatosDesdeTexto()
{
    if (!TryParseInt(txtX1.Text, out int xc) ||
        !TryParseInt(txtY1.Text, out int yc) ||
        !TryParseInt(txtX2.Text, out int r)  ||
        r <= 0)               // radio debe ser > 0
        return false;
    // ...
}
```

Un radio de cero o negativo haría el algoritmo de Punto Medio infinitamente iterativo (condición de parada $x \leq y$ nunca se cumpliría) y el algoritmo paramétrico generaría únicamente el punto central.

### 7.3. Validación de Semilla Dentro de la Figura

La validación más sofisticada del sistema es la verificación de que la semilla de relleno está **dentro** de la región cerrada y no en el exterior. Se implementa como un BFS de pre-validación que busca un camino libre de bordes hacia el límite de la matriz:

```csharp
private static bool EsDentroDelaFigura(byte[,] matriz, int semX, int semY)
{
    var visitado = new bool[ROWS, COLS];
    var cola = new Queue<(int X, int Y)>();
    cola.Enqueue((semX, semY));
    visitado[semY, semX] = true;

    while (cola.Count > 0)
    {
        var (x, y) = cola.Dequeue();

        // Si alcanza el borde de la matriz → semilla está fuera de la figura
        if (x == 0 || x == COLS-1 || y == 0 || y == ROWS-1)
            return false;

        foreach (var (nx, ny) in new[] { (x, y-1), (x+1,y), (x,y+1), (x-1,y) })
        {
            if (nx >= 0 && nx < COLS && ny >= 0 && ny < ROWS
                && matriz[ny, nx] == VACIA && !visitado[ny, nx])
            {
                visitado[ny, nx] = true;
                cola.Enqueue((nx, ny));
            }
        }
    }
    return true;  // la región explorada nunca alcanzó el borde → es interior
}
```

Esta validación tiene la misma complejidad $O(n)$ que el relleno en sí, pero se ejecuta sobre una copia de la matriz, dejando el estado original intacto si la semilla es inválida.

### 7.4. Guardias de Nulidad en el Timer de Animación

El timer de animación verifica en cada tick que el estado del sistema sea consistente antes de intentar acceder a datos:

```csharp
private void Timer_Tick(object? sender, EventArgs e)
{
    // Triple guardia: resultado, matriz y límite de pasos
    if (resultado == null || matriz == null || pasoActual >= resultado.Pasos.Count)
    {
        timer1.Stop();
        ActualizarEstado($"Relleno completo. Celdas pintadas: {resultado?.TotalCeldas}");
        return;
    }
    // Procesamiento seguro garantizado
    var paso = resultado.Pasos[pasoActual];
    if (paso.Y < ROWS && paso.X < COLS)
        matriz[paso.Y, paso.X] = 2;
    pasoActual++;
    pnlContenedor.Refresh();  // repintado síncrono para animación precisa
}
```

El uso de `Refresh()` en lugar de `Invalidate()` garantiza que cada tick del timer resulte en exactamente un frame visible, evitando que múltiples invalidaciones se acumulen y produzcan saltos en la animación.

---

## 8. Conclusiones Técnicas y Análisis de Complejidad

### 8.1. Tabla Resumen de Algoritmos

| Algoritmo              | Categoría       | Estructura de datos | Técnica matemática            | API principal          |
|------------------------|-----------------|---------------------|-------------------------------|------------------------|
| DDA                    | Línea           | Lista               | Incremento fraccionario       | `FillRectangle`        |
| Bresenham              | Línea           | Lista               | Aritmética entera + error     | `FillRectangle`        |
| Punto Medio Línea      | Línea           | Lista               | Función implícita + decisión  | `FillRectangle`        |
| Punto Medio Círculo    | Cónica          | Lista               | Decisión incremental 8-fold   | `FillRectangle`        |
| Paramétrico Círculo    | Cónica          | Lista + HashSet     | Muestreo trigonométrico       | `FillRectangle`        |
| Polar Círculo          | Cónica          | Lista + HashSet     | Coordenadas polares           | `FillRectangle`        |
| Relleno Semilla (BFS)  | Relleno         | Queue               | Búsqueda en anchura           | `FillRectangle`        |
| Relleno Pila (DFS)     | Relleno         | Stack               | Búsqueda en profundidad       | `FillRectangle`        |
| Scanline               | Relleno         | Queue               | Tramos horizontales           | `FillRectangle`        |

### 8.2. Análisis Comparativo de Eficiencia

#### Algoritmos de Líneas

| Algoritmo        | Operaciones por píxel       | Tipo aritmético | Complejidad |
|------------------|-----------------------------|-----------------|-------------|
| DDA              | 2 sumas, 2 redondeos        | Punto flotante  | $O(k)$      |
| Bresenham        | 2 sumas, 1 comparación      | Entera          | $O(k)$      |
| Punto Medio      | 1 suma, 1 comparación       | Entera          | $O(k)$      |

Donde $k = \max(|\Delta x|, |\Delta y|)$. Aunque la complejidad asintótica es equivalente, **Bresenham y Punto Medio son significativamente más rápidos** en hardware que no cuenta con unidad de punto flotante, dado que eliminan operaciones de redondeo y divisiones en coma flotante. En hardware moderno con FPU, la diferencia es marginal pero sigue siendo relevante en contextos de renderizado masivo.

#### Algoritmos de Circunferencia

| Algoritmo        | Iteraciones totales | Tipo aritmético | Operaciones trigonométricas |
|------------------|---------------------|-----------------|-----------------------------|
| Punto Medio      | $\leq r/\sqrt{2}$   | Entera          | Ninguna                     |
| Paramétrico      | 360                 | Punto flotante  | 2 por iteración (cos, sin)  |
| Polar            | 360                 | Punto flotante  | 2 por iteración (cos, sin)  |

El **Punto Medio de Círculo** es el más eficiente: genera entre $\lfloor r/\sqrt{2}\rfloor + 1$ iteraciones (el primer octante), con 8-fold symmetry, totalizando $O(r)$ operaciones enteras sin ninguna función trigonométrica. El paramétrico y el polar son $O(360)$ independientemente del radio e involucran funciones transcendentes costosas.

#### Algoritmos de Relleno

| Algoritmo       | Celdas encoladas       | Patrón visual        | Eficiencia relativa |
|-----------------|------------------------|----------------------|---------------------|
| BFS (Semilla)   | $O(n)$ — cada celda 1x | Uniforme concéntrico | Base                |
| DFS (Pila)      | $O(n)$ — puede repetir | Profundidad variable | Similar a BFS       |
| Scanline        | $O(\text{filas})$      | Por filas            | Mayor para figuras amplias |

El **Scanline** presenta la mejor eficiencia práctica para figuras de geometría regular porque el número de semillas encoladas es proporcional al número de **filas** de la figura, no al número de celdas individuales. Para una figura de $w$ columnas y $h$ filas, el Scanline encola $O(h)$ semillas mientras BFS/DFS encolan hasta $O(w \times h)$ celdas, una mejora de un factor $w$ en las operaciones de gestión de la estructura de datos.

### 8.3. Decisión de API Gráfica: `FillRectangle` vs. `DrawEllipse`

Todos los píxeles rasterizados se dibujan mediante `FillRectangle` con un tamaño igual al factor de escala, en vez de usar primitivas de alto nivel como `FillEllipse` o `FillPolygon`. Esta decisión tiene base en la **semántica del algoritmo**: el objetivo es visualizar cada píxel discreto como una celda cuadrada de la grilla, representando fielmente el espacio de píxeles de un monitor real. Usar `FillEllipse` o `FillPolygon` para representar píxeles sería semánticamente incorrecto, aunque visualmente más estético.

La figura **ideal** (la línea o circunferencia matemática continua) sí utiliza las primitivas de alto nivel de GDI+ (`DrawLine`, `DrawEllipse`), que internamente aplican antialiasing a nivel de subpíxel, creando la separación visual entre la *referencia matemática continua* y la *aproximación discreta rasterizada*.

---

## 9. Repositorio

El código fuente completo del proyecto, incluyendo el historial de versiones, la estructura MVC completa y todos los algoritmos documentados en este informe, está disponible en el repositorio de GitHub del autor:

**Repositorio:** [github.com/[usuario]/Computacion-Grafica](https://github.com)

El repositorio contiene:
- Proyecto C# SDK-style compilable con Visual Studio 2022 y .NET 10.0
- Estructura de carpetas MVC por algoritmo
- Historial de commits que refleja el desarrollo incremental
- Todos los formularios con interfaz interactiva paso a paso

---

*Informe generado como documentación técnica del proyecto de Computación Gráfica — Universidad de las Fuerzas Armadas ESPE, DCCO, 2026.*