using System;

using Main;

using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private ComponentsController _componentsController;
    private Animator targetAnimator;
    private const float normalizedTransitionDuration = 0f;

    private void Awake()
    {
        _componentsController.ThenPeakedComponent += SetAnimationState;
    }

    private void Start()
    {
        targetAnimator = GetComponent<Animator>();
    }

    private void SetAnimationState(string stateName)
    {
        targetAnimator.CrossFade(stateName,normalizedTransitionDuration);
    }

    private void OnDestroy()
    {
        _componentsController.ThenPeakedComponent -= SetAnimationState;
    }
}