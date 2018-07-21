using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrucatable : MonoBehaviour
{
    [SerializeField]
    private GameObject _crateDestroyed;

    public void DestroyCrate()
    {
        Instantiate(_crateDestroyed, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
