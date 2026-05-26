using UnityEngine;
using UnityEngine.SceneManagement; // ✨ 必須引入這個命名空間才能控制場景切換

public class MapTransfer : MonoBehaviour
{
    [Header("Scene2")]
    public string sceneToLoad;

    // 當有帶有 Collider 2D 的物件進入觸發範圍時，Unity 會自動呼叫這個方法
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 檢查走進來的物件，它的 Tag 是不是 "Player"
        if (collision.CompareTag("Player"))
        {
            //Debug.Log("玩家踩到傳送點！準備切換地圖至：" + sceneToLoad);
            
            // 載入指定的場景（地圖）
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
