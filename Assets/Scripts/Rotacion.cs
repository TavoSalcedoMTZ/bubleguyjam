using UnityEngine;

public class Rotacion : MonoBehaviour
{
    // M�todo que rota el objeto hacia el objetivo
    public void RotarHaciaObjetivo(Vector2 direccion)
    {
        // Calculamos el �ngulo entre el objeto y el objetivo
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        // Aplicamos la rotaci�n al objeto para que apunte hacia el objetivo
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }
}
