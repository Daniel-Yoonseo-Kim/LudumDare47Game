using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Attack : MonoBehaviour
{
	public int attackDamage = 20;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;

	public void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;

		Collider2D[] hits = Physics2D.OverlapCircleAll(
			gameObject.transform.position + transform.right * attackOffset.x + transform.up * attackOffset.y, attackRange, attackMask);
		foreach (Collider2D enemy in hits) {
			GameObject enemyObj = enemy.gameObject;
			enemyObj.GetComponent<PlayerStatus>().TakeDamage(attackDamage);
		}
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
