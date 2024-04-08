**Propuesta de Solución para el Sistema de Conectividad Hotelera**

Esta solución ha sido diseñada para ofrecer una plataforma robusta y eficiente. Al implementar una arquitectura hexagonal, he logrado una separación clara entre la lógica del dominio y los detalles de la infraestructura, lo que garantiza flexibilidad y mantenibilidad a largo plazo. Estas son algunas características principales del sistema:

1.  **Arquitectura Desacoplada**: Utilizando principios de arquitectura hexagonal, el sistema facilita la adición o sustitución de componentes sin perturbar la lógica central del negocio. Esto asegura una escalabilidad y facilita la integración con una amplia gama de proveedores de servicios.
    
2.  **Validación Robusta**: Incorporé atributos de validación personalizados que garantizan la integridad de los datos en todos los puntos de entrada. Esto incluye la verificación de códigos de moneda reales conforme a la norma ISO, así como validaciones de rango y lógica de negocio para fechas y cantidades.
    
3.  **Eficiencia en el Mapeo de Datos**: He diseñado un sistema de mapeo, en un principio con AutoMapper, pero pensando en el cosumo de memoria y la escalabilidad me decidí por usar un mapeo directo, este mapeo permite transformar y agrupar eficientemente las respuestas de diversos proveedores, priorizando siempre las tarifas más económicas para características idénticas. Esto maximiza el valor para los usuarios finales ya que todos queremos ahorrar y la principal politica de nuestra empresa es dar el precio más competitivo.
    
4.  **Manejo de Errores y Resiliencia**: Nuestra implementación de patrones de manejo de errores asegura que las fallas en la conexión con un proveedor no comprometan la operatividad general del sistema. Esto significa que nuestro sistema continúa funcionando de manera óptima, incluso frente a problemas con servicios externos.
