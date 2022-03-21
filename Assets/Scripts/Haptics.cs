using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// 必須コンポーネントとしてXRGrabInteractableを指定
[RequireComponent(typeof(XRGrabInteractable))]
public class Haptics : MonoBehaviour
{
   // 外部から設定するパラメータ
   [Header("Trigger haptics parameters")]
   [SerializeField] float triggerAmplitude = 0.95f; // 振幅 (0.0〜1.0)
   [SerializeField] float triggerDuration = 0.2f; // 持続時間(秒) ()

   [Header("Grip haptics parameters")]
   [SerializeField] float gripAmplitude = 0.3f; // 振幅 (0.0〜1.0)
   [SerializeField] float gripDuration = 0.1f; // 持続時間(秒) ()

   [Header("Collided haptics parameters")]
   [SerializeField] float collidedAmplitude = 0.1f; // 振幅 (0.0〜1.0)
   [SerializeField] float collidedDuration = 0.1f; // 持続時間(秒) ()

   // コンポーネントが有効化された時に実行
   private void OnEnable()
   {
       // GameObjectに設定されているXRGrabInteractableコンポーネントを取得
       XRBaseInteractable interactable = GetComponent<XRGrabInteractable>();
       // Activate Action発生時に実行する理を追加
       interactable.activated.AddListener(pullTrigger);
       // Select Action発生時に実行する処理を追加
       interactable.selectEntered.AddListener(pullGrip);
       // 接触発生時に実行する処理を追加
       interactable.hoverEntered.AddListener(collided);
   }

   // コンポーネントが無効化された時に実行
   private void OnDisable()
   {
       // GameObjectに設定されているXRGrabInteractableコンポーネントを取得
       XRBaseInteractable interactable = GetComponent<XRGrabInteractable>();
       // Activate Action発生時に実行する処理を削除
       interactable.activated.RemoveListener(pullTrigger);
       // Select Action発生時に実行する処理を削除
       interactable.selectEntered.RemoveListener(pullGrip);
       // 接触発生時に実行する処理を削除
       interactable.hoverEntered.RemoveListener(collided);
   }

   // Activate Action(トリガー押下)発生時に実行する処理
   private void pullTrigger(ActivateEventArgs arg)
   {
       // イベント発生元のXRBaseControllerに振動させる。
       print("test");
    //    arg.interactorObject.GetComponent<XRBaseController>().SendHapticImpulse(triggerAmplitude,triggerDuration);
   }

   // Select Action(グリップ押下)発生時に実行する処理
   private void pullGrip(SelectEnterEventArgs arg)
   {
       // イベント発生元のXRBaseControllerに振動させる。
    //    arg.interactorObject.GetComponent<XRBaseController>().SendHapticImpulse(gripAmplitude,gripDuration);
   }

   // 接触の発生時に実行する処理
   private void collided(HoverEnterEventArgs arg)
   {
       // イベント発生元のXRBaseControllerに振動させる。
    //    arg.interactorObject.GetComponent<XRBaseController>().SendHapticImpulse(collidedAmplitude,collidedDuration);
   }
}