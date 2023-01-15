using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canAttack = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit: " + collision.name);
        IDamagable hit = collision.GetComponent<IDamagable>();
        if(hit != null && _canAttack)
        {
            hit.Damage();
            _canAttack = false;
            StartCoroutine("ResetAttack");
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
        _canAttack = true;
    }
}
