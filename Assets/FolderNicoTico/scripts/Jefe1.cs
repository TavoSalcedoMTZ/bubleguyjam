using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Jefe1 : MonoBehaviour
{
    public Transform target;
    public Enemigo enemigoScript;
    public GameObject prefabToSpawn;
    public CircleCollider2D spawnArea;

    [SerializeField] private float moveSpeedInicial;
    [SerializeField] private float moveSpeedMax;
    [SerializeField] private float tiempoAtaque;
    [SerializeField] private float tiempoGenerarPrefab;
    [SerializeField] private float probabilidadGenerarPrefab;
    [SerializeField] private float stopDistanceThreshold;

    [SerializeField] private MovimientoJefe1 movimiento;
    [SerializeField] private AtaqueJefe1 ataque;
    [SerializeField] private GeneradorPrefabsJefe1 generadorPrefabs;

    private void Start()
    {
        movimiento = GetComponent<MovimientoJefe1>();
        ataque = GetComponent<AtaqueJefe1>();
        generadorPrefabs = GetComponent<GeneradorPrefabsJefe1>();

        ataque.Inicializar(enemigoScript, tiempoAtaque);
        generadorPrefabs.Inicializar(prefabToSpawn, spawnArea, tiempoGenerarPrefab, probabilidadGenerarPrefab);
    }
}