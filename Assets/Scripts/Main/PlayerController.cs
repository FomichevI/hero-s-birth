using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _rotationSpeed = 2.5f;
    [SerializeField] private SkillsController _skillsController;
    [SerializeField] private GameObject _effect; //Объект со Sprite Renderer

    private GameManager _gameManager;
    private Rigidbody2D _rb;
    private Transform _headTransform;
    private bool _arrowControl; //Переменная для определения типа управления
    private Vector2 _movement; //Направление движения
    private float _rotationZ;
    private float _originalMoveSpeed;    
    private Animator _animator;

    private bool[] _checkPoints;
    private int _lastCheckpoint;
    private bool _isMoving = false;    

    private bool _underEffect;
    private float _effectDuration;
    private string _currentSkillTag = null;
        
    void Start()
    {
        _gameManager = Camera.main.GetComponent<GameManager>();

        _rb = GetComponent<Rigidbody2D>();
        _headTransform = transform.Find("Head").GetComponent<Transform>();
        _originalMoveSpeed = _moveSpeed;
        _checkPoints = new bool[3];        

        string parentName = transform.name; //Получаем родительское имя

        _animator = GetComponentInChildren<Animator>();
        if (parentName == "Player1") //Если это игрок1 то управление не на стрелках        
            _arrowControl = false;        
        else        
            _arrowControl = true;
            
        _rotationZ = transform.rotation.eulerAngles.z;
        _lastCheckpoint = _checkPoints.Length - 1; // В начале игры выставляем индикатор последнего чекпоинта             
    }

    private void Update()
    {
        if (!_arrowControl)
        {
            if (Input.GetAxisRaw("HorizontalP1") != 0 || Input.GetAxisRaw("VerticalP1") != 0)
                _isMoving = true;
            else
                _isMoving = false;
        }
        if (_arrowControl)
        {
            if (Input.GetAxisRaw("HorizontalP2") != 0 || Input.GetAxisRaw("VerticalP2") != 0)
                _isMoving = true;
            else
                _isMoving = false;
        }

        if (!_arrowControl && Input.GetKey(KeyCode.Q) && _currentSkillTag != null) // Использование скилла первым игроком
        {
            _skillsController.UseSkill(_currentSkillTag, transform);
            _currentSkillTag = null;
            _animator.CrossFade("Pers", 0);
            GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load("Sprites/Character/Pers", typeof(Sprite)) as Sprite;
        }

        if (_arrowControl && Input.GetKey(KeyCode.RightControl) && _currentSkillTag != null) // Использование скилла вторым игроком
        {
            _skillsController.UseSkill(_currentSkillTag, transform);
            _currentSkillTag = null;
            _animator.CrossFade("Pers", 0);
            GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load("Sprites/Character/Pers", typeof(Sprite)) as Sprite;
        }
    }

    private void FixedUpdate()
    {
        _animator.speed = 0;

        if (!_arrowControl && _isMoving)
        {
            // Поворот
            _rotationZ -= Input.GetAxis("HorizontalP1") * _rotationSpeed;
            _rb.transform.rotation = Quaternion.Euler(0, 0, _rotationZ);
            // Движение вперед-назад
            if (Input.GetAxisRaw("VerticalP1") < 0)
            {
                _movement.y = (transform.position.y - _headTransform.position.y) * Input.GetAxis("VerticalP1") * _moveSpeed / 2; // скорость движения назад в 2 раза меньше 
                _movement.x = (transform.position.x - _headTransform.position.x) * Input.GetAxis("VerticalP1") * _moveSpeed / 2;
            }
            else
            {
                _movement.y = (transform.position.y - _headTransform.position.y) * Input.GetAxis("VerticalP1") * _moveSpeed;
                _movement.x = (transform.position.x - _headTransform.position.x) * Input.GetAxis("VerticalP1") * _moveSpeed;
            }
            _rb.velocity -= _movement;
            _animator.speed = 1;
        }

        if (_arrowControl && _isMoving)
        {
            // Поворот
            _rotationZ -= Input.GetAxis("HorizontalP2") * _rotationSpeed;
            _rb.transform.rotation = Quaternion.Euler(0, 0, _rotationZ);
            // Движение вперед-назад
            if (Input.GetAxisRaw("VerticalP2") < 0)
            {
                _movement.y = (transform.position.y - _headTransform.position.y) * Input.GetAxis("VerticalP2") * _moveSpeed / 2; // скорость движения назад в 2 раза меньше 
                _movement.x = (transform.position.x - _headTransform.position.x) * Input.GetAxis("VerticalP2") * _moveSpeed / 2;
            }
            else
            {
                _movement.y = (transform.position.y - _headTransform.position.y) * Input.GetAxis("VerticalP2") * _moveSpeed;
                _movement.x = (transform.position.x - _headTransform.position.x) * Input.GetAxis("VerticalP2") * _moveSpeed;
            }
            _rb.velocity -= _movement;
            _animator.speed = 1;
        }

        if (_underEffect)
        {
            if (_effectDuration > 0)
            {
                _effectDuration -= 0.02f;
            }
            else
            {
                _moveSpeed = _originalMoveSpeed;
                _underEffect = false;
                _effect.SetActive(false);
                GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
        }    
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.tag == "Bacterium" || other.gameObject.tag == "Mucus")
        {
            if (other.gameObject.GetComponent<BoostWithEffect>())
                other.gameObject.GetComponent<BoostWithEffect>().UseBoost(this);
            else if (other.gameObject.GetComponent<BacteriumController>())
                other.gameObject.GetComponent<BacteriumController>().UseBoost(this);
        }
        else if(other.gameObject.tag == "SkillBox")
        {
            _skillsController.SetSkill(other.gameObject.name, this);
            Destroy(other.gameObject);
        }
    }

    public void FinishComplete()
    {
        if (_checkPoints[2])
        {       
            if (!_arrowControl)
                _gameManager.CompleteLapP1();
            else
                _gameManager.CompleteLapP2();
            ClearCheckPoints(); // Приравниваем все чекпоинты к false  
        }         
    }

    public void CheckPointComplete(int checkPointNumber)
    {
        if (checkPointNumber == 0)        
            _checkPoints[checkPointNumber] = true;
        else if (_checkPoints[checkPointNumber - 1])
            _checkPoints[checkPointNumber] = true;

        // Проверка на направление движения
        if (checkPointNumber == _lastCheckpoint)
        {
            if (checkPointNumber == 0)
                _lastCheckpoint = _checkPoints.Length - 1;
            else
                _lastCheckpoint = checkPointNumber - 1;
            Debug.Log(transform.name + "Едет в другую сторону");
            _gameManager.ShowWarning(transform.name);
        }
        else
        {
            _lastCheckpoint = checkPointNumber;
        }
    }

    private void ClearCheckPoints()
    {
        for (int i = 0; i < _checkPoints.Length; i++)
            _checkPoints[i] = false;
    }

    public void AddEffect(float duration, float speedChange, Sprite sprite) // Добавление эффекта ускорения или замедления на персонажа
    {
        _effectDuration = duration;
        _underEffect = true;
        _moveSpeed = _originalMoveSpeed + speedChange;
        _effect.SetActive(true);
        _effect.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void AddEffect(float duration, float speedChange)
    {
        _effectDuration = duration;
        _underEffect = true;
        _moveSpeed = _originalMoveSpeed + speedChange;        
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    public void AddSkill(string tag, string animName) // Добавление скилла
    {
        _currentSkillTag = tag;
        if(!_arrowControl)
            _animator.CrossFade(animName, 0);
        else
            _animator.CrossFade(animName, 0);
    }
}

