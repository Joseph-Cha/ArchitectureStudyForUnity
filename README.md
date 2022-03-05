# MVP 패턴에 대해서

## 왜 MVP 패턴을 스터디 하게 되었을까?

첫 출발점은 “데이터를 어떻게 하면 잘 관리(또는 구분) 할 수 있을까?”란 질문에서 시작 되었다.

최근 들어 기획에서 사용자가 자유롭게 앱(또는 앱 내부 특정 기능)을 커스텀하는 기능을 요구하고 있다. 사용자가 자유롭게 앱을 꾸미거나 또는 캐릭터를 자유롭게 꾸미는 등 결국 사용자 입맛에 맛도록 원하는 설정을 다양하게 제공을 해줘야 한다는 이야기를 듣고 있다.

어떻게 하면 좋을까를 계속 고민을 하다가 결국 든 생각은 **데이터를 별도로 관리**하는 것이었다.

즉, 클래스 내부에서 데이터를 관리하는 것이 아니라 별도의 파일 형태로 관리를 하는 방식을 사용해보고자 한다. 

이와 같이 데이터와 로직을 구분 짓기 위한 방법을 찾던 중 MVP 패턴을 발견하게 되어 스터디를 시작하게 되었다.

## 우선 어떻게 데이터를 별도의 파일 형태로 관리를 해줄까?

Unity에서 데이터 자체를 Asset 형태로 관리 해주는 기능이 있는데 바로 `Scriptable Object`이다. 

(Scriptable Object에 대한 설명은 여기 [링크](https://docs.unity3d.com/kr/2018.4/Manual/class-ScriptableObject.html)를 참조)

## 그래서 MVP 패턴을 어떻게 활용하면 좋을까?

MVP 패턴 자체에 대한 설명은 여기 [블로그](https://velog.io/@sonohoshi/MVP-%ED%8C%A8%ED%84%B4-with-Unity)에 자세히 나와 있으니 참고하면 좋을 것 같다.

여기서 직접 해볼 것은 `Slider를 통해 캐릭터의 머리 색상을 변경`해보는 시나리오를 MVP 패턴으로 구현을 해보는 것이다.

### <시나리오>

`View`  

⇓ Slider를 이동

`Presenter` 

⇓ 변경된 UI 값을 Scriptable Object에 저장

`Scriptable Obejct`  

일단 여기까지 구현을 해보고 `Scriptable Obejct`의 값을 어떻게 캐릭터에게 전달을 해줄지 이어서 이야기를 해보자.

## 구현부

1. 씬 구성
    
![image](https://user-images.githubusercontent.com/75019048/154803805-131a6718-2a2a-4d3e-88ca-921848f19184.png)

2. 소스
    
    `HairColorPresenter.cs`
    
    ```csharp
    using UnityEngine;
    using UnityEngine.UI;
    
    public class HairColorPresenter : MonoBehaviour
    {
        // View
        [SerializeField]
        private Slider RedColor_Slider;
        [SerializeField]
        private Slider GreenColor_Slider;
        [SerializeField]
        private Slider BlueColor_Slider;
    
        // Model
        [SerializeField]
        private HairColorModel HairColorModel;
    
        private void Awake()
        {
            RedColor_Slider.onValueChanged.AddListener(v => HairColorModel.RedValue = v);
            GreenColor_Slider.onValueChanged.AddListener(v => HairColorModel.GreenValue = v);
            BlueColor_Slider.onValueChanged.AddListener(v => HairColorModel.BlueValue = v);
        }
    }
    ```
    
    `HairColorModel.cs`
    
    ```csharp
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "HairColorModel", menuName = "Hair")]
    public class HairColorModel : ScriptableObject
    {
        public float RedValue;
        public float GreenValue;
        public float BlueValue;
    }
    ```
    

### Slider를 변경 시켰을 때

1. Slider의 변경된 값이 바로 Sciptable Object에 저장되는 것을 볼 수 있다.
    
![image](https://user-images.githubusercontent.com/75019048/154803813-f53f4da6-531a-4a1f-b9aa-ddb55386f40b.png)

2. (중요)**Play를 종료해도 `Sciptable Object` 값이 그대로 있는 것을 알 수 있다.**
    
![image](https://user-images.githubusercontent.com/75019048/154803817-2a8afd0e-7311-47f6-b7ae-bbd110c6d806.png)    

## 이제 어떻게 하면 Model 값이 변경 되었을 때 변경된 값을 GameObject에 알려줄 수 있을까?

일단 UI를 수정 했을 때 Model 데이터를 저장하는 것까지는 구현이 되었다.

이제 남은 것은 변경된 Model의 데이터를 GameObject에도 반영을 시켜주는 것이다.

이를 위해 다양한 방식이 있겠지만 두가지 방식을 활용해서 구현을 해보려고 한다.

1. Addressable System
2. 이벤트 구독

Addressable은 요즘 유니티에서 밀고 있는 Asset 관리 시스템인데 엄청 깊게 들어가기 보다 Asset이 있는 경로 값을 통해 에셋을 쉽게 불러오는 로직만 사용해보려고 한다.

그리고 이벤트 구독 방식은 소스를 보면 이해가 될 것이다.

## Addressable을 활용해서 Model 데이터 불러오기

Addressable에 대해서는 여기 [링크](https://young-94.tistory.com/47)를 통해 참고를 하면 좋을 것 같다.

1. 씬 셋팅
    
![image](https://user-images.githubusercontent.com/75019048/154803820-5e54b0ff-dded-42d7-99cd-f49b7b929690.png)

2. Addressables 설정
    
![image](https://user-images.githubusercontent.com/75019048/154803822-57235049-7165-4d40-9e73-2da84d583415.png)

3. Model 데이터 불러오기
    
    ```csharp
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.AddressableAssets;
    
    public class Hair : MonoBehaviour
    {
        [SerializeField]
        private Image Image;
    
        private HairColorModel hairColor { get; set; }
    
        private void Awake()
        {
            Addressables.LoadAssetAsync<HairColorModel>("HairModel").Completed += ao =>
            {
                if (ao.IsDone)
                {
                    hairColor = ao.Result;
                }
            };
        }
    }
    ```
    

## 이벤트 구독 방식 구현

1. HairColorModel에서 이벤트 생성
    
    ```csharp
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    
    [CreateAssetMenu(fileName = "HairColorModel", menuName = "Hair")]
    public class HairColorModel : ScriptableObject
    {
        public float RedValue;
        public float GreenValue;
        public float BlueValue;
    		
    	// 각 Color 값이 변경 되었을 때 실행될 이벤트 추가
        public UnityAction<float> OnRedValueChanged;
        public UnityAction<float> OnGreenValueChanged;
        public UnityAction<float> OnBlueValueChanged;
    }
    ```
    
2. Presenter에서 Slider와 Model의 이벤트를 연결
    
    ```csharp
    using UnityEngine;
    using UnityEngine.UI;
    
    public class HairColorPresenter : MonoBehaviour
    {
        // View
        [SerializeField]
        private Slider RedColor_Slider;
        [SerializeField]
        private Slider GreenColor_Slider;
        [SerializeField]
        private Slider BlueColor_Slider;
    
        // Model
        [SerializeField]
        private HairColorModel HairColorModel;
    
        private void Awake()
        {
            // UI가 변경 되었을 때 
            // Model에 값을 저장하고 해당 이벤트를 실행
            RedColor_Slider.onValueChanged.AddListener(v =>
            {
                HairColorModel.RedValue = v;
                HairColorModel.OnRedValueChanged?.Invoke(v);
            });
    
            GreenColor_Slider.onValueChanged.AddListener(v =>
            {
                HairColorModel.GreenValue = v;
                HairColorModel.OnGreenValueChanged?.Invoke(v);
            });
    
            BlueColor_Slider.onValueChanged.AddListener(v =>
            {
                HairColorModel.BlueValue = v;
                HairColorModel.OnBlueValueChanged?.Invoke(v);
            });
        }
    }
    ```
    
3. Hair에서 Model의 이벤트를 구독
    
    ```csharp
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.AddressableAssets;
    
    public class Hair : MonoBehaviour
    {
        [SerializeField]
        private Image Image;
    
        private HairColorModel hairColor { get; set; }
    
        private Color color = new Color(r: 0, g: 0, b: 0, a: 1);
    
        private void Awake()
        {
            Addressables.LoadAssetAsync<HairColorModel>("HairModel").Completed += ao =>
            {
                if (ao.IsDone)
                {
                    hairColor = ao.Result;
    								
                    // 모델이 들고 있는 이벤트 구독
                    hairColor.OnRedValueChanged += ChangeRedColor;
                    hairColor.OnGreenValueChanged += ChangeGreenColor;
                    hairColor.OnBlueValueChanged += ChangeBlueColor;
                }
            };
        }
    
        private void ChangeRedColor(float value)
        {
            color.r = value;
            Image.color = color;
        }
    
        private void ChangeGreenColor(float value)
        {
            color.g = value;
            Image.color = color;
        }
    
        private void ChangeBlueColor(float value)
        {
            color.b = value;
            Image.color = color;
        }
    }
    ```
    

## 실제 작동 확인

![image](https://user-images.githubusercontent.com/75019048/154803829-c25aa156-45a9-495d-9549-afae38cb4f38.png)

## 결론

굳이 이렇게 어렵게 구현을 해야하나 싶은 의구심이 생길 수도 있지만,

앞서 이야기 한 것처럼 **데이터와 로직을 구분을 짓기 위해서** 위와 같은 방식을 사용해보았다.

즉, 데이터를 가지고 와서 로직을 처리하는 Hair class에서는 UI를 전혀 몰라도 되고 

데이터를 활용한 로직을 어떻게 짤 것인지에만 집중을 하면 된다.

그리고 여기서 아래 부분을 더 보안하면 조금 더 코드가 깔끔해 질 것 같다.

1. HairColorModel에서 속성 값이 변경되면 바로 이벤트가 실행 시킬 수 있는 로직 추가
2. RGB 값 변경에서 중복되는 코드 제거 → RGB 값을 따로 따로 구분 짓는 것이 아니라 하나의 함수로 빼놓는 방식
