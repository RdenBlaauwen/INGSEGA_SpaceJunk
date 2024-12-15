using UnityEngine;
using System.Linq;
using UnityEditor;

public class Enemy : MonoBehaviour
{

    public Texture2D texture;
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public float speed = 0;
    public GameObject explosion;

    private Vector3 playerPos;
    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        string spriteSheet = AssetDatabase.GetAssetPath(texture);
        sprites = AssetDatabase.LoadAllAssetsAtPath(spriteSheet).OfType<Sprite>().ToArray();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            return;
        }


        playerPos = player.transform.position;

    }

    void FixedUpdate()
    {

        rb.MovePosition(Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime));
        
        Vector3 direction = playerPos - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y);
        rb.MoveRotation(-(angle * Mathf.Rad2Deg));
    }

    public void Explode() {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
