using UnityEngine;

[CreateAssetMenu(fileName = "Ball", menuName = "PinPong/Ball", order = 1)]
public class BallData : ScriptableObject
{
    public string ballName;

    [Range(0.1f, 2.0f)] public float size;
    [Range(5.0f, 20.0f)] public float moveSpeed;
    [Range(0.1f, 3.0f)] public float reboundSpeed;

}