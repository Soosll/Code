using System.Collections;
using UnityEngine;

namespace Infrastructure.General
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}