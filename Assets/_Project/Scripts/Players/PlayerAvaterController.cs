using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerAvaterController : MonoBehaviour {


    // temp sprites for assigning avatars
    public SpriteRenderer spriteRenderer;
    public SpriteMask spriteMask;
    public Player player;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();

        // if the file is a .gif
        if (player.avatarPath.Contains (".gif"))
            // choose random avatar
            spriteRenderer.sprite = PlayerManager.Instance.avatars [Random.Range (0, PlayerManager.Instance.avatars.Length - 1)];
        else
            StartCoroutine (DownloadImage ("https://tallysavestheinternet.com/" + player.avatarPath));

        // set random sorting order
        spriteRenderer.sortingOrder = Random.Range (100, 10000);
        // get spritemask parent
        spriteMask = transform.GetComponentInParent<SpriteMask> ();
        // set sorting layers based on this order (prevents accidentally showing what's behind the mask of other avatars)
        spriteMask.frontSortingOrder = spriteRenderer.sortingOrder + 1;
        spriteMask.backSortingOrder = spriteRenderer.sortingOrder - 1;

    }

    IEnumerator DownloadImage (string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture (MediaUrl);
        yield return request.SendWebRequest ();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log (request.error + " " + MediaUrl);
        else {
            Texture2D onlineAvatar = ((DownloadHandlerTexture)request.downloadHandler).texture;
            spriteRenderer.sprite = Sprite.Create (onlineAvatar, new Rect (0, 0, onlineAvatar.width, onlineAvatar.height), new Vector2 (0.5f, 0.5f));
        }
    }

}
