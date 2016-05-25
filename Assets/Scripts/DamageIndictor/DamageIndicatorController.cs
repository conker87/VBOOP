using UnityEngine;
using System.Collections;

public class DamageIndicatorController : MonoBehaviour {

	public DamageIndicator DamageIndicatorPrefab;
	TextMesh damageTextMesh;

	public static void ShowDamageAtEnemy( Projectile p, EnemyBase e ) {

		DamageIndicator dmg = Instantiate(DamageIndicatorPrefab, e.transform.position, e.transform.rotation) as DamageIndicator;

		damageTextMesh = dmg.GetComponent<TextMesh> ();

		damageTextMesh.text = p.GetCurrentDamage ().ToString();



	}

}
