using CPCComplex.MonoBehaviours;
using Photon.Pun;
using System;
using System.Collections;
using UnboundLib;
using UnityEngine;
using UnityEngine.Events;

public class EndermenTeleport : MonoBehaviour
{
    public float damageNeeded = 25f;

    public float cd = 0.2f;

    public bool allowSelfDamage;

    public float time;

    private float damageDealt;

    public ParticleSystem[] parts;

    public ParticleSystem[] remainParts;

    public float distance = 10f;

    public LayerMask mask;

    private CharacterData data;

    private AttackLevel level;

    public UnityEvent triggerEvent;

    private void Start()
    {
        parts = GetComponentsInChildren<ParticleSystem>();
        data = GetComponentInParent<CharacterData>();
        level = GetComponentInParent<AttackLevel>();
    }
    public bool CanTrigger(float damage, bool selfDamage)
    {
        if (!selfDamage || allowSelfDamage)
        {
            if (damage > damageNeeded && Time.time > time + cd)
            {
                return true;
            }
        }
        return false;
    }

    public bool Trigger(float damage, bool selfDamage, Vector3 bulletDir)
    {
        if (!selfDamage || allowSelfDamage)
        {
            damageDealt += damage;
            if (damageDealt > damageNeeded && Time.time > time + cd)
            {
                time = Time.time;
                damageDealt = 0f;
                triggerEvent.Invoke();
                StartCoroutine(DelayMove(base.transform.position, bulletDir));
                return true;
            }
        }
        return false;
    }
    private IEnumerator DelayMove(Vector3 beforePos, Vector3 bulletDir)
    {
        Vector3 position = base.transform.position;
        Vector3 position2 = base.transform.position;
        int num = 10;
        float num2 = distance * (float)level.attackLevel / (float)num;
        for (int i = 0; i < num; i++)
        {
            //position += num2 * data.aimDirection;
            position += num2 * bulletDir.normalized;
            if (!Physics2D.OverlapCircle(position, 0.5f))
            {
                position2 = position;
            }
        }
        for (int j = 0; j < remainParts.Length; j++)
        {
            remainParts[j].transform.position = base.transform.root.position;
            remainParts[j].Play();
        }
        GetComponentInParent<PlayerCollision>().IgnoreWallForFrames(2);

        //GetComponentInParent<HealthHandler>().Heal(damage.magnitude);

        base.transform.root.position = position2;
        for (int k = 0; k < parts.Length; k++)
        {
            parts[k].transform.position = position2;
            parts[k].Play();
        }
        yield break;
    }
}
