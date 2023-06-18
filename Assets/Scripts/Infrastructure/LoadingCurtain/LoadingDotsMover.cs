using System.Collections;
using TMPro;
using UnityEngine;

namespace Infrastructure.LoadingCurtain
{
    public class LoadingDotsMover : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _dotsDeltaSeconds;
        private void Awake()
        {
            StartCoroutine(LoadingDotsChanger());
        }

        IEnumerator LoadingDotsChanger()
        {
            while (true)
            {
                _text.text = "Loading.";
                yield return new WaitForSeconds(_dotsDeltaSeconds);
                _text.text = "Loading..";
                yield return new WaitForSeconds(_dotsDeltaSeconds);
                _text.text = "Loading...";
                yield return new WaitForSeconds(_dotsDeltaSeconds);
            }
        }
    }
}
