using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _anim;

    private AudioSource _audioSource;
    private BoxCollider2D _boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("The player is NULL");
        }
        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.LogError("The animator is NULL");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the enemy is NULL");
        }
        _boxCollider2D = GetComponent<BoxCollider2D>();
        if(_boxCollider2D == null)
        {
            Debug.LogError("BoxCollider2D on the enemy is NULL");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        } 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _boxCollider2D.enabled = false;
            Destroy(this.gameObject, 2.8f);
            _audioSource.Play();
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _boxCollider2D.enabled = false;
            Destroy(this.gameObject, 2.8f);
            _audioSource.Play();
        }
    }



}
