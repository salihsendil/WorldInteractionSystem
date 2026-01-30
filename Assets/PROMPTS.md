## Özet

| Bilgi | Değer |
|-------|-------|
| Toplam prompt sayısı | 14 |
| Kullanılan araçlar | Gemini / ChatGPT |
| En çok yardım alınan konular | Hold Type Button|
| Tahmini LLM ile kazanılan süre | 2 saat |

---

## Prompt 1: FPS Kamera Rotasyonu

**Araç:** [Gemini]
**Tarih/Saat:** 2026-01-29 19:10

**Prompt:**
```
Unity'de bir fps projesi için yeni input sistemi kullanarak mouse ile kamera rotasyonu yapan bir "HandleRotation" methodu yazar mısın?
```

**Alınan Cevap (Özet):**
```
Tabii ki! Unity'nin New Input System paketini kullanarak modern, akıcı ve performanslı bir kamera kontrolü yazalım.

Bu sistemde en önemli nokta, look değerini (Vector2) sistemden alıp, dikey rotasyonu (Pitch) kısıtlamak (Clamp) ve yatay rotasyonu (Yaw) karaktere uygulamaktır.

Gerekli Ön Hazırlık
Input System Package'ın yüklü olduğundan emin ol.

Bir Input Action Asset oluştur ve içine "Look" adında bir Value (Tipi: Vector2) tanımla.

Script'ine using UnityEngine.InputSystem; kütüphanesini ekle.

HandleRotation Metodu ve Script Yapısı
Aşağıdaki kod, profesyonel bir FPS kontrolcüsünün temel rotasyon mantığını içerir:

C#

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Settings")]
    public float mouseSensitivity = 25f;
    public Transform playerBody; // Karakterin kendisi (Yatay rotasyon için)
    
    private float xRotation = 0f; // Dikey bakış açısı takibi
    private Vector2 lookInput;

    // Input Action'dan gelen veriyi yakalamak için (OnLook mesajı veya PlayerInput bileşeni ile)
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void HandleRotation()
    {
        // 1. Mouse verisini al ve hassasiyetle çarp
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        // 2. Dikey rotasyonu hesapla (Yukarı/Aşağı)
        xRotation -= mouseY;
        // Kameranın takla atmasını engellemek için 90 derece ile sınırlandırıyoruz
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 3. Rotasyonları uygula
        // Kamerayı sadece dikeyde döndür (Bu script kamera üzerinde olmalı)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakter gövdesini yatayda döndür (Sağa/Sola)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Update()
    {
        HandleRotation();
    }

    void Start()
    {
        // Mouse imlecini ekranın ortasına kilitle ve gizle
        Cursor.lockState = CursorLockMode.Locked;
    }
}
Dikkat Etmen Gereken 3 Püf Noktası
Sensitivity (Hassasiyet): New Input System, eski sisteme göre daha yüksek ham değerler döndürebilir. Eğer kamera çok hızlı dönüyorsa mouseSensitivity değerini düşürmeyi veya 0.1f gibi bir çarpan eklemeyi dene.

Update vs LateUpdate: Eğer karakter hareketinde titreme (jittering) hissedersen, HandleRotation metodunu LateUpdate içerisinde çağırmak daha pürüzsüz bir görüntü sağlayabilir.

Frame Rate Bağımsızlığı: Time.deltaTime kullanımı, oyuncunun FPS değeri ne olursa olsun dönüş hızının sabit kalmasını sağlar.

Bu scripti karakterinin üzerindeki kameraya atayıp "Player Body" kısmına karakterin ana objesini sürüklemeyi unutma.

Karakterin zıplama veya yürüme mekanikleri için de yardıma ihtiyacın var mı?
```

**Nasıl Kullandım:**
- [ ] Direkt kullandım (değişiklik yapmadan)
- [+] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Mouse ile kamera rotasyonu.
> Tekrar düzenlemesini istedim, çünkü hatalar vardı.

**Yapılan Değişiklikler (adapte ettiyseniz):**
> Hatayı test ederek buldum ve düzeltmesini istedim.

## Prompt 2: Fps Kamera Look

**Araç:** [Gemini]
**Tarih/Saat:** 2026-01-29 19:13

**Prompt:**
```
PlayerCam(MainCam) player gameobjectinin içerisinde bulunuyor ve karakterim kapsül. İstiyorum ki sağa sola dönerken karakterde etrafında dönsün ama yukarı aşağı bakarken sadece kamera dönsün. Zenject kullanıyorum ve inputları yakalayıp işleyen "Input Handler" sınıfım var. Bu sınıf mouse delta'yı tutuyor. "PlayerLook" sınıfına ise bu detaylara göre uyarlama yaparak verdiğin kodları yapıştırdım. Eksiklikleri tamamla.
```

**Alınan Cevap (Özet):**
```
Zenject kullanıyor olman harika; bu, bağımlılık yönetimini çok daha profesyonel bir seviyeye çeker. InputHandler sınıfından MouseDelta verisini çekerek, kamerayı ve karakter gövdesini birbirinden bağımsız ama koordineli şekilde döndüreceğiz.

İstediğin mantık (yatayda karakterle beraber, dikeyde sadece kamera) FPS oyunları için standarttır. İşte Zenject yapına uygun güncellenmiş PlayerLook sınıfı:

PlayerLook Sınıfı (Zenject Uyumlu)
C#

using UnityEngine;
using Zenject;

public class PlayerLook : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private Transform playerCamera; // SerializeField ile Inspector'dan atanabilir veya Inject edilebilir
    
    private InputHandler _inputHandler;
    private float _xRotation = 0f;

    [Inject]
    public void Construct(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        // Eğer playerCamera script'in bağlı olduğu objeyse otomatik ata
        if (playerCamera == null) playerCamera = transform; 
    }

    private void LateUpdate()
    {
        HandleRotation();
    }

    public void HandleRotation()
    {
        // 1. InputHandler üzerinden veriyi çek
        Vector2 mouseDelta = _inputHandler.MouseDelta;

        float mouseX = mouseDelta.x * sensitivity;
        float mouseY = mouseDelta.y * sensitivity;

        // 2. Dikey Rotasyon (Sadece Kamera)
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // Kameranın localRotation'ını değiştir (Ebeveyni olan Capsule etkilenmez)
        playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        // 3. Yatay Rotasyon (Karakter Gövdesi/Capsule)
        // Bu script Capsule üzerinde olduğu için transform.Rotate kullanıyoruz
        transform.Rotate(Vector3.up * mouseX);
    }
}
Dikkat Etmen Gereken Yapısal Detaylar
Scriptin Konumu: Bu PlayerLook scriptini direkt olarak Kapsül (Player) objesine atmalısın.

Referans Ayarı: * playerCamera değişkenine Kapsül'ün içindeki Main Camera'yı sürükle.

transform.Rotate komutu scriptin üzerinde olduğu ana objeyi (Kapsülü) döndürürken, playerCamera.localRotation sadece kamerayı dikeyde hareket ettirir.

InputHandler Notu: InputHandler sınıfında MouseDelta özelliğinin (Property) dışarıya açık olduğundan emin ol. Genellikle şu şekilde görünmelidir:

public Vector2 MouseDelta { get; private set; }

Neden LateUpdate? Kamera rotasyonunu LateUpdate içerisinde yapmak, karakterin hareketiyle (Update veya FixedUpdate) çakışmaları önler ve "stuttering" dediğimiz titremeyi engeller.
```

**Nasıl Kullandım:**
- [+] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Bir önceki promptun hataları giderilmiş hali. Direkt implemente ettim ve zaman kazandırdı.

## Prompt 3: Hold Input Girdisi

**Araç:** [Gemini]
**Tarih/Saat:** 2026-01-29 20:02

**Prompt:**
```
unity yeni input sisteminde tek tuşa bazı objelerle sadece tıklama ile etkilişime geçicem bazıları ile basılı tutma ile etkileşime geçicem. nasıl yapabilirim?
```

**Alınan Cevap (Özet):**
```
1️⃣ Input Action’ı SADECE “Interact” olarak tanımla

Tek action, tek tuş

Action Type: Button
Binding: E (veya sen ne kullanıyorsan)
Interaction: ❌ boş bırak

⚠️ Interaction’ı input’ta çözmeye çalışma
“Hold mu Press mi” kararı objeye ait olmalı, input’a değil.

2️⃣ Input’tan sadece 3 sinyali al

Player tarafında şunları dinle:

interactAction.started   // tuşa basıldığı an
interactAction.performed // hold süresi dolunca VEYA press'te hemen
interactAction.canceled  // tuş bırakıldığında


Bu sana basıldı mı / tutuluyor mu / bırakıldı mı bilgisini verir.

3️⃣ Interactable için bir “contract” oluştur (çok kritik)

Objeler nasıl etkileşime gireceğini kendisi söylesin.

public interface IInteractable
{
    InteractType InteractType { get; }
    void OnInteractStart();
    void OnInteractHold(float holdTime);
    void OnInteractComplete();
    void OnInteractCancel();
}

public enum InteractType
{
    Click,
    Hold
}


💡 SRP + OCP + extensible
Yarın “Charge”, “Mash”, “Timed” eklemek istersen mimari bozulmaz.

4️⃣ Player tarafı (karar merkezi)

Player sadece:

Önündeki objeyi bulur

Objenin InteractType’ına bakar

Input sinyalini ona iletir

private IInteractable currentInteractable;
private float holdTimer;

void OnInteractStarted()
{
    if (currentInteractable == null) return;

    holdTimer = 0f;
    currentInteractable.OnInteractStart();
}

void OnInteractPerformed()
{
    if (currentInteractable == null) return;

    if (currentInteractable.InteractType == InteractType.Click)
    {
        currentInteractable.OnInteractComplete();
    }
}

void OnInteractCanceled()
{
    if (currentInteractable == null) return;

    if (currentInteractable.InteractType == InteractType.Hold)
    {
        currentInteractable.OnInteractCancel();
    }
}


Hold için Update’te:

void Update()
{
    if (currentInteractable == null) return;

    if (currentInteractable.InteractType == InteractType.Hold &&
        interactAction.IsPressed())
    {
        holdTimer += Time.deltaTime;
        currentInteractable.OnInteractHold(holdTimer);
    }
}

5️⃣ Click objesi örneği (Kapı, buton)
public class Door : MonoBehaviour, IInteractable
{
    public InteractType InteractType => InteractType.Click;

    public void OnInteractStart() { }

    public void OnInteractHold(float holdTime) { }

    public void OnInteractComplete()
    {
        OpenDoor();
    }

    public void OnInteractCancel() { }
}

6️⃣ Hold objesi örneği (Sandık, kazma, hackleme)
public class Chest : MonoBehaviour, IInteractable
{
    public InteractType InteractType => InteractType.Hold;
    public float requiredHoldTime = 2f;

    public void OnInteractStart()
    {
        ShowProgressUI();
    }

    public void OnInteractHold(float holdTime)
    {
        UpdateProgress(holdTime / requiredHoldTime);

        if (holdTime >= requiredHoldTime)
        {
            OnInteractComplete();
        }
    }

    public void OnInteractComplete()
    {
        OpenChest();
    }

    public void OnInteractCancel()
    {
        ResetProgress();
    }
}

7️⃣ Neden BU yol doğru?

✔ Input sistemi şişmez
✔ Aynı tuş → sınırsız etkileşim tipi
✔ Obje kendi davranışından sorumlu (SRP)
✔ UI (progress bar vs.) objeye bağlı olur
✔ Multiplayer’a da taş gibi taşınır
```

**Nasıl Kullandım:**
- [ ] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [+] Reddettim (kullanmadım)

**Açıklama:**
> Reddetme sebebim aşağıdaki promptta

## Prompt 4: Hold Button Input

**Araç:** [ChatGpt]
**Tarih/Saat:** 2026-01-29 20:10

**Prompt:**
```
işte bu durumda door kullanmayacağı methodlara sahip oluyor.
```

**Alınan Cevap (Özet):**
```
Aynen. 💯
Ve bu itirazın çok doğru, hatta “iyi mimari refleksi olan developer” itirazı bu.

“Door, hiç kullanmayacağı OnInteractHold, OnInteractCancel gibi metodlara neden sahip?”

Bu tam olarak Interface Segregation Principle (ISP) alarmı 🚨

Şimdi sana gerçekten temiz, production’da kullanılan 2 doğru çözümü göstereyim.
İlk anlattığım yapı çalışır, ama ideal değil. Senin dediğin gibi.

🎯 Asıl Doğru Yaklaşım: Interface’leri Böl
❌ Kötü Koku
public interface IInteractable
{
    void OnInteractStart();
    void OnInteractHold(float t);
    void OnInteractComplete();
    void OnInteractCancel();
}


→ Door: “Ben hold kullanmıyorum kardeşim”
→ Sandık: “Click umrumda değil”

✅ ÇÖZÜM 1 (EN TEMİZ): Interaction Type’a Göre Interface
1️⃣ Base marker
public interface IInteractable
{
    void Interact();
}

2️⃣ Click için
public interface IClickInteractable : IInteractable
{
    void OnClick();
}

3️⃣ Hold için
public interface IHoldInteractable : IInteractable
{
    float RequiredHoldTime { get; }
    void OnHoldStart();
    void OnHoldProgress(float t);
    void OnHoldComplete();
    void OnHoldCancel();
}

Door artık tertemiz
public class Door : MonoBehaviour, IClickInteractable
{
    public void Interact()
    {
        OnClick();
    }

    public void OnClick()
    {
        OpenDoor();
    }
}

Chest (Hold)
public class Chest : MonoBehaviour, IHoldInteractable
{
    public float RequiredHoldTime => 2f;

    public void OnHoldStart()
    {
        ShowUI();
    }

    public void OnHoldProgress(float t)
    {
        UpdateBar(t / RequiredHoldTime);
    }

    public void OnHoldComplete()
    {
        OpenChest();
    }

    public void OnHoldCancel()
    {
        HideUI();
    }
}

Player tarafı (karar merkezi ama type-safe)
void OnInteractStarted()
{
    if (current is IHoldInteractable hold)
    {
        holdTimer = 0;
        hold.OnHoldStart();
    }
}

void Update()
{
    if (current is IHoldInteractable hold && interactAction.IsPressed())
    {
        holdTimer += Time.deltaTime;
        hold.OnHoldProgress(holdTimer);

        if (holdTimer >= hold.RequiredHoldTime)
        {
            hold.OnHoldComplete();
        }
    }
}

void OnInteractPerformed()
{
    if (current is IClickInteractable click)
    {
        click.OnClick();
    }
}

void OnInteractCanceled()
{
    if (current is IHoldInteractable hold)
    {
        hold.OnHoldCancel();
    }
}


✔ Door hold kodu görmez
✔ Chest click kodu görmez
✔ ISP %100 uygulanmış

✅ ÇÖZÜM 2 (DAHA DA PRO): Composition over Inheritance

AAA ve framework’lerde daha sık görülen yol......Devam Ediyor.
```

**Nasıl Kullandım:**
- [ ] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [+] Reddettim (kullanmadım)

**Açıklama:**
> İlk çözümü beğenmedim - ikinci çözüm bu durum için over-engineering geldi.

## Prompt 5: FPS Kamera Rotasyonu

**Araç:** [ChatGPT]
**Tarih/Saat:** 2026-01-30 03:10

**Prompt:**
```
unitask ile tmp_text'i setleyen bir saniye sonra opacity düşürüp invisible yapan kodu yazar mısın?
```

**Alınan Cevap (Özet):**
```
Tabii. Kısa, net ve UniTask’e yakışır şekilde yazıyorum.
Coroutine yok, GC yok, okunaklı 👍

Aşağıdaki kod:

TMP_Text’i set eder

1 saniye bekler

opacity’yi yumuşakça düşürür

invisible (alpha = 0) yapar

✅ UniTask + TMP_Text Fade Out
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class TMPFadeController : MonoBehaviour
{
    [SerializeField] private TMP_Text tmpText;
    [SerializeField] private float fadeDuration = 0.5f;

    public async UniTaskVoid ShowMessage(string message)
    {
        tmpText.text = message;

        SetAlpha(1f);
        tmpText.gameObject.SetActive(true);

        await UniTask.Delay(1000);

        await FadeOut();

        tmpText.gameObject.SetActive(false);
    }

    private async UniTask FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            SetAlpha(alpha);
            await UniTask.Yield();
        }

        SetAlpha(0f);
    }

    private void SetAlpha(float alpha)
    {
        var color = tmpText.color;
        color.a = alpha;
        tmpText.color = color;
    }
}

🧠 Kullanım
ShowMessage("Door is Locked!");


UniTaskVoid burada bilerek kullanıldı
UI fire-and-forget senaryosu → gayet uygun

⚠️ Küçük ama önemli notlar
🔹 Aynı anda üst üste çağrılacaksa

Bu kod üst üste çağrılınca çakışır.
Eğer ihtimal varsa, söyle → CancellationToken’lı versiyonunu yazarım.

🔹 TimeScale bağımsız olsun dersen
await UniTask.Delay(1000, ignoreTimeScale: true);
```

**Nasıl Kullandım:**
- [+] Direkt kullandım (değişiklik yapmadan)
- [ ] Adapte ettim (değişiklikler yaparak)
- [ ] Reddettim (kullanmadım)

**Açıklama:**
> Floating Text için kullanılan kod.
> Sorunsuz çalışıyordu direkçt kullandım.




**Özet LLM Hakkında** 
Yaklaşık kullanılan prompt sayısını ve detayları giriş kısmında verdim. Ancak hepsini kopyala yapıştır yapıp hem dosyayı uzatmak istemiyorum hemde son 10 dakikam içerisindeyim. Yapay zekayı aktif kullansam da bir miktarda bloglar ve youtube videoları üzerinden araştırma yapmayı seviyorum. Tamamen yapay zeka bağımlısı değilim.