using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatItem : MonoBehaviour
{
    // 拾ったら太るアイテムのスクリプト 太らせてくる敵から30％の確率でドロップする
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl_Nat>().hp += 10;
            Destroy(gameObject);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerControl_Nat>().hp += 50;
            Destroy(gameObject);
        }
    }

    // プレイヤーのモデルにColliderとCharacterControlerの二種類のComponentがついており、どっちで判定取っているのか確認する時間が無かったので、
    // 苦肉の策でどっちで反応してもいいような書き方してます。ごめんなさい
}
