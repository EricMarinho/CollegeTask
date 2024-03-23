using UnityEngine;

// ScriptableObjects são uma ótima maneira de armazenar dados que são usados em vários lugares.
// Fazendo isso, você pode evitar a duplicação de dados e garantir que todos os objetos que usam
// esses dados estejam sempre atualizados.
[CreateAssetMenu(fileName = "NPCData", menuName = "Data/NPCData")]
public class NPCData : ScriptableObject
{
    public float speed;
    public MinMax stoppingDistance;
    public MinMax idleTime;
    public Vector2 direction;
}