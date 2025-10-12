using R3;
using UnityEngine;

namespace myProject{
    public class BuildingsVisual: MonoBehaviour {
        public void  Init(ReactiveProperty<Vector2Int> position, ReactiveProperty<Color> color){
            position.Subscribe(newPosition => transform.position = new Vector3(position.Value.x, position.Value.y, 0f));
            color.Subscribe(newColor => this.GetComponent<SpriteRenderer>().color = newColor);
        }

        public void DestroyGo(){
            Destroy(this.gameObject);
        }
    }
}