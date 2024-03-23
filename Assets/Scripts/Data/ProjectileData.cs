using UnityEngine;

// ScriptableObjects são uma ótima maneira de armazenar dados que são usados em vários lugares.
// Fazendo isso, você pode evitar a duplicação de dados e garantir que todos os objetos que usam
// esses dados estejam sempre atualizados.
[CreateAssetMenu(fileName = "ProjectileData", menuName = "Data/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public float speed;
    public float despawnTime;
    public float pushForce;
}