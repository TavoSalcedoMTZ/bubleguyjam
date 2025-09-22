using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputHandler inputHandler;

    [SerializeField] private string inputHandlerType;

    private void Start()
    {
        switch (inputHandlerType)
        {
            case "Main":
                inputHandler = new InputHandlerMainScene();
                break;

            default:
                Debug.LogError("Tipo de InputHandler no válido");
                return;
        }

        inputHandler.Initialize();
    }

    private void Update()
    {
        inputHandler?.HandleInput();
    }
}