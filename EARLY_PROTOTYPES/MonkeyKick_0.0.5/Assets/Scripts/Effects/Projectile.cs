using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1f;
    public LayerMask characterHit;
    public float radius = 0.35f;

    private void Update()
    {
        PlayerHitCheck(characterHit);
        transform.Translate(-1f * transform.right * speed * Time.deltaTime);
        Destroy(gameObject, 3f);
    }

    // checks to see if colliding with a player
    private void PlayerHitCheck(LayerMask character)
    {
        Collider[] surfaces = Physics.OverlapSphere(transform.position, radius, character);

        if (surfaces.Length > 0)
        {
            Destroy(gameObject);
        }
    }
}
