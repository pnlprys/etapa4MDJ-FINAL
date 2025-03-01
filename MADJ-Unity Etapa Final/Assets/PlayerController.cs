using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 7f;
    private bool noChao;

    private Rigidbody2D rb;
    private Animator anim;

    private bool andando;
    private bool idle;
    private bool atacando;
    private bool pulando;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float movimento = Input.GetAxisRaw("Horizontal"); // Captura o movimento horizontal
        bool apertouPulo = Input.GetKeyDown(KeyCode.Space); // Verifica se apertou espaço para pular
        bool apertouAtaque = Input.GetMouseButtonDown(0); // Verifica se clicou com o mouse para atacar

        // Definição dos estados
        andando = movimento != 0;
        idle = !andando && !atacando && !pulando;
        atacando = apertouAtaque;
        pulando = !noChao;

        // Movimentação
        rb.velocity = new Vector2(movimento * velocidade, rb.velocity.y);

        // Pulo
        if (apertouPulo && noChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, forcaPulo);
            noChao = false;
        }

        // Atualiza animações no Animator
        anim.SetBool("andando", andando);
        anim.SetBool("idle", idle);
        anim.SetBool("atacando", atacando);
        anim.SetBool("pulando", pulando);

        // Vira o personagem na direção que está andando
        if (movimento > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (movimento < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta quando toca o chão
        if (collision.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }
}