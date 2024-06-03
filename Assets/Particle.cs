using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    // 消滅するまでの時間
    private float lifeTime;
    // 消滅するまでの残り時間
    private float liftLifeTime;
    // 移動量
    private Vector3 velocity;
    // 初期Scale
    private Vector3 defaultScale;

    //public static class Random
    //{
    //    public static float Range(float minInclusive, float maxInclusive);
    //}
    //float minInclusive = 0.1f;
    //float maxInclusive = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // 消滅するまでの時間を0.3秒とする
        lifeTime = 0.3f;
        // 残り時間を初期化
        liftLifeTime = lifeTime;
        // 現在のScsleを記録
        defaultScale = transform.localScale;
        // ランダムで決まる移動量の最大値
        float maxVelocity = 5.0f;
        // 各方向へランダムに飛ばす
        velocity = new Vector3
            (Random.Range(-maxVelocity, maxVelocity),
             Random.Range(-maxVelocity, maxVelocity),
             0
            );
        // 
        liftLifeTime += Time.deltaTime;
        // 
        transform.position += velocity * Time.deltaTime;
        // 
        transform.localScale = Vector3.Lerp
            (new Vector3(0, 0, 0),
             defaultScale,
             liftLifeTime / lifeTime
            );
        // 
        if (liftLifeTime <= 0) { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
