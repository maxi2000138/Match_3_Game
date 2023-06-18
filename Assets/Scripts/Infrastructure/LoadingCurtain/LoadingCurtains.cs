using System.Collections;
using Infrastructure.StateMachine.CoroutineRunner;
using UnityEngine;
using Zenject;

namespace Infrastructure.LoadingCurtain
{
    public class LoadingCurtains : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private ICoroutineRunner _coroutineRunner;
        private Coroutine _routine;

        [Inject]
        public void Inject(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Show()
        {
            if (_routine != null)
                _coroutineRunner.StopCoroutine(_routine);
            
            _canvasGroup.alpha = 1;
            gameObject.SetActive(true);
        }

        public void Fade() => 
            _routine = _coroutineRunner.StartCoroutine(Fade(1.6f));

        private IEnumerator Fade(float FadeTime)
        {
            float time = 0;
            float alpha;
            while (time <= FadeTime)
            {
                alpha = 1 - (time / FadeTime);
                _canvasGroup.alpha = alpha;
                time += Time.deltaTime;
                yield return null;
            }
            
            gameObject.SetActive(false);
        }

    }
}
