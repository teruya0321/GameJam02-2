using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript_nos : MonoBehaviour
{
    public int atk;
    // 攻撃力 生成される際に別のスクリプトから代入される
    float timer;
    // 時間経過で消すためのタイマー

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Ground")
        {
            Destroy();
            // 壁が地面に着弾したら消すように
        }
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl_Nat>().hp += atk * 2;
            Destroy();
            // プレイヤーに着弾したら、プレイヤーの体重を攻撃力分増やして消す
            // ゲームバランス的に攻撃力を二倍に。必要なら後ろの*2を消してもよい
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            hit.gameObject.GetComponent<PlayerControl_Nat>().hp += atk * 2;
            Destroy();
            // 上のOncollisionEnterで書いた内容と同じです
            // 同じ内容を二度書いてる理由は、プレイヤーのモデルにColliderとCharacterControlerの二種類のComponentがついており、どっちで判定取っているのか確認する時間が無かったので、
            // 苦肉の策でどっちで反応してもいいような書き方してます。ごめんなさい
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5)
        {
            Destroy();
            // 一定時間経ったら弾を消すように
        }
    }

    private void Destroy() // 弾を消す関数 一行しか書いてないので正直ない方がよかったかもしれない
    {
        Destroy(gameObject);
    }
}
