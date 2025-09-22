using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    [Header("Scripts Refs")]
    public PlayerMovement playerMovement;
    public PlayerSight playerSight;



    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerSight = GetComponent<PlayerSight>();
    }








}
