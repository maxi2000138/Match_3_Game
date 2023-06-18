using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.StateMachine.CoroutineRunner;
using UnityEngine;
using Zenject;

namespace GameTile
{
    public class TileMover : MonoBehaviour
    {
        private ICoroutineRunner _coroutineRunner;
        private Dictionary<GameTile, MoveRoutine> _moveTweens = new();
        private float _maxTileCount;
        private float _width;
        private double _eps = 1f;
    
        [SerializeField] private GameTileView _tileView;
        [SerializeField] private float _flyDuration = 0.1f;
        [SerializeField] private Transform _anchorTransform ;

        public bool IsAnyoneMoved =>
            _moveTweens.Count > 0;


        public float FlyDuration =>
            _flyDuration;

        [Inject]
        public void Construct(LooseWinCheacker looseWinCheacker, ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _maxTileCount = looseWinCheacker.MaxTileCount;
        }

        private void Start()
        {
            _width = _tileView.GetComponent<RectTransform>().sizeDelta.x;
        }

        public void MoveAll(LinkedList<GameTile> tiles)
        {
            int counter = 0;
        
            if(tiles.Count > _maxTileCount)
                return;
            
            foreach (GameTile tile in tiles)
            {
                Vector2 pos = _anchorTransform.position;
                pos.x += (_width * counter++) + _width/2;
            
                if(Vector2.Distance(pos,tile.TileView.transform.position) < _eps)
                    continue;
            
                if (_moveTweens.ContainsKey(tile))
                {
                    _moveTweens[tile].ChangeTargetPosition(pos);
                }
                else
                {
                    MoveRoutine mover = new MoveRoutine(_coroutineRunner);
                    _moveTweens.Add(tile, mover);
                    mover.StartMoving(tile.TileView, pos, _flyDuration, () =>
                    {
                        _moveTweens.Remove(tile);
                    });
                }
            }
        }
    
        private class MoveRoutine
        {
            private ICoroutineRunner _coroutineRunner;
            private Vector2 _targetPosition;
            private Coroutine _routine;
    
            public MoveRoutine(ICoroutineRunner coroutineRunner)
            {
                _coroutineRunner = coroutineRunner;
            }

            public void StartMoving(GameTileView tile, Vector2 targetPosition,float flyDuration, Action OnComplete = null) => 
                _routine = _coroutineRunner.StartCoroutine(DOMove(tile, targetPosition,flyDuration, OnComplete));

            public void StopMoving()
            {
                if(_routine != null)
                    _coroutineRunner.StopCoroutine(_routine);
            }

            public void ChangeTargetPosition(Vector2 targetPosition) => 
                _targetPosition = targetPosition;

            private IEnumerator DOMove(GameTileView tile, Vector2 targetPosition, float flyDuration,  Action OnComplete = null)
            {
                _targetPosition = targetPosition;
                Vector2 startPosition = tile.transform.position;
                float delta = 0f;

                while (delta <= 1f)
                {
                    tile.transform.position = Vector2.Lerp(startPosition, _targetPosition, delta * delta);
                    delta += Time.deltaTime * 1.0f/flyDuration;
                    yield return null;
                }
            
                if(tile != null)
                    tile.transform.position = _targetPosition;
            
                OnComplete?.Invoke();
            }
        }
    }
}

