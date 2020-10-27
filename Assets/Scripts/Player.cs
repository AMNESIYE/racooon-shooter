using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _explosionAnimationPrefab;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private float _speed = 8f;
    [SerializeField] public float _fireRate = 0.2f;
    
    public int _live = 3;
    
    private float _canFire = 0.0f;

    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool canShield = false;
    public bool canShieldFollow = false;

	// Use this for initialization
	private void Start ()
    {
        transform.position = new Vector3(0, -4.0f, 0);
    }
	
	// Update is called once per frame
	private void Update ()
	{
	    Movement();

	    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
	    {
	        Shoot();
	    }

	    if (canShieldFollow)
	    {
	        ShieldFollowPlayer();
	    }
	}

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            if (canTripleShot)
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            else
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (canSpeedBoost)
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * 1.5f * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * 1.5f * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        

        if (transform.position.x > 9.4f)
        {
            transform.position = new Vector3(-9.4f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.4f)
        {
            transform.position = new Vector3(9.4f, transform.position.y, 0);
        }

        if (transform.position.y > 4.3f)
        {
            transform.position = new Vector3(transform.position.x, 4.3f, 0);
        }
        else if (transform.position.y < -4.3f)
        {
            transform.position = new Vector3(transform.position.x, -4.3f, 0);
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostPowerupOn()
    {
        canSpeedBoost = true;
        Instantiate(_shieldPrefab, transform.position, Quaternion.identity);
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void ShieldPowerupOn()
    {
        canShield = true;
        StartCoroutine(ShieldPowerupRoutine());
    }

    public void ShieldFollowPlayer()
    {
        _shieldPrefab.transform.Translate(this.transform.position);
    }

    private IEnumerator ShieldPowerupRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        canShield = false;
        canShieldFollow = false;
    }

    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        canSpeedBoost = false;
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void Damaged()
    {
        if (!canShield)
        {
            _live--;
        
            if (_live < 1)
            {
                Instantiate(_explosionAnimationPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else
        {
            canShield = false;
        }
    }
}
