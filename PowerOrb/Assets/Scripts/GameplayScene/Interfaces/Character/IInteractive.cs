using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractive{

    void CollidedWithEnemy(Collider2D collision);
    void CollidedWithEnemyAttack(Collider2D collision);
    void CollidedWithOrb(Collider2D collision);
    void CollidedWithPlayer(Collider2D collision);
    void CollidedWithInvisible(Collider2D collision);
}
