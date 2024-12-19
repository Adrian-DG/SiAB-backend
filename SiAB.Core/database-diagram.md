```mermaid

---
title: Order example
---

erDiagram

    Articulos ||--o{ Depositos : has
    Articulos ||--o{ Modelos : has
    Articulos ||--o{ Categorias : has
    Articulos ||--o{ Tipos : has
    Articulos ||--o{ SubTipos : has
    Articulos ||--o{ Marcas : has
    Articulos ||--o{ Modelos : has
    Articulos ||--o{ Calibres : has

    RegistrosCargDescargo ||--o{ Articulos : has

```
