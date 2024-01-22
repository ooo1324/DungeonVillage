using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int monsterCount = 0;
    int reserveCount = 0;

    [SerializeField]
    int keepMonsterCount = 0;

    [SerializeField]
    Vector3 spawnPos;

    public Vector3 SpawnPos { get { return spawnPos; } set {  spawnPos = value; } }

    [SerializeField]
    float spawnRadius = 30f;

    [SerializeField]
    float spawnTime = 5f;

    public void AddMonsterCount(int value) { monsterCount += value; }

    public void SetKeepMonsterCount(int count) { keepMonsterCount = count; }

    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        // ������ ī��Ʈ + ���� ���� ī��Ʈ
        while (reserveCount + monsterCount < keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, spawnTime));
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Skeleton");
        NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;
        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, spawnRadius);
            randDir.y = 0;
            randPos = spawnPos + randDir;

            //�� �� �ִ��� üũ
            NavMeshPath path = new NavMeshPath();
            if (nma.CalculatePath(randPos, path))
                break;
        }

        obj.transform.position = randPos;
        reserveCount--;
    }
}
