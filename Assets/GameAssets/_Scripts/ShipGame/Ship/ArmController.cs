using UnityEngine;

public class ArmController: MonoBehaviour
{
    public Transform spriteMovil;  // Referencia al sprite que se mueve
    public Transform spriteEstatico;  // Primer sprite estático
    public LineRenderer lineRenderer;

    void Start()
    {
        // Referencia al LineRenderer que está en este objeto
        lineRenderer = GetComponent<LineRenderer>();
        // Asegúrate de que la línea tiene solo 2 puntos (1 línea)
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        
        lineRenderer.SetPosition(0, spriteEstatico.position);

    
        lineRenderer.SetPosition(1, spriteMovil.position);

      
    }
}