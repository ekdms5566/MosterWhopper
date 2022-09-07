
/***********************************************************************************************************************
 * 햄버거를 쌓는 기반이 되는 프리팹의 스크립트.
 * 개별 재료 추가, 랜덤으로 버거 생성, 두 버거 비교 기능
 **********************************************************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Burger.Scripts
{
public class BurgerBase : MonoBehaviour
{
    // 버거 재료를 찾지 못했을 때 throw할 예외
    class NoSuchBurgerComponentException : Exception
    {
        public NoSuchBurgerComponentException(string message)
            : base(message)
        {
        }
    }
    // 연속으로 중복되지 않게 재료를 생성하는 팩토리
    class BurgerComponentNameFactory
    {
        private List<GameObject> prefabs;
        private string prevComponentName="";
        
        // 생성자. BurgerBase의 prefabs를 넘겨줄 것
        public BurgerComponentNameFactory(List<GameObject> prefabs)
        {
            this.prefabs = prefabs;
        }

        // 연속으로 중복되지 않는 재료를 생성한다.
        public string CreateComponentName()
        {
            // "Bun"이 아니고 직전의 재료와 다를때까지 랜덤 생성
            string componentName;
            do
            {
                componentName = prefabs[Random.Range(0, prefabs.Count)].name;
            } while (componentName == prevComponentName || componentName == "Bun" || componentName == "Crown");
            
            prevComponentName = componentName;
            return componentName;
        }
    }
    
    // 재료 GameObject 리스트
    private List<GameObject> _components;
    
    // 재료 프리팹 리스트. Serialized
    [SerializeField]
    private List<GameObject> prefabs;
    // 쌓을 때 약간씩 오차를 두어 자연스럽게 보이게 함. Serialized
    [SerializeField] private float stackInconsistency;

    [SerializeField] private float yScale = 1.0f;
    
    private float _height;
    // 버거 랜덤 생성을 위한 재료 생성 팩토리
    private BurgerComponentNameFactory _componentNameFactory;

    public BurgerBase()
    {
        _height = 0.0f;
        _components = new List<GameObject>();
    }

    public void Start()
    {
        gameObject.tag = "BURGER_COMPONENT";
        // prefabs가 에디터로부터 할당된 이후 팩토리 할당
        _componentNameFactory = new BurgerComponentNameFactory(prefabs);

    }
    
    // 재료 하나 추가. componentName은 "Bun", "Cheese" 등 재료이름, animated는 떨어지는 애니메이션 on/off
    public bool Add(string componentName, bool animated = true)
    {
        bool prefabFound = false;

        if (componentName == "Bun" && _components.Count != 0) componentName = "Crown";
        
        // prefabs 요소 이터레이션해서 검색. 람다
        prefabs.ForEach((prefab) =>
        {
            if (componentName == prefab.name)
            {
                float prefabHeight = prefab.GetComponent<BurgerComponent>()._height*yScale;
                // hierarchy상 this의 하위로서 Instantiate. 최대 stackInconsistency만큼의 offset
                GameObject instantiated = Instantiate
                (
                    prefab, 
                    transform.position+Vector3.up*(_height)
                                      +Vector3.forward*Random.Range(-stackInconsistency,+stackInconsistency) 
                                      +Vector3.right*Random.Range(-stackInconsistency,+stackInconsistency), 
                    transform.rotation, 
                    transform);
                
                // 애니메이션 설정 전달
                instantiated.GetComponent<BurgerComponent>().SetAnimated(animated);
                // 후에 관리할 수 있도록 저장
                _components.Add(instantiated);
                // 다음 재료가 쌓일 높이 증가
                _height += prefabHeight;
                // 추가 성공 여부 return 위해
                prefabFound = true;
            }
        });
        
        // 재료 찾을 수 없을 시 NoSuchBurgerComponentException throw
        if(!prefabFound) throw new NoSuchBurgerComponentException("Burger component name "+componentName+" not found.");
        return true;
    }
    
    // reference 버거와 this버거를 비교하여 같은지 확인
    public bool Compare(BurgerBase reference)
    {
        return (CompareLevel(reference) == _components.Count && this.GetLevel() == reference.GetLevel());
    }
    
    // reference 버거의 '하위' 버거로 볼 수 있는지 확인
    public bool IsSubOf(BurgerBase reference)
    {
        return CompareLevel(reference) == _components.Count;
    }
    
    // this 버거를 reference 버거와 한층씩 비교하여 같은 최대 높이 확인
    public int CompareLevel(BurgerBase reference)
    {
        int level = 0;
        for(int i=0; i<_components.Count; i++)
        {
            if (_components[i].GetComponent<BurgerComponent>().GetName() == reference.GetLevelName(i))level++;
            else break;
        }
        return level;
    }

    // 무지성 랜덤 버거 생성
    public void GenerateRandom(int levels)
    {
        for (int i = 0; i < levels; i++)
        {
            this.Add(prefabs[Random.Range(0, prefabs.Count)].name, false);
        }
    }
    
    // stackInconsistency 설정
    public void SetStackInconsistency(float value)
    {
        stackInconsistency = value;
    }
    
    // 버거다운 버거 랜덤 생성
    public void GenerateRandomFeasible(int level)
    {
        Add("Bun", false);
        for (int i = 0; i < level-2; i++)
        {
            Add(_componentNameFactory.CreateComponentName(),false);
        }
        Add("Bun", false);
    }
    
    //level 층의 재료 이름 리턴
    public string GetLevelName(int level)
    {
        if (_components.Count <= level) return "";

        return _components[level].GetComponent<BurgerComponent>().GetName();
    }

    // 현재 버거 높이(재료수) 리턴
    public int GetLevel()
    {
        return _components.Count;
    }

    public void Clear()
    {
        for (int i = 0; i < _components.Count; i++)
        {
            Destroy(_components[i]);
        }
        _components.Clear();
        _height = 0;
    }

    public void Copy(BurgerBase reference)
    {
        for(int i=0; i<reference.GetLevel(); i++)
        {
            this.Add(reference.GetLevelName(i), false);
        }
    }
}
}
