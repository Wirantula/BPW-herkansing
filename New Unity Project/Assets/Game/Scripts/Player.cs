using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private AudioSource _weaponAudio;

    [SerializeField]
    private int currentAmmo;
    private int maxAmmo = 128;
    private bool _isReloading = false;

    private UIManager _uiManager;

    //public variable for coin
    public bool CoinPickedup = false;

    [SerializeField]
    private GameObject _Weapon;

	// Use this for initialization
	void Start ()
    {

        _controller = GetComponent<CharacterController>();
        //hide cursor
        Cursor.lockState = CursorLockMode.Locked;

        currentAmmo = maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {

        //if left click cast ray from center point of main camera
        if (Input.GetMouseButton(0) && currentAmmo > 0 && _Weapon.activeInHierarchy == true)
        {
            Shoot();
        }
        else
        {
            _bullet.SetActive(false);
            _weaponAudio.Stop();
        }

        if (Input.GetKeyDown(KeyCode.R) && _isReloading == false)
        {
            _isReloading = true;
            StartCoroutine(ReloadRoutine());
        }


        //if escape pressed unhide mouse cursor
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        CalculateMovement();
	}

    IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _isReloading = false;
    }

    void Shoot()
    {
        _bullet.SetActive(true);
        currentAmmo--;
        _uiManager.UpdateAmmo(currentAmmo);
        if (_weaponAudio.isPlaying == false)
        {
            _weaponAudio.Play();
        }
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;


        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            Destroy(hitMarker, 1f);

            if (hitInfo.collider.tag == "Destructable")
            {
                _uiManager.UpdateScore();
                Destroy(hitInfo.collider.gameObject);
            }

            //check if we hit the crate
            Destrucatable crate = hitInfo.transform.GetComponent<Destrucatable>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
            //destroy crate
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        //making local space to world space
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    public void EnableWeapons()
    {
        _Weapon.SetActive(true);
    }
    
}
