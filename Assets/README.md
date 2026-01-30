# Interaction System - [Adınız Soyadınız]

> Ludu Arts Unity Developer Intern Case

## Proje Bilgileri

| Bilgi | Değer |
|-------|-------|
| Unity Versiyonu | 6000.0.58f2 |
| Render Pipeline | URP |
| Case Süresi | 12 saat |
| Tamamlanma Oranı | %90 |

---

## Kurulum

1. Repository'yi klonlayın:
```bash
git clone https://github.com/salihsendil/WorldInteractionSystem.git
```

2. Unity Hub'da projeyi açın
3. `Assets/WorldInteractionSystem/Scenes/TestScene.unity` sahnesini açın
4. Play tuşuna basın

---

## Nasıl Test Edilir

### Kontroller

| Tuş | Aksiyon |
|-----|---------|
| WASD | Hareket |
| Mouse | Bakış yönü |
| E | Etkileşim |

### Test Senaryoları

1. **Door Test:**
   - Kapıya yaklaşın, "Press E to Open" mesajını konsolda görün
   - E'ye basın, kapı açılsın
   - Tekrar basın, kapı kapansın

2. **Key + Locked Door Test:**
   - Kilitli kapıya yaklaşın, "Locked - Key Required" mesajını görün
   - Anahtarı bulun ve toplayın
   - Kilitli kapıya geri dönün, şimdi açılabilir olmalı

3. **Switch Test:**
   - Switch'e yaklaşın ve aktive edin
   - Bağlı nesnenin (kapı/ışık vb.) tetiklendiğini görün

4. **Chest Test:**
   - Sandığa yaklaşın
   - E'ye basılı tutun, progress bar dolsun
   - Sandık açılsın ve içindeki item alınsın

---

## Mimari Kararlar

### Interaction System Yapısı

```
[Mimari diyagram veya açıklama]
```

**Neden bu yapıyı seçtim:**
> Genişlemeye açık olması ve kod tekrarını minumuma düşürmek. Yeni özellik eklemek için mevcut kodların değiştirilmesine gerek olmaması. 

**Alternatifler:**
> Etkileşimli nesneleri tek interface altında toplamak ve modüler/behaviour(Toggle Behaviour, Hold Behaviour) yapı tercih etmek. Ekstra olarak birden çok davranışa sahip eşyalarda öncelikli davranış seçebilme imkanı sunması. 

**Trade-off'lar:**
> Okunaklı olması, bir geliştirici için alışılmış bir yapıya sahip olması, script sayısının fazla olmaması, performanslı bir yapıda olması avantajlarından bir kaç tanesi olarak sayılabilir.
Dezavantajları ise, ISP ihlaline sebep olması, minimum düzeyde de olsa kod tekrarı yapılması. 

### Kullanılan Design Patterns

| Pattern | Kullanım Yeri | Neden |
|---------|---------------|-------|
| [Observer] | [Event system] | Switch ile kapı açmak için Action event kullanımı |
| [Observer] | [Event system] | Script-UI arası low coupling sağlayarak iletişim kurulması |

---

## Ludu Arts Standartlarına Uyum

### C# Coding Conventions

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| m_ prefix (private fields) | [ ] / [✅] | |
| s_ prefix (private static) | [ ] / [ ] | Kullanılmadı. |
| k_ prefix (private const) | [ ] / [✅] | |
| Region kullanımı | [x] / [ ] | Az kullanıldı o yüzden işaretlenmedi. |
| Region sırası doğru | [ ] / [✅] | Az kullanıldı sırası doğru. |
| XML documentation | [x] / [ ] | Mevcut değil. Süre yetmedi. |
| Silent bypass yok | [ ] / [✅] | |
| Explicit interface impl. | [x] / [ ] | İhtiyaç olmadı. Bu sebeple işaretlenmedi. |

### Naming Convention

| Kural | Uygulandı | Örnekler |
|-------|-----------|----------|
| P_ prefix (Prefab) | [ ] / [✅ ] | P_Door, P_Chest |
| M_ prefix (Material) | [ ] / [✅ ] | M_Door_Wood |
| T_ prefix (Texture) | [x] / [ ] | Texture mevcut değil. |
| SO isimlendirme | [ ] / [✅] | Script sonları SO ile, anahtar için "k_" prefix kullanıldı. |

### Prefab Kuralları

| Kural | Uygulandı | Notlar |
|-------|-----------|--------|
| Transform (0,0,0) | [] / [✅] | Scale değerleri değiştirildi. |
| Pivot bottom-center | [ ] / [✅] |Primitive Types kullanıldı. |
| Collider tercihi | [] / [✅] | Box ve Capsule  |
| Hierarchy yapısı | [x] / [ ] | |

### Zorlandığım Noktalar
> [Standartları uygularken zorlandığınız yerler]

---

## Tamamlanan Özellikler

### Zorunlu (Must Have)

- [] / [✅] Core Interaction System
  - [] / [✅] IInteractable interface
  - [] / [✅] InteractionDetector
  - [] / [✅] Range kontrolü

- [] / [✅] Interaction Types
  - [] / [✅] Instant
  - [] / [✅] Hold
  - [] / [✅] Toggle

- [] / [✅] Interactable Objects
  - [] / [✅] Door (locked/unlocked)
  - [] / [✅] Key Pickup
  - [] / [✅] Switch/Lever
  - [] / [✅] Chest/Container

- [x] / [✅] UI Feedback
  - [] / [✅] Interaction prompt
  - [x] / [✅] Dynamic text - (Buglar Mevcut.)
  - [] / [✅] Hold progress bar
  - [] / [✅] Cannot interact feedback

- [x] / [ ] Simple Inventory
  - [] / [✅] Key toplama
  - [x] / [ ] UI listesi - (Süre Yetmedi.)

### Bonus (Nice to Have)

- [ ] Animation entegrasyonu
- [ ] Sound effects
- [✅] Multiple keys / color-coded
- [ ] Interaction highlight
- [ ] Save/Load states
- [✅] Chained interactions

---

## Bilinen Limitasyonlar

### Tamamlanamayan Özellikler
1. UI Listesi için UI Panel - Altyapı olarak her şey hazır ancak sürem yetmediği için hazırlayamadım.
2. Bonus Özellikler - Süre yeterli hazır asset bulmak için yeterli gelmedi. Primitive objeleri hareketlendirmek için de süre yeterli gelmedi.

### Bilinen Bug'lar
1. Dynamic Text Bug - Interactor içerisinde update methodunda sürekli ray atılıp IInteractable öğesi aranıyor. Eğer bulunursa text her frame örn: "Press E for..." şeklinde setleniyor. Dolayısıyla kapı "Door Locked!" texti setlenmiyor. (Bir frame setlendikten sonra text tekrar değişiyor.) Bug'ı biliyorum. Çözümünü henüz bilmiyorum. Çok karışık bir bug olduğunu düşünmüyorum.
2. Door.cs scriptini maalesef çok kötü kodladığımın farkındayım.

### İyileştirme Önerileri
1. Kendimi game jam'de gibi hissettim. Yazılımcının hızlı olması güzel bir şey ancak. Zamandan tasarruf maalesef arkamda açık bırakmama sebep oldu. Bu konuda kendimi daha da geliştirebileceğimi düşünüyorum. 
2. Animator controller'ın hazır verilmesi bonus kısmın yapılmasına kesinlikle faydası olur diye düşünüyorum. State machine yazmayı düşündüm ancak animasyon hazırlamaya vakit kaybetmek istemedim. Animasyon kodlarını da controll logic bulunduran scriptte tutmak istemedim. O yüzden onu boş bırakmak zorunda kaldım.
3. Düşünmek yerine tamamen bildiklerimi koda dökmekle geçirdim bütün süreci.

---

## Ekstra Özellikler

Zorunlu gereksinimlerin dışında eklediklerim:

1. **[Özellik Adı]**
   - Açıklama: Chest eşya spawn ediyor.
   - Neden ekledim: Zaten istenen bir şeydi ancak kendi etrafında rastgele bir konumda spawn olmasını sağladım.

---

## Dosya Yapısı

```
Assets/
├── WorldInteractionSystem/
│   ├── Scripts/
│   │   ├── Runtime/
│   │   │   ├── Core/
│   │   │   │   ├── IInteractable.cs
│   │   │   │   ├── BaseInteractableHold.cs
│   │   │   │   ├── BaseInteractableInstant.cs
│   │   │   │   └── BaseInteractableToggle.cs
│   │   │   ├── Interactables/
│   │   │   │   ├── Chest.cs
│   │   │   │   ├── Door.cs
│   │   │   │   ├── Key.cs
│   │   │   │   └── Switch.cs
│   │   │   ├── Player/
│   │   │   │   ├── InputHandler.cs
│   │   │   │   ├── InteractionDetector.cs
│   │   │   │   ├── Inventory.cs
│   │   │   │   ├── PlayerLook.cs
│   │   │   │   └── PlayerMovement
│   │   │   └── ScriptableObjects/
│   │   │   │   ├── InventoryItemSO.cs
│   │   │   │   └── PlayerLook.cs
│   │   │   └── SignalBus/
│   │   │   │   ├── OnInventoryItemCollectedSignal.cs
│   │   │   │   ├── OnProgressBarChangedSignal.cs
│   │   │   │   └── OnPrompTextChangedSignal.cs
│   │   │   └── UI/
│   │   │   │   ├── InteractionPromptController.cs
│   │   │   │   ├── ProgressBarController.cs
│   │   │   │   └── UIController.cs
│   │   │   └── Utilities/
│   │   │   │   └── Timer.cs
│   │   └── Editor/
│   ├── ScriptableObjects/
│   │   ├── k_doorKey_001
│   │   └──k_doorKey_002
│   ├── Prefabs/
│   │   ├── P_Chest
│   │   ├── P_Door_001
│   │   ├── P_Door_002_Switch
│   │   ├── P_InputHandler
│   │   ├── P_Key_001
│   │   ├── P_Key_002
│   │   ├── P_Player
│   │   ├── P_PlayerUI
│   │   └── P_TestSceneGameContext
│   ├── Materials/
│   │   ├── M_Chest
│   │   ├── M_Door
│   │   ├── M_Eyes
│   │   ├── M_Key001
│   │   ├── M_Key002
│   │   ├── M_Player
│   │   └── M_Switch_Panel
│   └── Scenes/
│       └── TestScene.unity
├── Docs/
│   ├── CSharp_Coding_Conventions.md
│   ├── Naming_Convention_Kilavuzu.md
│   └── Prefab_Asset_Kurallari.md
├── README.md
├── PROMPTS.md
└── .gitignore
```

---

## İletişim

| Bilgi | Değer |
|-------|-------|
| Ad Soyad | [Salih Şendil] |
| E-posta | [sendillsalih@gmail.com]  |
| LinkedIn | [https://www.linkedin.com/in/salihsendil/] |
| GitHub | [github.com/salihsendil] |

---

*Bu proje Ludu Arts Unity Developer Intern Case için hazırlanmıştır.*
