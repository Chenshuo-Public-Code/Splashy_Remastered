using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    public float JumpHeight = 3f;
    public float JumpDisZ = 3f;
    public float MaxRangeX = 3f; //Range limit X of the ball

    private bool isGameStart = false;
    private bool isMoving = false;

    public void Init()
    {
        GameManager.Instance.GameStartEvent += StartGame;
        GameManager.Instance.GameEndEvent += GameOver;
        StartJump();
    }
    private void Update()
    {
        if (!isGameStart) return;

        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mousePosOnScreen = Input.mousePosition;

            mousePosOnScreen.z = screenPos.z;
            Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);

            float clampedX = Mathf.Clamp(mousePosInWorld.x, -MaxRangeX, MaxRangeX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }
    }
    private void StartGame()
    {
        isGameStart = true;
        transform.DOKill();
        transform.position = new Vector3(0, 0.55f, 0);
        StartJump();
    }
    private void GameOver()
    {
        isGameStart=false;
        transform.DOKill();
    }
    private void StartJump()
    {
        transform.DOMoveY(JumpHeight, GameManager.Instance.GameSpeed * 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            transform.DOMoveY(0.55f, GameManager.Instance.GameSpeed * 0.5f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                CheckPlatform(); 
            });
        });

        if (isGameStart)
        {
            transform.DOMoveZ(transform.position.z + JumpDisZ, GameManager.Instance.GameSpeed).SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }
    }
    private void CheckPlatform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            if (hit.collider.CompareTag("Platform"))
            {
                StartJump();
                if (isGameStart)
                    hit.collider.GetComponent<Platform>().DeactivatePlatform();
                GameManager.Instance.IncrementScore();
            }
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }
}
