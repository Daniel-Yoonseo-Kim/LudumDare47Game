using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_Grab : MonoBehaviour
{
	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;
	public Enemy enemy;

	public GameObject throwManager = null;

	public Transform throwPoint;


    public void Grab()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
		{
			enemy.anim.GetComponent<Enemy>().isGrabbed = true;
            enemy.anim.SetTrigger("throw");
        } else {
			enemy.anim.GetComponent<Enemy>().isGrabbed = false;
            enemy.anim.SetTrigger("miss");
        }
	}

	public void ThrowPlayer() {
		Instantiate(throwManager, throwPoint.position, Quaternion.identity);
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}

