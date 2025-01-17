using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    //KINEMATIC RIGIDBODY или вообще rigidbody должен стоять чтоб при соприкосновении работал триггер
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // Проверяем, столкнулся ли объект с триггером.
        // Можно добавить дополнительные условия, если необходимо.
        // Например, проверить тег объекта или другие параметры.
        if (other.CompareTag("Player")) // Замените "YourTag" на тег объектов, которые вы хотите уничтожить.
        {
            // Уничтожаем объект, попавший в триггер.
            Destroy(other.gameObject);
        }
    }
}
