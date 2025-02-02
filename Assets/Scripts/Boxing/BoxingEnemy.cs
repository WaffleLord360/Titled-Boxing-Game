using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxingEnemy : MonoBehaviour
{
    public FrameInput EnemyFrameInput { get; private set; } = new FrameInput();
    public UnityEvent<FrameInput> OnFrameInput;

    
    [Range(1, 100)] [SerializeField] float difficulty;
    [SerializeField] float distance;
    [SerializeField] Transform target;
    [SerializeField] Transform orientation;

    void Update()
    {
        EnemyFrameInput.SetInput(
            orientation.forward,
            Vector2.one,
            false,
            false,
            false,
            -1,
            false,
            -1,
            -1);

        float random = Random.Range(0, 100);
        float distToTarget = Vector3.Distance(orientation.position, target.position);

        float multi = Mathf.Clamp(distToTarget - distance, -1f, 1f);
        Vector3 movDir = orientation.forward * multi + (orientation.right * Random.Range(-1, 1));

        float doesSomething = Random.Range(0, 100);
        if (doesSomething >= difficulty)
        {
            EnemyFrameInput.SetInput(
                movDir,
                Vector2.one,
                false,
                false,
                false,
                -1,
                false,
                -1,
                -1);

            OnFrameInput?.Invoke(EnemyFrameInput);
            orientation.LookAt(target);
            return;
        }

        if (random > 50)
        {
            if (distToTarget < distance + 1f)
            {
                //Left Punch
                EnemyFrameInput.SetInput(
                movDir,
                Vector2.one,
                false,
                false,
                false,
                -1,
                false,
                0,
                -1);
            } 
        }
        else if (random > 4f)
        {
            if (distToTarget < distance + 1f)
            {
                //Right Punch
                EnemyFrameInput.SetInput(
                movDir,
                Vector2.one,
                false,
                false,
                false,
                -1,
                false,
                1,
                -1);
            } 
        }
        else if (random > 0.5f)
        {
            //Roll
            EnemyFrameInput.SetInput(
            movDir,
            Vector2.one,
            false,
            false,
            true,
            -1,
            false,
            -1,
            -1);
        }
        else if (random > 0.005f)
        {
            //Slip Left
            EnemyFrameInput.SetInput(
            movDir,
            Vector2.one,
            false,
            false,
            false,
            0,
            false,
            -1,
            -1);
        }
        else
        {
            //Slip Right
            EnemyFrameInput.SetInput(
            movDir,
            Vector2.one,
            false,
            false,
            false,
            1,
            false,
            -1,
            -1);
        }

        OnFrameInput?.Invoke(EnemyFrameInput);
        orientation.LookAt(target);
    }
}
