using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using TMPro;

namespace GAS.Demo
{
    public class SnackBar : Singleton<SnackBar>
    {
        public GameObject template;
        public RectTransform poolRect;
        public RectTransform targetRect;
        public int poolSize = 10;
        private Queue<GameObject> pool;
        private Queue<Snack> excessItems;

        public void CallBack()
        {
            // 取出队列中的内容
            if (excessItems.Count > 0)
            {
                Snack snack = excessItems.Dequeue();
                Show(snack.content, snack.color, snack.second);
            }
        }

        void Start()
        {
            pool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(template, poolRect);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }

            excessItems = new Queue<Snack>();
        }

        private GameObject GetObject()
        {
            if (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();
                obj.SetActive(true);
                return obj;
            }

            return null;
            //return Instantiate(template, rect);
        }

        private void ReturnObject(GameObject obj)
        {
            if (pool.Count > poolSize)
            {
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            obj.transform.SetParent(poolRect);
            pool.Enqueue(obj);
        }

        /// <summary>
        /// 显示定时提示（右上角）
        /// </summary>
        /// <param name="_content">内容</param>
        /// <param name="_second">显示秒数</param>
        /// <param name="_color">0默认 1红色 2蓝色 3橙色</param>
        public async void Show(string _content, int _color = 0, float _second = 3f)
        {
            GameObject go = GetObject();

            // 超出的数量储存在队列
            if (go == null)
            {
                Snack snack = new Snack
                {
                    content = _content,
                    color = _color,
                    second = _second
                };
                excessItems.Enqueue(snack);
                return;
            }

            RectTransform area = go.GetComponent<RectTransform>();

            Image background = go.GetComponentInChildren<Image>();
            RectTransform frameArea = background.rectTransform;

            Slider slider = go.GetComponentInChildren<Slider>();
            TextMeshProUGUI text = go.GetComponentInChildren<TextMeshProUGUI>();

            go.transform.SetParent(targetRect);

            float totalSecond = _second;

            slider.value = 1;
            text.text = _content;

            switch (_color)
            {
                default:
                    background.color = new Color(0, 0, 0, 0.8f); break;
                case 1:
                    background.color = new Color(0.5f, 0, 0, 0.8f); break;
                case 2:
                    background.color = new Color(0.15f, 0.15f, 0.55f, 0.8f); break;
                case 3:
                    background.color = new Color(1, 0.5f, 0, 0.8f); break;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(frameArea);

            area.sizeDelta = new Vector2(frameArea.rect.width, frameArea.rect.height + 20);
            frameArea.anchoredPosition = new Vector2(1500, -(frameArea.rect.height + 20) * 0.5f);

            frameArea.DOLocalMoveX(0, 0.8f).SetEase(Ease.OutCubic);
            slider.DOValue(0, totalSecond).SetEase(Ease.Linear);
            while (_second > 0)
            {
                _second--;
                await UniTask.Delay(1000, true);
            }

            if (_second <= 0)
            {
                area.DOSizeDelta(new Vector2(frameArea.sizeDelta.x, 0), 0.6f).SetEase(Ease.OutCubic)
                    .OnUpdate(() => { LayoutRebuilder.MarkLayoutForRebuild(targetRect); })
                    .OnComplete(() => { LayoutRebuilder.ForceRebuildLayoutImmediate(targetRect); });
                frameArea.DOLocalMoveX(1000, 0.6f).OnComplete(() =>
                {
                    ReturnObject(go);
                    CallBack();
                });
            }
        }

        private class Snack
        {
            public string content;
            public int color;
            public float second;
        }
    }
}
