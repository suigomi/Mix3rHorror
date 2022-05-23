using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
	/// <summary>
	/// パーティクルが他のGameObject(Collider)に当たると呼び出される
	/// </summary>
	/// <param name="other"></param>
	private void OnParticleCollision(GameObject other)
	{
		// 当たった相手の色をランダムに変える
		// other.gameObject.GetComponent<Renderer>().material.color = Random.ColorHSV();
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other);
        }
	}
}