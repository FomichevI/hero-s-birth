using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _showDelay = 12; //Задержка между подсказками
    [SerializeField] private float _showDuration = 2; //Продолжительность подсказки
    [SerializeField] private float _upperPlayer = 15; //Отступ от игрока вверх

    private float _currentTime;
    private Vector2 _pos;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _currentTime = 0;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _pos.x = _player.position.x;
        _pos.y = _player.position.y + _upperPlayer;
        gameObject.transform.position = _pos;

        if (_currentTime % _showDelay < _showDuration)
        {
            _spriteRenderer.color = Color.white;
        }
        else
        {
            _spriteRenderer.color = Color.clear;
        }

        if (_currentTime == float.MaxValue)
            _currentTime = 0;
        _currentTime += 0.02f;
    }
}
