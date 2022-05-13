using UnityEngine;

namespace PinPong
{
    public class EBall : Ball
    {
        PlayerBlock[] plBlocks;
        int moveDir = 0;

        public override void ActivateBall(BallData ballData)
        {
            base.ActivateBall(ballData);

            plBlocks = FindObjectsOfType<PlayerBlock>();
        }

        public override void PushBall(Vector3 reflectVector, Vector3 addForce)
        {
            base.PushBall(reflectVector, addForce);
        }

        private void Update()
        {
            for (int i = 0; i < plBlocks.Length; i++)
            {
                if (Vector3.Distance(_ballRB.position, plBlocks[i].transform.position) < 3)
                {
                    if (moveDir == 0)
                    {
                        float rand = Random.Range(-2.0f, 2.0f);
                        Vector3 dir = Vector3.right * rand;
                        _ballRB.AddForce(dir, ForceMode.Impulse);
                        moveDir = (int)rand;
                    }
                    else
                    {
                        Vector3 dir = Vector3.right * moveDir;
                        _ballRB.AddForce(dir, ForceMode.Impulse);
                    }
                }
            }
        }
    }
}
