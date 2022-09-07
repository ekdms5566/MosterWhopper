
/***********************************************************************************************************************
 * 햄버거를 이루는 재료들 프리팹의 스크립트.
 * 필요하다면(Bun 등) 상속해 사용할 것
 **********************************************************************************************************************/

using UnityEngine;

namespace Burger.Scripts
{

public class BurgerComponent : MonoBehaviour
{
    // Transform 저장을 위한 클래스
    private class StoreTransform
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 LocalScale;
    }

    // 모델링 높이. Serialized
    [SerializeField] public float _height;
    [SerializeField] private string componentName;

    // 떨어지는 효과 위한 rigidbody
    private Rigidbody _rigidbody;
    // 떨어지는 효과 후 원위치 위한 transform 저장
    private StoreTransform _initialTf;
    // 모델 위치 오프셋
    [SerializeField]private Vector3 offset;
    // 떨어지는 애니메이션 재생 여부
    private bool _animated;
    // 생성자
    protected BurgerComponent(float height)
    {
        _animated = false;
    }
    
    public void Start()
    {
        //모델 오프셋 적용
        transform.Translate(offset);
        // 애니메이션 재생 설정
        if (_animated)
        {
            // Transform 저장
            _initialTf = new StoreTransform()
                {Position = transform.position, Rotation = transform.rotation, LocalScale = transform.localScale};
            // 떨어지는 효과 위해 Rigidbody 적용
            _rigidbody = gameObject.AddComponent<Rigidbody>();
            // 위로 살짝 이동
            transform.Translate(Vector3.up * 0.3f);
            // collision detection위한 태그 설정
            gameObject.tag = "BURGER_COMPONENT";
        }
    }

    // 애니메이션 재생 여부 설정. Instantiate 후 바로 호출할것.
    public void SetAnimated(bool animate)
    {
        _animated = animate;
    }
    
    // 충돌 이벤트 핸들러
    public void OnCollisionEnter(Collision col)
    {
        if (_animated && col.collider.CompareTag($"BURGER_COMPONENT"))
        {
            // 오작동 막기 위해 rigidbody 삭제
            Destroy(_rigidbody);
            // 원위치
            transform.SetPositionAndRotation(_initialTf.Position, _initialTf.Rotation);
        }
    }
    
    // 모델 위치 오프셋 설정
    public void setOffset(Vector3 offset)
    {
        this.offset = offset;
    }

    public string GetName()
    {
        return componentName;
    }
}
}
