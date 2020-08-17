using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyronesiaFireBall : MonoBehaviour
{
    public float speed = 1f;
    public LayerMask characterHit;
    public float radius = 0.25f;
    public Pyronesia pyronesia;

    private void Update()
    {
        PlayerHitCheck(characterHit);
        transform.Translate(-1f * transform.right * speed * Time.deltaTime);
        Destroy(gameObject, 2.5f);
    }

    // checks to see if colliding with a player
    private void PlayerHitCheck(LayerMask character)
    {
        Collider[] surfaces = Physics.OverlapSphere(transform.position, radius, character);

        if (surfaces.Length > 0)
        {
            pyronesia.TriggerDamage(pyronesia.enemy.gameObject, 0.4f, 0);
            GameObject hitEffect = Instantiate(pyronesia.hitEffects[0], pyronesia.enemy.target.transform);
            Destroy(hitEffect, 1.0f);
            Destroy(gameObject);
        }
    }
}
