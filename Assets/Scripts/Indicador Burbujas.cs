using UnityEngine;
using UnityEngine.UI;

public class IndicadorJabon : MonoBehaviour
{
    public Slider slider;  // El slider que muestra el indicador de jabón
    public float jabonMaximo = 100f;  // Valor máximo de jabón
    public float jabonActual;  // Valor actual de jabón
    public float velocidadDeDecrecimiento = 10f;  // Velocidad con la que disminuye el jabón

    public GameObject panelActual;  // Referencia al panel actual (por ejemplo, el de las burbujas)
    public GameObject panelNuevo;   // Referencia al nuevo panel que se activará cuando se acaben las burbujas

    void Start()
    {
        jabonActual = jabonMaximo;
        slider.maxValue = jabonMaximo;
        slider.value = jabonActual;

        // Asegúrate de que el panel actual esté activo y el nuevo panel esté desactivado al inicio
        panelActual.SetActive(true);
        panelNuevo.SetActive(false);
    }

    void Update()
    {
        jabonActual -= velocidadDeDecrecimiento * Time.deltaTime;

        if (jabonActual < 0f)
            jabonActual = 0f;

        slider.value = jabonActual;

        // Si el valor del jabón llega a cero, ocultar el panel actual y mostrar el nuevo panel
        if (jabonActual == 0f)
        {
            panelActual.SetActive(false);  // Desactivar el panel actual
            panelNuevo.SetActive(true);    // Activar el nuevo panel
        }
    }
}
