using UnityEngine;

public class Rotacion : MonoBehaviour
{
    // Método que rota el objeto hacia el objetivo
    public void RotarHaciaObjetivo(Vector2 direccion)
    {
        // Calculamos el ángulo entre el objeto y el objetivo
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        // Aplicamos la rotación al objeto para que apunte hacia el objetivo
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }
}
