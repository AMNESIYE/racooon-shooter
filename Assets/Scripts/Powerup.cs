using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _powerupID;

	// Update is called once per frame
	void Update ()
    {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (_powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if (_powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                }
                else if (_powerupID == 2)
                {
                    player.ShieldPowerupOn();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
