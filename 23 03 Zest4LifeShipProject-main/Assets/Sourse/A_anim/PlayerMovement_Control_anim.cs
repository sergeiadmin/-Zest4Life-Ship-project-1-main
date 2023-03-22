using UnityEngine;

public class PlayerMovement_Control_anim : MonoBehaviour
{
    public GameObject objectToShow;  // объект, который нужно показать/скрыть
    public bool isWalking;  // флаг, указывающий, движетс€ ли игрок

    private Animator anim;  // компонент аниматора
    private Vector3 lastPosition;  // последн€€ позици€ игрока

    void Start()
    {
        anim = GetComponent<Animator>();  // получаем компонент аниматора у игрока
        lastPosition = transform.position;  // запоминаем текущую позицию игрока
    }

    void Update()
    {
        // провер€ем, двигаетс€ ли игрок
        if (transform.position != lastPosition)
        {
            isWalking = true;
            anim.SetBool("Walk", true);  // включаем анимацию ходьбы

            if (objectToShow != null)
            {
                // если объект задан, показываем его
                objectToShow.SetActive(true);
            }
        }
        else
        {
            isWalking = false;
            anim.SetBool("Walk", false);  // включаем анимацию просто€

            if (objectToShow != null)
            {
                // если объект задан, скрываем его
                objectToShow.SetActive(false);
            }
        }

        lastPosition = transform.position;  // запоминаем текущую позицию игрока дл€ сравнени€ в следующем кадре
    }
}
