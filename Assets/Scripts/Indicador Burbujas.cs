using UnityEngine;
using UnityEngine.UI;

public class IndicadorJabon : MonoBehaviour
{
    public Slider slider;  // El slider que muestra el indicador de jab�n
    public float jabonMaximo = 100f;  // Valor m�ximo de jab�n
    public float jabonActual;  // Valor actual de jab�n
    public float velocidadDeDecrecimiento = 10f;  // Velocidad con la que disminuye el jab�n

    public GameObject panelActual;  // Referencia al panel actual (por ejemplo, el de las burbujas)
    public GameObject panelNuevo;   // Referencia al nuevo panel que se activar� cuando se acaben las burbujas

    void Start()
    {
        jabonActual = jabonMaximo;
        slider.maxValue = jabonMaximo;
        slider.value = jabonActual;

        // Aseg�rate de que el panel actual est� activo y el nuevo panel est� desactivado al inicio
        panelActual.SetActive(true);
        panelNuevo.SetActive(false);
    }

    void Update()
    {
        jabonActual -= velocidadDeDecrecimiento * Time.deltaTime;

        if (jabonActual < 0f)
            jabonActual = 0f;

        slider.value = jabonActual;

        // Si el valor del jab�n llega a cero, ocultar el panel actual y mostrar el nuevo panel
        if (jabonActual == 0f)
        {
            panelActual.SetActive(false);  // Desactivar el panel actual
            panelNuevo.SetActive(true);    // Activar el nuevo panel
        }
    }
}
